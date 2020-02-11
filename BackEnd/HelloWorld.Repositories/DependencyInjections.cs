// <copyright file="DependencyInjections.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Repositories
{
    using HelloWorld.Repositories.Implementations;
    using HelloWorld.Repositories.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Provides dependency injections for repositories.
    /// </summary>
    public static class DependencyInjections
    {
        /// <summary>
        /// Adds repositories to <paramref name="serviceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">The original <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> with repositories.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMessageRepository, MessageRepository>();

            return serviceCollection;
        }
    }
}
