// <copyright file="MessageComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Components.Implementations
{
    using System;
    using System.Collections.Generic;
    using HelloWorld.Components.Interfaces;
    using HelloWorld.Models;
    using HelloWorld.Repositories.Interfaces;

    /// <inheritdoc/>
    public class MessageComponent : IMessageComponent
    {
        private readonly IMessageRepository messageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageComponent"/> class.
        /// </summary>
        /// <param name="messageRepository">An <see cref="IMessageRepository"/>.</param>
        public MessageComponent(
            IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        /// <inheritdoc/>
        public List<Message> GetMessages()
        {
            return this.messageRepository.GetMessages();
        }

        /// <inheritdoc/>
        public Message AddMessage(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            return this.messageRepository.AddMessage(message);
        }

        /// <inheritdoc/>
        public Message GetMessage(long id)
        {
            return this.messageRepository.GetMessage(id);
        }

        /// <inheritdoc/>
        public Message GetMessage(Guid externalId)
        {
            return this.messageRepository.GetMessage(externalId);
        }

        /// <inheritdoc/>
        public Message EditMessage(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            return this.messageRepository.EditMessage(message);
        }

        /// <inheritdoc/>
        public void RemoveMessage(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.messageRepository.RemoveMessage(message);
        }
    }
}
