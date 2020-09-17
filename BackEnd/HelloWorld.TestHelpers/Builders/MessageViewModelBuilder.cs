// <copyright file="MessageViewModelBuilder.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.TestHelpers.Builders
{
    using System;
    using HelloWorld.ViewModels;

    /// <summary>
    /// Represents a builder for <see cref="MessageViewModel"/>s.
    /// </summary>
    public class MessageViewModelBuilder
    {
        private readonly MessageViewModel messageViewModel;

        private MessageViewModelBuilder()
        {
            this.messageViewModel = new MessageViewModel
            {
                Id = Guid.NewGuid(),
                Content = "Hello, world!",
            };
        }

        /// <summary>
        /// Creates a new <see cref="MessageViewModelBuilder"/>.
        /// </summary>
        /// <returns>A new <see cref="MessageViewModelBuilder"/>.</returns>
        public static MessageViewModelBuilder ABuilder()
        {
            return new MessageViewModelBuilder();
        }

        /// <summary>
        /// Builds the <see cref="MessageViewModel"/>.
        /// </summary>
        /// <returns>The <see cref="MessageViewModel"/>.</returns>
        public MessageViewModel Build()
        {
            return this.messageViewModel;
        }

        /// <summary>
        /// Sets <see cref="MessageViewModel.Id"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The updated <see cref="MessageViewModelBuilder"/>.</returns>
        public MessageViewModelBuilder WithId(Guid id)
        {
            this.messageViewModel.Id = id;
            return this;
        }

        /// <summary>
        /// Sets <see cref="MessageViewModel.Content"/>.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The updated <see cref="MessageViewModelBuilder"/>.</returns>
        public MessageViewModelBuilder WithContent(string content)
        {
            this.messageViewModel.Content = content;
            return this;
        }
    }
}
