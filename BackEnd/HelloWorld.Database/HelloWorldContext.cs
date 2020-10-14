// <copyright file="HelloWorldContext.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Database
{
    using System;
    using HelloWorld.Models;
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
        public HelloWorldContext(
            DbContextOptions<HelloWorldContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        public DbSet<Message> Messages => this.Set<Message>();

        /// <inheritdoc/>
        public override int SaveChanges()
        {
            if (this.ChangeTracker == null)
            {
                throw new InvalidOperationException($"{nameof(this.ChangeTracker)} is null.");
            }

            var now = DateTime.UtcNow;

            foreach (var entry in this.ChangeTracker.Entries<PersistentModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.AddedOn = now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.EditedOn = now;
                        break;
                }
            }

            foreach (var entry in this.ChangeTracker.Entries<PersistentModelWithExternalId>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.ExternalId = Guid.NewGuid();
                        break;
                }
            }

            return base.SaveChanges();
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            // Use singular table names.
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }

            modelBuilder.Entity<Message>().HasIndex(message => message.ExternalId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
