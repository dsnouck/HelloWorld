// <copyright file="MessageAddEditViewModel.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Models
{
    using System;

    /// <summary>
    /// Represents a view model for adding or editing a message.
    /// </summary>
    public class MessageAddEditViewModel
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content { get; set; } = string.Empty;
    }
}
