// <copyright file="MessageComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Components.Implementations
{
    using System.Collections.Generic;
    using HelloWorld.Components.Interfaces;
    using HelloWorld.Entities;
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
        public Message AddMessage(Message message)
        {
            return this.messageRepository.AddMessage(message);
        }

        /// <inheritdoc/>
        public List<Message> GetAllMessages()
        {
            return this.messageRepository.GetAllMessages();
        }

        /// <inheritdoc/>
        public Message GetMessage(long id)
        {
            return this.messageRepository.GetMessage(id);
        }

        /// <inheritdoc/>
        public Message UpdateMessage(Message message)
        {
            return this.messageRepository.UpdateMessage(message);
        }
    }
}
