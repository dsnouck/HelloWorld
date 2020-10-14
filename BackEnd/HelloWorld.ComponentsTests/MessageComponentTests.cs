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
        /// Tests <see cref="MessageComponent.GetMessages()"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenGetMessagesIsCalledThenTheMessageIsReturned()
        {
            // Arrange.
            var messages = new List<Message>
            {
                MessageBuilder.ABuilder().Build(),
            };
            this.messageRepositoryTestDouble
                .Setup(repository => repository.GetMessages())
                .Returns(messages);

            // Act.
            var result = this.systemUnderTest.GetMessages();

            // Assert.
            result.Should().BeEquivalentTo(messages);
            this.messageRepositoryTestDouble
                .Verify(repository => repository.GetMessages(), Times.Once);
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
        /// Tests <see cref="MessageComponent.GetMessage(long)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenGetMessageOnLongIsCalledThenTheMessageIsReturned()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            this.messageRepositoryTestDouble
                .Setup(repository => repository.GetMessage(It.IsAny<long>()))
                .Returns(message);

            // Act.
            var result = this.systemUnderTest.GetMessage(message.Id);

            // Assert.
            result.Should().Be(message);
            this.messageRepositoryTestDouble
                .Verify(repository => repository.GetMessage(message.Id), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="MessageComponent.GetMessage(Guid)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenGetMessageOnGuidIsCalledThenTheMessageIsReturned()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            this.messageRepositoryTestDouble
                .Setup(repository => repository.GetMessage(It.IsAny<Guid>()))
                .Returns(message);

            // Act.
            var result = this.systemUnderTest.GetMessage(message.ExternalId);

            // Assert.
            result.Should().Be(message);
            this.messageRepositoryTestDouble
                .Verify(repository => repository.GetMessage(message.ExternalId), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="MessageComponent.EditMessage(Message)"/>.
        /// </summary>
        [Fact]
        public void GivenTheMessageIsNullWhenEditMessageIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            var message = NullBuilder.Build<Message>();

            // Act.
            Action action = () => this.systemUnderTest.EditMessage(message);

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="MessageComponent.EditMessage(Message)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenEditMessageIsCalledThenTheMessageIsEdited()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            this.messageRepositoryTestDouble
                .Setup(repository => repository.EditMessage(It.IsAny<Message>()))
                .Returns(message);

            // Act.
            var result = this.systemUnderTest.EditMessage(message);

            // Assert.
            result.Should().Be(message);
            this.messageRepositoryTestDouble
                .Verify(repository => repository.EditMessage(message), Times.Once);
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
