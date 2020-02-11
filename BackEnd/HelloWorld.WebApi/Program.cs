// <copyright file="Program.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApi
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Contains the main entry point.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public static void Main(string[] arguments)
        {
            CreateHostBuilder(arguments).Build().Run();
        }

        /// <summary>
        /// Creates an <see cref="IHostBuilder"/> for the given arguments.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>An <see cref="IHostBuilder"/> for the given arguments.</returns>
        public static IHostBuilder CreateHostBuilder(string[] arguments)
        {
            return Host.CreateDefaultBuilder(arguments)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
