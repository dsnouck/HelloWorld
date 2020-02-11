// <copyright file="WeatherForecast.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApi
{
    using System;

    /// <summary>
    /// Represents a weather forecast.
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the temperature in degrees Celsius.
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Gets the temperature in degrees Fahernheit.
        /// </summary>
        public int TemperatureF => 32 + (int)(this.TemperatureC / 0.5556);

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        public string Summary { get; set; } = string.Empty;
    }
}
