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
    using HelloWorld.Models;
    using HelloWorld.Repositories.Implementations;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="MessageRepository"/>.
    /// </summary>
    public class MessageRepositoryTests : IDisposable
    {
        private readonly MessageRepository systemUnderTest;
        private readonly HelloWorldContext helloWorldContextTestDouble;
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageRepositoryTests"/> class.
        /// </summary>
        public MessageRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<HelloWorldContext>()
                .UseInMemoryDatabase("HelloWorld")
                .Options;
            this.helloWorldContextTestDouble = new HelloWorldContext(options);
            this.systemUnderTest = new MessageRepository(this.helloWorldContextTestDouble);
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

            this.helloWorldContextTestDouble.Messages?.Add(message);
            this.helloWorldContextTestDouble.SaveChanges();

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
                this.helloWorldContextTestDouble.Dispose();
            }

            this.disposed = true;
        }
    }
}
