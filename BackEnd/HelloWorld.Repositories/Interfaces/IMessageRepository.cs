// <copyright file="IMessageRepository.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Repositories.Interfaces
{
    using System;
    using System.Collections.Generic;
    using HelloWorld.Models;

    /// <summary>
    /// Provides repository operations concerning <see cref="Message"/>s.
    /// </summary>
    public interface IMessageRepository
    {
        /// <summary>
        /// Gets <see cref="Message"/>s.
        /// </summary>
        /// <returns><see cref="Message"/>s.</returns>
        List<Message> GetMessages();

        /// <summary>
        /// Adds a <see cref="Message"/>.
        /// </summary>
        /// <param name="message">A <see cref="Message"/>.</param>
        /// <returns>The added <see cref="Message"/>.</returns>
        Message AddMessage(Message message);

        /// <summary>
        /// Gets the <see cref="Message"/> with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The <see cref="Message"/> with the given <paramref name="id"/>.</returns>
        Message GetMessage(long id);

        /// <summary>
        /// Gets the <see cref="Message"/> with the given <paramref name="externalId"/>.
        /// </summary>
        /// <param name="externalId">The external id.</param>
        /// <returns>The <see cref="Message"/> with the given <paramref name="externalId"/>.</returns>
        Message GetMessage(Guid externalId);

        /// <summary>
        /// Edits the given <see cref="Message"/>.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The edited <see cref="Message"/>.</returns>
        Message EditMessage(Message message);

        /// <summary>
        /// Removes the given <see cref="Message"/>.
        /// </summary>
        /// <param name="message">The message.</param>
        void RemoveMessage(Message message);
    }
}
