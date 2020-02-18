// <copyright file="InMemoryWebApplicationFactory.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApiIntegrationTests
{
    using System;
    using System.Linq;
    using HelloWorld.Database;
    using HelloWorld.Entities;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// A custom <see cref="WebApplicationFactory{TEntryPoint}"/> that uses an in memory database.
    /// </summary>
    /// <typeparam name="TStartup">The type of startup class.</typeparam>
    public class InMemoryWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        /// <inheritdoc/>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ConfigureServices(serviceCollection =>
            {
                var serviceDescriptor = serviceCollection
                    .SingleOrDefault(serviceDescriptor => serviceDescriptor.ServiceType == typeof(DbContextOptions<HelloWorldContext>));
                if (serviceDescriptor != null)
                {
                    serviceCollection.Remove(serviceDescriptor);
                }

                serviceCollection.AddDbContext<HelloWorldContext>(options =>
                    options.UseInMemoryDatabase("HelloWorld"));

                var serviceProvider = serviceCollection.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var scopedServiceProvider = scope.ServiceProvider;
                var helloWorldContext = scopedServiceProvider.GetRequiredService<HelloWorldContext>();
                helloWorldContext.Database.EnsureCreated();
                helloWorldContext.Messages?.Add(new Message { Content = "Hello, world!" });
                helloWorldContext.SaveChanges();
            });
        }
    }
}