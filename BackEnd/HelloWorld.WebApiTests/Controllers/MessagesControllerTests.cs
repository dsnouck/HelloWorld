// <copyright file="MessagesControllerTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApiTests.Controllers
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using HelloWorld.Components.Interfaces;
    using HelloWorld.Models;
    using HelloWorld.WebApi.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="MessagesController"/>.
    /// </summary>
    public class MessagesControllerTests : IDisposable
    {
        private readonly MessagesController systemUnderTest;
        private readonly Mock<IMessageComponent> messageComponentTestDouble;
        private bool disposed = false;

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
            var messages = new List<Message>
            {
                new Message
                {
                    Id = Guid.NewGuid(),
                    Content = "Hello, world!",
                },
            };
            this.messageComponentTestDouble
                .Setup(component => component.GetAllMessages())
                .Returns(messages);

            // Act.
            var result = this.systemUnderTest.GetAllMessages();

            // Assert.
            result.Should().BeOfType<OkObjectResult>();
            var resultValue = ((OkObjectResult)result).Value;
            resultValue.Should().BeEquivalentTo(messages);
            this.messageComponentTestDouble
                .Verify(component => component.GetAllMessages(), Times.Once);
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
                this.systemUnderTest.Dispose();
            }

            this.disposed = true;
        }
    }
}
