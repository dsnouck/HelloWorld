// <copyright file="DependencyInjectionsTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.RepositoriesTests
{
    using System.Linq;
    using FluentAssertions;
    using HelloWorld.Repositories;
    using HelloWorld.Repositories.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="DependencyInjections"/>.
    /// </summary>
    public class DependencyInjectionsTests
    {
        /// <summary>
        /// Tests <see cref="DependencyInjections.AddRepositories(IServiceCollection)"/>.
        /// </summary>
        [Fact]
        public void GivenAServiceCollectionWhenAddRepositoriesIsCalledThenMessageRepositoryIsAdded()
        {
            // Arrange.
            var serviceCollection = new ServiceCollection();

            // Act.
            var result = serviceCollection.AddRepositories();

            // Assert.
            var messageComponentServiceDescriptor = result.SingleOrDefault(serviceDescriptor => serviceDescriptor.ServiceType == typeof(IMessageRepository));
            messageComponentServiceDescriptor.Should().NotBeNull();
        }
    }
}
