// <copyright file="MessagesController.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HelloWorld.Components.Interfaces;
    using HelloWorld.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// A controller for messages.
    /// </summary>
    [ApiController]
    [Route("messages")]
    public class MessagesController
    {
        private readonly IMessageComponent messageComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesController"/> class.
        /// </summary>
        /// <param name="messageComponent">An <see cref="IMessageComponent"/>.</param>
        public MessagesController(
            IMessageComponent messageComponent)
        {
            this.messageComponent = messageComponent;
        }

        /// <summary>
        /// Gets all messages.
        /// </summary>
        /// <returns>All messages.</returns>
        [HttpGet]
        public List<MessageViewModel> GetAllMessages()
        {
            return this.messageComponent
                .GetAllMessages()
                .Select(message => new MessageViewModel
                {
                    Id = message.Id,
                    Content = message.Content,
                })
                .ToList();
        }

        /// <summary>
        /// Adds a message.
        /// </summary>
        /// <param name="message">A message.</param>
        /// <returns>The added message.</returns>
        [HttpPost]
        public MessageViewModel AddMessage(MessageAddEditViewModel message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var newMessage = new Message
            {
                Content = message.Content,
            };

            this.messageComponent.AddMessage(newMessage);

            return new MessageViewModel
            {
                Id = newMessage.Id,
                Content = newMessage.Content,
            };
        }

        /// <summary>
        /// Gets the message with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The message with the given <paramref name="id"/>.</returns>
        [HttpGet("{id}")]
        public MessageViewModel GetMessage(Guid id)
        {
            var message = this.messageComponent.GetMessage(id);
            return new MessageViewModel
            {
                Id = message.Id,
                Content = message.Content,
            };
        }

        /// <summary>
        /// Updates the message with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="message">The message.</param>
        /// <returns>The updated message.</returns>
        [HttpPut("{id}")]
        public MessageViewModel UpdateMessage(Guid id, MessageAddEditViewModel message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var updateMessage = new Message
            {
                Id = id,
                Content = message.Content,
            };

            this.messageComponent.UpdateMessage(updateMessage);

            return new MessageViewModel
            {
                Id = updateMessage.Id,
                Content = updateMessage.Content,
            };
        }

        /// <summary>
        /// Removes the message with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        [HttpDelete("{id}")]
        public void RemoveMessage(Guid id)
        {
            this.messageComponent.RemoveMessage(id);
        }
    }
}
