// <copyright file="MessagesController.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
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
        public List<Message> GetAllMessages()
        {
            return this.messageComponent.GetAllMessages();
        }

        /// <summary>
        /// Adds a message.
        /// </summary>
        /// <param name="message">A message.</param>
        /// <returns>The added message.</returns>
        [HttpPost]
        public Message AddMessage(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Id = Guid.Empty;
            return this.messageComponent.AddMessage(message);
        }

        /// <summary>
        /// Gets the message with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The message with the given <paramref name="id"/>.</returns>
        [HttpGet("{id}")]
        public Message GetMessage(Guid id)
        {
            return this.messageComponent.GetMessage(id);
        }

        /// <summary>
        /// Updates the message with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="message">The message.</param>
        /// <returns>The updated message.</returns>
        [HttpPut("{id}")]
        public Message UpdateMessage(Guid id, Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Id = id;
            return this.messageComponent.UpdateMessage(message);
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
