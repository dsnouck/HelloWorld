// <copyright file="DependencyInjections.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Components
{
    using HelloWorld.Components.Implementations;
    using HelloWorld.Components.Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Provides dependency injections for components.
    /// </summary>
    public static class DependencyInjections
    {
        /// <summary>
        /// Adds components to <paramref name="serviceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">The original <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> with components.</returns>
        public static IServiceCollection AddComponents(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IMessageComponent, MessageComponent>();

            return serviceCollection;
        }
    }
}
