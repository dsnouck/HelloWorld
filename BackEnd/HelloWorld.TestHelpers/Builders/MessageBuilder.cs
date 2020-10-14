// <copyright file="MessageBuilder.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.TestHelpers.Builders
{
    using System;
    using HelloWorld.Models;

    /// <summary>
    /// Represents a builder for <see cref="Message"/>s.
    /// </summary>
    public class MessageBuilder
    {
        private readonly Message message;

        private MessageBuilder()
        {
            this.message = new Message
            {
                Id = 1L,
                ExternalId = Guid.NewGuid(),
                AddedOn = DateTime.UtcNow,
                EditedOn = DateTime.UtcNow,
                Content = "Hello, world!",
            };
        }

        /// <summary>
        /// Creates a new <see cref="MessageBuilder"/>.
        /// </summary>
        /// <returns>A new <see cref="MessageBuilder"/>.</returns>
        public static MessageBuilder ABuilder()
        {
            return new MessageBuilder();
        }

        /// <summary>
        /// Builds the <see cref="Message"/>.
        /// </summary>
        /// <returns>The <see cref="Message"/>.</returns>
        public Message Build()
        {
            return this.message;
        }

        /// <summary>
        /// Sets <see cref="PersistentModel.Id"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The updated <see cref="MessageBuilder"/>.</returns>
        public MessageBuilder WithId(long id)
        {
            this.message.Id = id;
            return this;
        }

        /// <summary>
        /// Sets <see cref="PersistentModelWithExternalId.ExternalId"/>.
        /// </summary>
        /// <param name="externalId">The external id.</param>
        /// <returns>The updated <see cref="MessageBuilder"/>.</returns>
        public MessageBuilder WithExternalId(Guid externalId)
        {
            this.message.ExternalId = externalId;
            return this;
        }

        /// <summary>
        /// Sets <see cref="PersistentModel.AddedOn"/>.
        /// </summary>
        /// <param name="addedOn">Added on.</param>
        /// <returns>The updated <see cref="MessageBuilder"/>.</returns>
        public MessageBuilder WithAddedOn(DateTime addedOn)
        {
            this.message.AddedOn = addedOn;
            return this;
        }

        /// <summary>
        /// Sets <see cref="PersistentModel.EditedOn"/>.
        /// </summary>
        /// <param name="editedOn">Edited on.</param>
        /// <returns>The updated <see cref="MessageBuilder"/>.</returns>
        public MessageBuilder WithEditedOn(DateTime? editedOn)
        {
            this.message.EditedOn = editedOn;
            return this;
        }

        /// <summary>
        /// Sets <see cref="Message.Content"/>.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The updated <see cref="MessageBuilder"/>.</returns>
        public MessageBuilder WithContent(string content)
        {
            this.message.Content = content;
            return this;
        }
    }
}
