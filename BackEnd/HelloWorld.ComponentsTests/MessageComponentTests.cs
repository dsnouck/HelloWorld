// <copyright file="MessageComponentTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.ComponentsTests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using HelloWorld.Components.Implementations;
    using HelloWorld.Entities;
    using HelloWorld.Repositories.Interfaces;
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
        public void GivenMessagesWhenGetAllMessagesIsCalledThenAllMessagesAreReturned()
        {
            // Arrange.
            var messages = new List<Message>
            {
                new Message
                {
                    Id = 1L,
                    Content = "Hello, world!",
                },
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
    }
}
