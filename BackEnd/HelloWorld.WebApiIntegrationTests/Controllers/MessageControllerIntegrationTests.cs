// <copyright file="MessageControllerIntegrationTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApiIntegrationTests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using FluentAssertions;
    using HelloWorld.TestHelpers.Builders;
    using HelloWorld.ViewModels;
    using HelloWorld.WebApi;
    using HelloWorld.WebApi.Controllers;
    using Xunit;

    /// <summary>
    /// Provides integration tests for <see cref="MessagesController"/>.
    /// </summary>
    public class MessageControllerIntegrationTests : IDisposable
    {
        private readonly InMemoryWebApplicationFactory<Startup> factory;
        private readonly HttpClient client;
        private readonly Uri uri;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageControllerIntegrationTests"/> class.
        /// </summary>
        public MessageControllerIntegrationTests()
        {
            this.factory = new InMemoryWebApplicationFactory<Startup>();
            this.client = this.factory.CreateClient();
            this.uri = new Uri("http://localhost/api/v1/Messages");
        }

        /// <summary>
        /// Tests GET /messages.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GivenNoMessagesWhenGetMessagesIsCalledThenNoMessagesAreReturned()
        {
            // Arrange

            // Act
            var response = await this.client.GetAsync(this.uri).ConfigureAwait(false);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var messages = Deserialize<List<MessageViewModel>>(content);
            messages.Should().BeEmpty();
        }

        /// <summary>
        /// Tests GET /messages.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GivenAMessageWhenGetMessagesIsCalledThenTheMessageIsReturned()
        {
            // Arrange
            var messageToAdd = MessageAddEditViewModelBuilder.ABuilder().Build();
            var addedMessage = await this.AddMessage(messageToAdd).ConfigureAwait(false);

            // Act
            var response = await this.client.GetAsync(this.uri).ConfigureAwait(false);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var messages = Deserialize<List<MessageViewModel>>(content);
            messages.Should().BeEquivalentTo(new List<MessageViewModel> { addedMessage });
        }

        /// <summary>
        /// Tests POST /messages.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GivenAnInvalidMessageWhenAddMessageIsCalledThenTheMessageIsNotAdded()
        {
            // Arrange
            var messageToAdd = MessageAddEditViewModelBuilder
                .ABuilder()
                .WithContent(string.Empty)
                .Build();
            using var stringContent = GetStringContent(messageToAdd);

            // Act
            var response = await this.client.PostAsync(this.uri, stringContent).ConfigureAwait(false);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var allMessages = await this.GetMessages().ConfigureAwait(false);
            allMessages.Should().BeEmpty();
        }

        /// <summary>
        /// Tests POST /messages.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GivenAMessageWhenAddMessageIsCalledThenTheMessageIsAdded()
        {
            // Arrange
            var messageToAdd = MessageAddEditViewModelBuilder.ABuilder().Build();
            using var stringContent = GetStringContent(messageToAdd);

            // Act
            var response = await this.client.PostAsync(this.uri, stringContent).ConfigureAwait(false);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var message = Deserialize<MessageViewModel>(content);
            var allMessages = await this.GetMessages().ConfigureAwait(false);
            allMessages.Should().BeEquivalentTo(new List<MessageViewModel> { message });
        }

        /// <summary>
        /// Tests GET /messages.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GivenANonExistentIdWhenGetMessageIsCalledThenThemessageIsNotFound()
        {
            // Arrange
            var uri = new Uri($"{this.uri}/{Guid.NewGuid()}");

            // Act
            var response = await this.client.GetAsync(uri).ConfigureAwait(false);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Tests GET /messages.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GivenAMessageWhenGetMessageIsCalledThenTheMessageIsReturned()
        {
            // Arrange
            var messageToAdd = MessageAddEditViewModelBuilder.ABuilder().Build();
            var addedMessage = await this.AddMessage(messageToAdd).ConfigureAwait(false);
            var uri = new Uri($"{this.uri}/{addedMessage.Id}");

            // Act
            var response = await this.client.GetAsync(uri).ConfigureAwait(false);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var message = Deserialize<MessageViewModel>(content);
            var allMessages = await this.GetMessages().ConfigureAwait(false);
            allMessages.Should().BeEquivalentTo(new List<MessageViewModel> { message });
        }

        /// <summary>
        /// Tests PUT /messages.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GivenANonExistentIdWhenEditMessageIsCalledThenTheMessageIsNotFound()
        {
            // Arrange
            var uri = new Uri($"{this.uri}/{Guid.NewGuid()}");
            var messageAddEditViewModel = MessageAddEditViewModelBuilder.ABuilder().Build();
            using var stringContent = GetStringContent(messageAddEditViewModel);

            // Act
            var response = await this.client.PutAsync(uri, stringContent).ConfigureAwait(false);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            var allMessages = await this.GetMessages().ConfigureAwait(false);
            allMessages.Should().BeEmpty();
        }

        /// <summary>
        /// Tests PUT /messages.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GivenAnInvalidMessageWhenEditMessageIsCalledThenTheMessageIsNotEdited()
        {
            // Arrange
            var messageToAdd = MessageAddEditViewModelBuilder.ABuilder().Build();
            var addedMessage = await this.AddMessage(messageToAdd).ConfigureAwait(false);
            var uri = new Uri($"{this.uri}/{addedMessage.Id}");
            var messageToEdit = MessageAddEditViewModelBuilder
                .ABuilder()
                .WithContent(string.Empty)
                .Build();
            using var stringContent = GetStringContent(messageToEdit);

            // Act
            var response = await this.client.PutAsync(uri, stringContent).ConfigureAwait(false);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var allMessages = await this.GetMessages().ConfigureAwait(false);
            allMessages.Should().BeEquivalentTo(new List<MessageViewModel> { addedMessage });
        }

        /// <summary>
        /// Tests PUT /messages.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GivenAMessageWhenEditMessageIsCalledThenTheMessageIsEdited()
        {
            // Arrange
            var messageToAdd = MessageAddEditViewModelBuilder.ABuilder().Build();
            var addedMessage = await this.AddMessage(messageToAdd).ConfigureAwait(false);
            var uri = new Uri($"{this.uri}/{addedMessage.Id}");
            var messageToEdit = MessageAddEditViewModelBuilder
                .ABuilder()
                .WithContent("Edited")
                .Build();
            using var stringContent = GetStringContent(messageToEdit);

            // Act
            var response = await this.client.PutAsync(uri, stringContent).ConfigureAwait(false);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var message = Deserialize<MessageViewModel>(content);
            var allMessages = await this.GetMessages().ConfigureAwait(false);
            allMessages.Should().BeEquivalentTo(new List<MessageViewModel> { message });
        }

        /// <summary>
        /// Tests DELETE /messages.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GivenANonExistentIdWhenRemoveMessageIsCalledThenTheMessageIsNotFound()
        {
            // Arrange
            var uri = new Uri($"{this.uri}/{Guid.NewGuid()}");

            // Act
            var response = await this.client.DeleteAsync(uri).ConfigureAwait(false);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        /// <summary>
        /// Tests DELETE /messages.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GivenAMessageWhenRemoveMessageIsCalledThenTheMessageIsRemoved()
        {
            // Arrange
            var messageToAdd = MessageAddEditViewModelBuilder.ABuilder().Build();
            var addedMessage = await this.AddMessage(messageToAdd).ConfigureAwait(false);
            var uri = new Uri($"{this.uri}/{addedMessage.Id}");

            // Act
            var response = await this.client.DeleteAsync(uri).ConfigureAwait(false);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            var allMessages = await this.GetMessages().ConfigureAwait(false);
            allMessages.Should().BeEmpty();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of resources.
        /// </summary>
        /// <param name="disposing">Whether we are disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.client.Dispose();
                this.factory.Dispose();
            }

            this.disposed = true;
        }

        private static TValue Deserialize<TValue>(string content)
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<TValue>(content, jsonSerializerOptions);
        }

        private static StringContent GetStringContent<TValue>(TValue value)
        {
            return new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        }

        private async Task<List<MessageViewModel>> GetMessages()
        {
            var response = await this.client.GetAsync(this.uri).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return Deserialize<List<MessageViewModel>>(content);
        }

        private async Task<MessageViewModel> AddMessage(MessageAddEditViewModel messageAddEditViewModel)
        {
            using var stringContent = GetStringContent(messageAddEditViewModel);
            var response = await this.client.PostAsync(this.uri, stringContent).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return Deserialize<MessageViewModel>(content);
        }
    }
}