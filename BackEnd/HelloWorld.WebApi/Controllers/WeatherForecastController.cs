// <copyright file="WeatherForecastController.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The first controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
        };

        private readonly string getCalledMessage = $"{nameof(Get)} called.";
        private readonly ILogger<WeatherForecastController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
        /// </summary>
        /// <param name="logger">An <see cref="ILogger{WeatherForecastController}"/>.</param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Gets all <see cref="WeatherForecast"/>s.
        /// </summary>
        /// <returns>All <see cref="WeatherForecast"/>s.</returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            this.logger.LogInformation(this.getCalledMessage);
            var random = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = random.Next(-20, 55),
                Summary = Summaries[random.Next(Summaries.Length)],
            })
            .ToArray();
        }
    }
}
