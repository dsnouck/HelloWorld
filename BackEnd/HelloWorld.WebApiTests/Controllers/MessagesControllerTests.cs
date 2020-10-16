// <copyright file="MessagesControllerTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApiTests.Controllers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using FluentAssertions;
    using HelloWorld.Components.Interfaces;
    using HelloWorld.Models;
    using HelloWorld.TestHelpers.Builders;
    using HelloWorld.ViewModels;
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
        private readonly Mock<IMapper> mapperTestDouble;
        private readonly Mock<IMessageComponent> messageComponentTestDouble;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesControllerTests"/> class.
        /// </summary>
        public MessagesControllerTests()
        {
            this.mapperTestDouble = new Mock<IMapper>();
            this.messageComponentTestDouble = new Mock<IMessageComponent>();
            this.systemUnderTest = new MessagesController(
                this.mapperTestDouble.Object,
                this.messageComponentTestDouble.Object);
        }

        /// <summary>
        /// Tests <see cref="MessagesController.GetMessages()"/>.
        /// </summary>
        [Fact]
        public void GivenNoMessagesWhenGetMessagesIsCalledThenNoMessagesAreReturned()
        {
            // Arrange.
            var messages = new List<Message>();
            var messageViewModels = new List<MessageViewModel>();
            this.messageComponentTestDouble
                .Setup(component => component.GetMessages())
                .Returns(messages);

            // Act.
            var result = this.systemUnderTest.GetMessages();

            // Assert.
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = (OkObjectResult)result;
            okObjectResult.Value.Should().BeEquivalentTo(messageViewModels);
            this.messageComponentTestDouble
                .Verify(component => component.GetMessages(), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="MessagesController.GetMessages()"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenGetMessagesIsCalledThenTheMessageIsReturned()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            var messages = new List<Message>
            {
                message,
            };
            var messageViewModel = MessageViewModelBuilder.ABuilder().Build();
            var messageViewModels = new List<MessageViewModel>
            {
                messageViewModel,
            };
            this.messageComponentTestDouble
                .Setup(component => component.GetMessages())
                .Returns(messages);
            this.mapperTestDouble
                .Setup(mapper => mapper.Map<MessageViewModel>(It.IsAny<Message>()))
                .Returns(messageViewModel);

            // Act.
            var result = this.systemUnderTest.GetMessages();

            // Assert.
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = (OkObjectResult)result;
            okObjectResult.Value.Should().BeEquivalentTo(messageViewModels);
            this.messageComponentTestDouble
                .Verify(component => component.GetMessages(), Times.Once);
            this.mapperTestDouble
                .Verify(mapper => mapper.Map<MessageViewModel>(message), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="MessagesController.AddMessage(MessageAddEditViewModel)"/>.
        /// </summary>
        [Fact]
        public void GivenTheMessageIsNullWhenAddMessageIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            var messageAddEditViewModel = NullBuilder.Build<MessageAddEditViewModel>();

            // Act.
            Action action = () => this.systemUnderTest.AddMessage(messageAddEditViewModel);

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="MessagesController.AddMessage(MessageAddEditViewModel)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenAddMessageIsCalledThenTheMessageIsAdded()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            var messageAddEditViewModel = MessageAddEditViewModelBuilder.ABuilder().Build();
            var messageViewModel = MessageViewModelBuilder.ABuilder().Build();
            this.mapperTestDouble
                .Setup(mapper => mapper.Map<Message>(It.IsAny<MessageAddEditViewModel>()))
                .Returns(message);
            this.mapperTestDouble
                .Setup(mapper => mapper.Map<MessageViewModel>(It.IsAny<Message>()))
                .Returns(messageViewModel);

            // Act.
            var result = this.systemUnderTest.AddMessage(messageAddEditViewModel);

            // Assert.
            result.Should().BeOfType<CreatedResult>();
            var createdresult = (CreatedResult)result;
            createdresult.Value.Should().BeEquivalentTo(messageViewModel);
            createdresult.Location.Should().Be($"/api/v1/Messages/{messageViewModel.Id}");
            this.mapperTestDouble
                .Verify(mapper => mapper.Map<Message>(messageAddEditViewModel), Times.Once);
            this.messageComponentTestDouble
                .Verify(component => component.AddMessage(message), Times.Once);
            this.mapperTestDouble
                .Verify(mapper => mapper.Map<MessageViewModel>(message), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="MessagesController.GetMessage(Guid)"/>.
        /// </summary>
        [Fact]
        public void GivenANonExistentIdWhenGetMessageIsCalledThenTheMessageIsNotFound()
        {
            // Arrange.
            var id = Guid.NewGuid();

            // Act.
            var result = this.systemUnderTest.GetMessage(id);

            // Assert.
            result.Should().BeOfType<NotFoundResult>();
            this.messageComponentTestDouble
                .Verify(component => component.GetMessage(id), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="MessagesController.GetMessage(Guid)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenGetMessageIsCalledThenTheMessageIsReturned()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            var messageViewModel = MessageViewModelBuilder.ABuilder().Build();
            this.messageComponentTestDouble
                .Setup(component => component.GetMessage(It.IsAny<Guid>()))
                .Returns(message);
            this.mapperTestDouble
                .Setup(mapper => mapper.Map<MessageViewModel>(It.IsAny<Message>()))
                .Returns(messageViewModel);

            // Act.
            var result = this.systemUnderTest.GetMessage(message.ExternalId);

            // Assert.
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = (OkObjectResult)result;
            okObjectResult.Value.Should().BeEquivalentTo(messageViewModel);
            this.messageComponentTestDouble
                .Verify(component => component.GetMessage(message.ExternalId), Times.Once);
            this.mapperTestDouble
                .Verify(mapper => mapper.Map<MessageViewModel>(message), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="MessagesController.EditMessage(Guid, MessageAddEditViewModel)"/>.
        /// </summary>
        [Fact]
        public void GivenTheMessageIsNullWhenEditMessageIsCalledThenAnArgumentNullExceptionIsThrown()
        {
            // Arrange.
            var id = Guid.NewGuid();
            var messageAddEditViewModel = NullBuilder.Build<MessageAddEditViewModel>();

            // Act.
            Action action = () => this.systemUnderTest.EditMessage(id, messageAddEditViewModel);

            // Assert.
            action.Should().Throw<ArgumentNullException>();
        }

        /// <summary>
        /// Tests <see cref="MessagesController.EditMessage(Guid, MessageAddEditViewModel)"/>.
        /// </summary>
        [Fact]
        public void GivenANonExistentIdWhenEditMessageIsCalledThenTheMessageIsNotFound()
        {
            // Arrange.
            var id = Guid.NewGuid();
            var messageAddEditViewModel = MessageAddEditViewModelBuilder.ABuilder().Build();

            // Act.
            var result = this.systemUnderTest.EditMessage(id, messageAddEditViewModel);

            // Assert.
            result.Should().BeOfType<NotFoundResult>();
            this.messageComponentTestDouble
                .Verify(component => component.GetMessage(id), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="MessagesController.EditMessage(Guid, MessageAddEditViewModel)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenEditMessageIsCalledThenTheMessageIsEdited()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            var messageAddEditViewModel = MessageAddEditViewModelBuilder.ABuilder().Build();
            var messageViewModel = MessageViewModelBuilder.ABuilder().Build();
            this.messageComponentTestDouble
                .Setup(component => component.GetMessage(It.IsAny<Guid>()))
                .Returns(message);
            this.mapperTestDouble
                .Setup(mapper => mapper.Map<MessageViewModel>(It.IsAny<Message>()))
                .Returns(messageViewModel);

            // Act.
            var result = this.systemUnderTest.EditMessage(message.ExternalId, messageAddEditViewModel);

            // Assert.
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = (OkObjectResult)result;
            okObjectResult.Value.Should().BeEquivalentTo(messageViewModel);
            this.messageComponentTestDouble
                .Verify(component => component.GetMessage(message.ExternalId), Times.Once);
            this.mapperTestDouble
                .Verify(mapper => mapper.Map(messageAddEditViewModel, message), Times.Once);
            this.messageComponentTestDouble
                .Verify(component => component.EditMessage(message), Times.Once);
            this.mapperTestDouble
                .Verify(mapper => mapper.Map<MessageViewModel>(message), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="MessagesController.RemoveMessage(Guid)"/>.
        /// </summary>
        [Fact]
        public void GivenANonExistentIdWhenRemoveMessageIsCalledThenTheMessageIsNotFound()
        {
            // Arrange.
            var id = Guid.NewGuid();

            // Act.
            var result = this.systemUnderTest.RemoveMessage(id);

            // Assert.
            result.Should().BeOfType<NotFoundResult>();
            this.messageComponentTestDouble
                .Verify(component => component.GetMessage(id), Times.Once);
        }

        /// <summary>
        /// Tests <see cref="MessagesController.RemoveMessage(Guid)"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenRemoveMessageIsCalledThenTheMessageIsRemoved()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            this.messageComponentTestDouble
                .Setup(component => component.GetMessage(It.IsAny<Guid>()))
                .Returns(message);

            // Act.
            var result = this.systemUnderTest.RemoveMessage(message.ExternalId);

            // Assert.
            result.Should().BeOfType<NoContentResult>();
            this.messageComponentTestDouble
                .Verify(component => component.GetMessage(message.ExternalId), Times.Once);
            this.messageComponentTestDouble
                .Verify(component => component.RemoveMessage(message), Times.Once);
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
