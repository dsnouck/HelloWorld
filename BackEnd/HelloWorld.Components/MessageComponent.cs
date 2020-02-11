// <copyright file="MessageComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Components
{
    using System.Collections.Generic;
    using HelloWorld.Entities;

    /// <inheritdoc/>
    public class MessageComponent : IMessageComponent
    {
        private readonly string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageComponent"/> class.
        /// </summary>
        public MessageComponent()
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
