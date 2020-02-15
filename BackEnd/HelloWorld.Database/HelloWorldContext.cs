// <copyright file="HelloWorldContext.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Database
{
    using System;
    using HelloWorld.Entities;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents the context for the HelloWorld database.
    /// </summary>
    public class HelloWorldContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelloWorldContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public HelloWorldContext(DbContextOptions<HelloWorldContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public DbSet<Message>? Messages { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
