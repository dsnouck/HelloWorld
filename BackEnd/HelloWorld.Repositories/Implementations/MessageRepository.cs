﻿// <copyright file="MessageRepository.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Repositories.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HelloWorld.Database;
    using HelloWorld.Models;
    using HelloWorld.Repositories.Interfaces;

    /// <inheritdoc/>
    public class MessageRepository : IMessageRepository
    {
        private readonly HelloWorldContext helloWorldContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageRepository"/> class.
        /// </summary>
        /// <param name="helloWorldContext">A <see cref="HelloWorldContext"/>.</param>
        public MessageRepository(
            HelloWorldContext helloWorldContext)
        {
            this.helloWorldContext = helloWorldContext;
        }

        /// <inheritdoc/>
        public List<Message> GetMessages()
        {
            return this.helloWorldContext.Messages.ToList();
        }

        /// <inheritdoc/>
        public Message AddMessage(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.helloWorldContext.Messages.Add(message);
            this.helloWorldContext.SaveChanges();

            return message;
        }

        /// <inheritdoc/>
        public Message GetMessage(long id)
        {
            return this.helloWorldContext.Messages.SingleOrDefault(message => message.Id == id);
        }

        /// <inheritdoc/>
        public Message GetMessage(Guid externalId)
        {
            return this.helloWorldContext.Messages.SingleOrDefault(message => message.ExternalId == externalId);
        }

        /// <inheritdoc/>
        public Message EditMessage(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.helloWorldContext.Messages.Update(message);
            this.helloWorldContext.SaveChanges();

            return message;
        }

        /// <inheritdoc/>
        public void RemoveMessage(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            this.helloWorldContext.Messages.Remove(message);
            this.helloWorldContext.SaveChanges();
        }
    }
}
