﻿// <copyright file="IMessageComponent.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Components
{
    using System.Collections.Generic;
    using HelloWorld.Entities;

    /// <summary>
    /// Provides operations concerning <see cref="Message"/>s.
    /// </summary>
    public interface IMessageComponent
    {
        /// <summary>
        /// Gets all <see cref="Message"/>s.
        /// </summary>
        /// <returns>All <see cref="Message"/>s.</returns>
        List<Message> GetAllMessages();
    }
}
