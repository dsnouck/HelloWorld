// <copyright file="MessagesController.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApi.Controllers
{
    using System.Collections.Generic;
    using HelloWorld.Components.Interfaces;
    using HelloWorld.Entities;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// A controller for <see cref="Message"/>s.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MessagesController
    {
        private readonly IMessageComponent messageComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessagesController"/> class.
        /// </summary>
        /// <param name="messageComponent">An <see cref="IMessageComponent"/>.</param>
        public MessagesController(
            IMessageComponent messageComponent)
        {
            this.messageComponent = messageComponent;
        }

        /// <summary>
        /// Gets all <see cref="Message"/>s.
        /// </summary>
        /// <returns>All <see cref="Message"/>s.</returns>
        [HttpGet]
        public List<Message> GetAllMessages()
        {
            return this.messageComponent.GetAllMessages();
        }
    }
}
