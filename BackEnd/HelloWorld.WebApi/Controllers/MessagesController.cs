// <copyright file="MessagesController.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using HelloWorld.Components.Interfaces;
    using HelloWorld.Entities;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// A controller for <see cref="Message"/>s.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
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
        /// Adds a <see cref="Message"/>.
        /// </summary>
        /// <param name="message">A <see cref="Message"/>.</param>
        /// <returns>The added <see cref="Message"/>.</returns>
        [HttpPost]
        public Message AddMessage(Message message)
        {
            return this.messageComponent.AddMessage(message);
        }

        /// <summary>
        /// Gets all <see cref="Message"/>s.
        /// </summary>
        /// <returns>All <see cref="Message"/>s.</returns>
        [HttpGet]
        public List<Message> GetAllMessages()
        {
            return this.messageComponent.GetAllMessages();
        }

        /// <summary>
        /// Gets the <see cref="Message"/> with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The <see cref="Message"/> with the given <paramref name="id"/>.</returns>
        [HttpGet("{id}")]
        public Message GetMessage(long id)
        {
            return this.messageComponent.GetMessage(id);
        }

        /// <summary>
        /// Removes the <see cref="Message"/> with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        [HttpDelete("{id}")]
        public void RemoveMessage(long id)
        {
            this.messageComponent.RemoveMessage(id);
        }

        /// <summary>
        /// Updates the <see cref="Message"/> with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="message">The message.</param>
        /// <returns>The updated <see cref="Message"/>.</returns>
        [HttpPut("{id}")]
        public Message UpdateMessage(long id, Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            message.Id = id;
            return this.messageComponent.UpdateMessage(message);
        }
    }
}
