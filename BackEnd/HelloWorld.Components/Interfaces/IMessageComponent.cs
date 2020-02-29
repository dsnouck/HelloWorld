﻿// <copyright file="IMessageComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Components.Interfaces
{
    using System.Collections.Generic;
    using HelloWorld.Entities;

    /// <summary>
    /// Provides operations concerning <see cref="Message"/>s.
    /// </summary>
    public interface IMessageComponent
    {
        /// <summary>
        /// Adds a <see cref="Message"/>.
        /// </summary>
        /// <param name="message">A <see cref="Message"/>.</param>
        /// <returns>The added <see cref="Message"/>.</returns>
        Message AddMessage(Message message);

        /// <summary>
        /// Gets all <see cref="Message"/>s.
        /// </summary>
        /// <returns>All <see cref="Message"/>s.</returns>
        List<Message> GetAllMessages();

        /// <summary>
        /// Gets the <see cref="Message"/> with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The <see cref="Message"/> with the given <paramref name="id"/>.</returns>
        Message GetMessage(long id);
    }
}
