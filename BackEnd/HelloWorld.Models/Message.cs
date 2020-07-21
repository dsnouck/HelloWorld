// <copyright file="Message.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Represents a message.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [Required]
        [StringLength(256, MinimumLength = 1)]
        public string Content { get; set; } = string.Empty;
    }
}
