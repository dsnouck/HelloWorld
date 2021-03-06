﻿// <copyright file="MessageViewModel.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.ViewModels
{
    using System;

    /// <summary>
    /// Represents a view model for a message.
    /// </summary>
    public class MessageViewModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets when this was added.
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Gets or sets when this was edited.
        /// </summary>
        public DateTime? EditedOn { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content { get; set; } = string.Empty;
    }
}
