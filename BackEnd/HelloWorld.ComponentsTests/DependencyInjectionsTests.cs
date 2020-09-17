// <copyright file="DependencyInjectionsTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.ComponentsTests
{
    using System.Linq;
    using FluentAssertions;
    using HelloWorld.Components;
    using HelloWorld.Components.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="DependencyInjections"/>.
    /// </summary>
    public class DependencyInjectionsTests
    {
        /// <summary>
        /// Tests <see cref="DependencyInjections.AddComponents(IServiceCollection)"/>.
        /// </summary>
        [Fact]
        public void GivenAServiceCollectionWhenAddComponentsIsCalledThenMessageComponentIsAdded()
        {
            // Arrange.
            var serviceCollection = new ServiceCollection();

            // Act.
            var result = serviceCollection.AddComponents();

            // Assert.
            var messageComponentServiceDescriptor = result.SingleOrDefault(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IMessageComponent));
            messageComponentServiceDescriptor.Should().NotBeNull();
        }
    }
}
