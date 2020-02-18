// <copyright file="MessagesControllerTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApiTests.Controllers
{
    using System.Collections.Generic;
    using FluentAssertions;
    using HelloWorld.Components.Interfaces;
    using HelloWorld.Entities;
    using HelloWorld.WebApi.Controllers;
    using Moq;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="MessagesController"/>.
    /// </summary>
    public class MessagesControllerTests
    {
        private readonly MessagesController systemUnderTest;
        private readonly Mock<IMessageComponent> messageComponentTestDouble;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesControllerTests"/> class.
        /// </summary>
        public MessagesControllerTests()
        {
            this.messageComponentTestDouble = new Mock<IMessageComponent>();
            this.systemUnderTest = new MessagesController(
                this.messageComponentTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="MessagesController.GetAllMessages()"/>.
        /// </summary>
        [Fact]
        public void GivenMessagesWhenGetAllMessagesIsCalledThenAllMessagesAreReturned()
        {
            // Arrange.
            var messages = new List<Entities.Message>
            {
                new Message
                {
                    Id = 1L,
                    Content = "Hello, world!",
                },
            };
            this.messageComponentTestDouble
                .Setup(component => component.GetAllMessages())
                .Returns(messages);

            // Act.
            var result = this.systemUnderTest.GetAllMessages();

            // Assert.
            result.Should().BeEquivalentTo(messages);
            this.messageComponentTestDouble
                .Verify(component => component.GetAllMessages(), Times.Once);
        }
    }
}
