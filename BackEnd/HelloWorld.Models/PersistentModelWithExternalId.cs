// <copyright file="PersistentModelWithExternalId.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Models
{
    using System;

    /// <summary>
    /// Represents a model that is persisted in the database with an external id.
    /// </summary>
    public abstract class PersistentModelWithExternalId : PersistentModel
    {
        /// <summary>
        /// Gets or sets the external id.
        /// </summary>
        public Guid ExternalId { get; set; }
    }
}
