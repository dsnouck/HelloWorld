// <copyright file="MessagesControllerIntegrationTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApiIntegrationTests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;
    using FluentAssertions;
    using FluentAssertions.Equivalency;
    using HelloWorld.Models;
    using HelloWorld.WebApi;
    using HelloWorld.WebApi.Controllers;
    using Xunit;

    /// <summary>
    /// Provides integration tests for <see cref="MessagesController"/>.
    /// </summary>
    public class MessagesControllerIntegrationTests : IClassFixture<InMemoryWebApplicationFactory<Startup>>
    {
        private readonly InMemoryWebApplicationFactory<Startup> factory;
        private readonly JsonSerializerOptions jsonSerializerOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesControllerIntegrationTests"/> class.
        /// </summary>
        /// <param name="factory">An <see cref="InMemoryWebApplicationFactory{TStartup}"/>.</param>
        public MessagesControllerIntegrationTests(
            InMemoryWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
            this.jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        /// <summary>
        /// Tests GET /messages.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GivenMessagesWhenGetMessagesIsCalledThenAllMessagesAreReturned()
        {
            // Arrange
            var client = this.factory.CreateClient();
            var uri = new Uri("http://localhost/messages");

            // Act
            var response = await client.GetAsync(uri).ConfigureAwait(false);

            // Assert
            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var messages = JsonSerializer.Deserialize<List<Message>>(content, this.jsonSerializerOptions);
            messages.Should().BeEquivalentTo(
            new List<Message>
            {
                new Message
                {
                    Content = "Hello, world!",
                },
            }, IgnoreId);
        }

        private static EquivalencyAssertionOptions<Message> IgnoreId(EquivalencyAssertionOptions<Message> options)
        {
            return options.Excluding(message => message.Id);
        }
    }
}