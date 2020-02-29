// <copyright file="MessageRepository.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Repositories.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using HelloWorld.Database;
    using HelloWorld.Entities;
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
        public Message AddMessage(Message message)
        {
            this.helloWorldContext.Messages?.Add(message);
            this.helloWorldContext.SaveChanges();
            return message;
        }

        /// <inheritdoc/>
        public List<Message> GetAllMessages()
        {
            return this.helloWorldContext.Messages.ToList();
        }

        /// <inheritdoc/>
        public Message GetMessage(long id)
        {
            return this.helloWorldContext.Messages.Single(message => message.Id == id);
        }

        /// <inheritdoc/>
        public void RemoveMessage(long id)
        {
            var message = this.helloWorldContext.Messages.Single(message => message.Id == id);
            this.helloWorldContext.Messages?.Remove(message);
            this.helloWorldContext.SaveChanges();
        }

        /// <inheritdoc/>
        public Message UpdateMessage(Message message)
        {
            this.helloWorldContext.Messages?.Update(message);
            this.helloWorldContext.SaveChanges();
            return message;
        }
    }
}
