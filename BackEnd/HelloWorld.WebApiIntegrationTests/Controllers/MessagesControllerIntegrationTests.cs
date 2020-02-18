// <copyright file="MessagesControllerIntegrationTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApiIntegrationTests.Controllers
{
    using System;
    using System.Threading.Tasks;
    using FluentAssertions;
    using HelloWorld.WebApi;
    using HelloWorld.WebApi.Controllers;
    using Xunit;

    /// <summary>
    /// Provides integration tests for <see cref="MessagesController"/>.
    /// </summary>
    public class MessagesControllerIntegrationTests : IClassFixture<InMemoryWebApplicationFactory<Startup>>
    {
        private readonly InMemoryWebApplicationFactory<Startup> factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesControllerIntegrationTests"/> class.
        /// </summary>
        /// <param name="factory">An <see cref="InMemoryWebApplicationFactory{TStartup}"/>.</param>
        public MessagesControllerIntegrationTests(
            InMemoryWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
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
            content.Should().Be(@"[{""id"":1,""content"":""Hello, world!""}]");
        }
    }
}