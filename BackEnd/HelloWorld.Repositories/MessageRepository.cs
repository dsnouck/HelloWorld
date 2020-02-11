// <copyright file="MessageRepository.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Repositories
{
    using System.Collections.Generic;
    using HelloWorld.Entities;

    /// <inheritdoc/>
    public class MessageRepository : IMessageRepository
    {
        private readonly string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageRepository"/> class.
        /// </summary>
        public MessageRepository()
        {
            this.text = "Hello, world!";
        }

        /// <inheritdoc/>
        public List<Message> GetAllMessages()
        {
            return new List<Message>
            {
                new Message
                {
                    Text = this.text,
                },
            };
        }
    }
}
