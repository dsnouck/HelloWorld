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
    using HelloWorld.TestHelpers.Builders;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="MessageRepository"/>.
    /// </summary>
    public class MessageRepositoryTests : IDisposable
    {
        private readonly MessageRepository systemUnderTest;
        private readonly HelloWorldContext helloWorldContextTestDouble;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageRepositoryTests"/> class.
        /// </summary>
        public MessageRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<HelloWorldContext>()
                .UseInMemoryDatabase($"HelloWorld{Guid.NewGuid()}")
                .Options;
            this.helloWorldContextTestDouble = new HelloWorldContext(options);
            this.systemUnderTest = new MessageRepository(this.helloWorldContextTestDouble);
        }

        /// <summary>
        /// Tests <see cref="MessageRepository.GetAllMessages()"/>.
        /// </summary>
        [Fact]
        public void GivenNoMessagesWhenGetAllMessagesIsCalledThenNoMessagesAreReturned()
        {
            // Arrange.

            // Act.
            var result = this.systemUnderTest.GetAllMessages();

            // Assert.
            result.Should().BeEmpty();
        }

        /// <summary>
        /// Tests <see cref="MessageRepository.GetAllMessages()"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenGetAllMessagesIsCalledThenTheMessageIsReturned()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            this.helloWorldContextTestDouble.Messages.Add(message);
            this.helloWorldContextTestDouble.SaveChanges();

            // Act.
            var result = this.systemUnderTest.GetAllMessages();

            // Assert.
            result.Should().BeEquivalentTo(new List<Message> { message });
        }

        /// <summary>
        /// Tests <see cref="MessageRepository.AddMessage(Message)"/>.
        /// </summary>
        [Fact]
        public void GivenTheMessageIsNullWhenAddMessageIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            var message = NullBuilder.Build<Message>();

            // Act.
            Action action = () => this.systemUnderTest.AddMessage(message);

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="MessageRepository.AddMessage(Message)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenAddMessageIsCalledThenTheMessageIsAdded()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();

            // Act.
            var result = this.systemUnderTest.AddMessage(message);

            // Assert.
            result.Should().BeEquivalentTo(message);
            this.helloWorldContextTestDouble.Messages.Should().HaveCount(1);
        }

        /// <summary>
        /// Tests <see cref="MessageRepository.GetMessage(Guid)"/>.
        /// </summary>
        [Fact]
        public void GivenANonExistentIdWhenGetMessageIsCalledThenNullIsReturned()
        {
            // Arrange.
            var id = Guid.NewGuid();

            // Act.
            var result = this.systemUnderTest.GetMessage(id);

            // Assert.
            result.Should().BeNull();
        }

        /// <summary>
        /// Tests <see cref="MessageRepository.GetMessage(Guid)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenGetMessageIsCalledThenTheMessageIsReturned()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            this.helloWorldContextTestDouble.Messages.Add(message);
            this.helloWorldContextTestDouble.SaveChanges();

            // Act.
            var result = this.systemUnderTest.GetMessage(message.Id);

            // Assert.
            result.Should().BeEquivalentTo(message);
        }

        /// <summary>
        /// Tests <see cref="MessageRepository.UpdateMessage(Message)"/>.
        /// </summary>
        [Fact]
        public void GivenTheMessageIsNullWhenUpdateMessageIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            var message = NullBuilder.Build<Message>();

            // Act.
            Action action = () => this.systemUnderTest.UpdateMessage(message);

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="MessageRepository.UpdateMessage(Message)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWithIdEmptyWhenUpdateMessageIsCalledThenTheMessageIsAdded()
        {
            // Arrange.
            var message = MessageBuilder
                .ABuilder()
                .WithId(Guid.Empty)
                .Build();

            // Act.
            var result = this.systemUnderTest.UpdateMessage(message);

            // Assert.
            result.Should().BeEquivalentTo(message);
            this.helloWorldContextTestDouble.Messages.Should().HaveCount(1);
        }

        /// <summary>
        /// Tests <see cref="MessageRepository.UpdateMessage(Message)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWithIdFilledWhenUpdateMessageIsCalledThenADbUpdateConcurrencyExceptionIsThrown()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();

            // Act.
            Action action = () => this.systemUnderTest.UpdateMessage(message);

            // Assert.
            action.Should().Throw<DbUpdateConcurrencyException>();
        }

        /// <summary>
        /// Tests <see cref="MessageRepository.UpdateMessage(Message)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenUpdateMessageIsCalledThenTheMessageIsUpdated()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            this.helloWorldContextTestDouble.Messages.Add(message);
            this.helloWorldContextTestDouble.SaveChanges();
            message.Content = "Updated";

            // Act.
            var result = this.systemUnderTest.UpdateMessage(message);

            // Assert.
            result.Should().BeEquivalentTo(message);
        }

        /// <summary>
        /// Tests <see cref="MessageRepository.RemoveMessage(Message)"/>.
        /// </summary>
        [Fact]
        public void GivenTheMessageIsNullWhenRemoveMessageIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            var message = NullBuilder.Build<Message>();

            // Act.
            Action action = () => this.systemUnderTest.RemoveMessage(message);

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="MessageRepository.RemoveMessage(Message)"/>.
        /// </summary>
        [Fact]
        public void GivenANewMessageWhenRemoveMessageIsCalledThenADbUpdateConcurrencyExceptionIsThrown()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();

            // Act.
            Action action = () => this.systemUnderTest.RemoveMessage(message);

            // Assert.
            action.Should().Throw<DbUpdateConcurrencyException>();
        }

        /// <summary>
        /// Tests <see cref="MessageRepository.RemoveMessage(Message)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenRemoveMessageIsCalledThenTheMessageIsRemoved()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            this.helloWorldContextTestDouble.Messages.Add(message);
            this.helloWorldContextTestDouble.SaveChanges();

            // Act.
            this.systemUnderTest.RemoveMessage(message);

            // Assert.
            this.helloWorldContextTestDouble.Messages.Should().BeEmpty();
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
