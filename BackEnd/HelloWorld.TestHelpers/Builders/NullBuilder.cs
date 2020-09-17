// <copyright file="NullBuilder.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.TestHelpers.Builders
{
    using System.Text.Json;

    /// <summary>
    /// Represents a builder for null.
    /// </summary>
    /// <remarks>
    /// When using nullable reference types, literal null values trigger warnings.
    /// This builder creates null values by means of deserialization.
    /// </remarks>
    public static class NullBuilder
    {
        /// <summary>
        /// Builds null of type <typeparamref name="TNull"/>.
        /// </summary>
        /// <typeparam name="TNull">The type.</typeparam>
        /// <returns>Null of type <typeparamref name="TNull"/>.</returns>
        public static TNull Build<TNull>()
        {
            return JsonSerializer.Deserialize<TNull>("null");
        }
    }
}
