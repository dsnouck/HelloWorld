// <copyright file="MessageRepositoryTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.RepositoriesTests
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using HelloWorld.Database;
    using HelloWorld.Entities;
    using HelloWorld.Repositories.Implementations;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="MessageRepository"/>.
    /// </summary>
    public class MessageRepositoryTests : IDisposable
    {
        private readonly MessageRepository systemUnderTest;
        private readonly HelloWorldContext helloWorldContext;
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageRepositoryTests"/> class.
        /// </summary>
        public MessageRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<HelloWorldContext>()
                .UseInMemoryDatabase("HelloWorld")
                .Options;
            this.helloWorldContext = new HelloWorldContext(options);
            this.systemUnderTest = new MessageRepository(this.helloWorldContext);
        }

        /// <summary>
        /// Tests <see cref="MessageRepository.GetAllMessages()"/>.
        /// </summary>
        [Fact]
        public void GivenMessagesWhenGetAllMessagesIsCalledThenAllMessagesAreReturned()
        {
            // Arrange.
            var message = new Message
            {
                Content = "Hello, world.",
            };

            this.helloWorldContext.Messages?.Add(message);
            this.helloWorldContext.SaveChanges();

            // Act.
            var result = this.systemUnderTest.GetAllMessages();

            // Assert.
            result.Should().BeEquivalentTo(new List<Message> { message });
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
                this.helloWorldContext.Dispose();
            }

            this.disposed = true;
        }
    }
}
