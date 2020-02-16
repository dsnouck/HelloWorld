// <copyright file="Message.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Entities
{
    /// <summary>
    /// Represents a message.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Content { get; set; } = string.Empty;
    }
}
