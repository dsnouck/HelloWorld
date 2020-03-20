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
        /// Gets all <see cref="Message"/>s.
        /// </summary>
        /// <returns>All <see cref="Message"/>s.</returns>
        List<Message> GetAllMessages();

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
        Message GetMessage(Guid id);

        /// <summary>
        /// Updates the given <see cref="Message"/>.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The updated <see cref="Message"/>.</returns>
        Message UpdateMessage(Message message);

        /// <summary>
        /// Removes the given <see cref="Message"/>.
        /// </summary>
        /// <param name="message">The message.</param>
        void RemoveMessage(Message message);
    }
}
