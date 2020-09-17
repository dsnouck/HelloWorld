// <copyright file="MessageComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.ComponentsTests
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using HelloWorld.Components.Implementations;
    using HelloWorld.Models;
    using HelloWorld.Repositories.Interfaces;
    using HelloWorld.TestHelpers.Builders;
    using Moq;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="MessageComponent"/>.
    /// </summary>
    public class MessageComponentTests
    {
        private readonly MessageComponent systemUnderTest;
        private readonly Mock<IMessageRepository> messageRepositoryTestDouble;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageComponentTests"/> class.
        /// </summary>
        public MessageComponentTests()
        {
            this.messageRepositoryTestDouble = new Mock<IMessageRepository>();
            this.systemUnderTest = new MessageComponent(
                this.messageRepositoryTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="MessageComponent.GetAllMessages()"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenGetAllMessagesIsCalledThenTheMessageIsReturned()
        {
            // Arrange.
            var messages = new List<Message>
            {
                MessageBuilder.ABuilder().Build(),
            };
            this.messageRepositoryTestDouble
                .Setup(repository => repository.GetAllMessages())
                .Returns(messages);

            // Act.
            var result = this.systemUnderTest.GetAllMessages();

            // Assert.
            result.Should().BeEquivalentTo(messages);
            this.messageRepositoryTestDouble
                .Verify(repository => repository.GetAllMessages(), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="MessageComponent.AddMessage(Message)"/>.
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
        /// Tests <see cref="MessageComponent.AddMessage(Message)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenAddMessageIsCalledThenTheMessageIsAdded()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            this.messageRepositoryTestDouble
                .Setup(repository => repository.AddMessage(It.IsAny<Message>()))
                .Returns(message);

            // Act.
            var result = this.systemUnderTest.AddMessage(message);

            // Assert.
            result.Should().Be(message);
            this.messageRepositoryTestDouble
                .Verify(repository => repository.AddMessage(message), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="MessageComponent.GetMessage(Guid)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenGetMessageIsCalledThenTheMessageIsReturned()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            this.messageRepositoryTestDouble
                .Setup(repository => repository.GetMessage(It.IsAny<Guid>()))
                .Returns(message);

            // Act.
            var result = this.systemUnderTest.GetMessage(message.Id);

            // Assert.
            result.Should().Be(message);
            this.messageRepositoryTestDouble
                .Verify(repository => repository.GetMessage(message.Id), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="MessageComponent.UpdateMessage(Message)"/>.
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
        /// Tests <see cref="MessageComponent.UpdateMessage(Message)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenUpdateMessageIsCalledThenTheMessageIsUpdated()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            this.messageRepositoryTestDouble
                .Setup(repository => repository.UpdateMessage(It.IsAny<Message>()))
                .Returns(message);

            // Act.
            var result = this.systemUnderTest.UpdateMessage(message);

            // Assert.
            result.Should().Be(message);
            this.messageRepositoryTestDouble
                .Verify(repository => repository.UpdateMessage(message), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="MessageComponent.RemoveMessage(Message)"/>.
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
        /// Tests <see cref="MessageComponent.RemoveMessage(Message)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenRemoveMessageIsCalledThenTheMessageIsRemoved()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();

            // Act.
            this.systemUnderTest.RemoveMessage(message);

            // Assert.
            this.messageRepositoryTestDouble
                .Verify(repository => repository.RemoveMessage(message), Times.Once);
        }
    }
}
