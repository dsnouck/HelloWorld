// <copyright file="MessagesController.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApi.Controllers
{
    using System;
    using System.Linq;
    using HelloWorld.Components.Interfaces;
    using HelloWorld.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// A controller for messages.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : Controller
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
        /// Gets all messages.
        /// </summary>
        /// <returns>All messages.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllMessages()
        {
            var allMessageViewModels = this.messageComponent
                .GetAllMessages()
                .Select(message => new MessageViewModel
                {
                    Id = message.Id,
                    Content = message.Content,
                })
                .ToList();

            return this.Ok(allMessageViewModels);
        }

        /// <summary>
        /// Adds a message.
        /// </summary>
        /// <param name="messageAddEditViewModel">A message.</param>
        /// <returns>The added message.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddMessage(MessageAddEditViewModel messageAddEditViewModel)
        {
            if (messageAddEditViewModel == null)
            {
                throw new ArgumentNullException(nameof(messageAddEditViewModel));
            }

            var message = new Message
            {
                Content = messageAddEditViewModel.Content,
            };

            this.messageComponent.AddMessage(message);

            var messageViewModel = new MessageViewModel
            {
                Id = message.Id,
                Content = message.Content,
            };

            var uri = new Uri($"{this.Request.Path}/{messageViewModel.Id}", UriKind.Relative);
            return this.Created(uri, messageViewModel);
        }

        /// <summary>
        /// Gets the message with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The message with the given <paramref name="id"/>.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMessage(Guid id)
        {
            var message = this.messageComponent.GetMessage(id);
            if (message == null)
            {
                return this.NotFound();
            }

            var messageViewModel = new MessageViewModel
            {
                Id = message.Id,
                Content = message.Content,
            };

            return this.Ok(messageViewModel);
        }

        /// <summary>
        /// Updates the message with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="messageAddEditViewModel">The message.</param>
        /// <returns>The updated message.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateMessage(Guid id, MessageAddEditViewModel messageAddEditViewModel)
        {
            if (messageAddEditViewModel == null)
            {
                throw new ArgumentNullException(nameof(messageAddEditViewModel));
            }

            var message = this.messageComponent.GetMessage(id);
            if (message == null)
            {
                return this.NotFound();
            }

            message.Content = messageAddEditViewModel.Content;

            this.messageComponent.UpdateMessage(message);

            var messageViewModel = new MessageViewModel
            {
                Id = message.Id,
                Content = message.Content,
            };

            return this.Ok(messageViewModel);
        }

        /// <summary>
        /// Removes the message with the given <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult RemoveMessage(Guid id)
        {
            var message = this.messageComponent.GetMessage(id);
            if (message == null)
            {
                return this.NotFound();
            }

            this.messageComponent.RemoveMessage(message);

            return this.NoContent();
        }
    }
}
