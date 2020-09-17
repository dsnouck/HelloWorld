// <copyright file="MessageAddEditViewModelBuilder.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.TestHelpers.Builders
{
    using HelloWorld.ViewModels;

    /// <summary>
    /// Represents a builder for <see cref="MessageAddEditViewModel"/>s.
    /// </summary>
    public class MessageAddEditViewModelBuilder
    {
        private readonly MessageAddEditViewModel messageAddEditViewModel;

        private MessageAddEditViewModelBuilder()
        {
            this.messageAddEditViewModel = new MessageAddEditViewModel
            {
                Content = "Hello, world!",
            };
        }

        /// <summary>
        /// Creates a new <see cref="MessageAddEditViewModelBuilder"/>.
        /// </summary>
        /// <returns>A new <see cref="MessageAddEditViewModelBuilder"/>.</returns>
        public static MessageAddEditViewModelBuilder ABuilder()
        {
            return new MessageAddEditViewModelBuilder();
        }

        /// <summary>
        /// Builds the <see cref="MessageAddEditViewModel"/>.
        /// </summary>
        /// <returns>The <see cref="MessageAddEditViewModel"/>.</returns>
        public MessageAddEditViewModel Build()
        {
            return this.messageAddEditViewModel;
        }

        /// <summary>
        /// Sets <see cref="MessageAddEditViewModel.Content"/>.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>The updated <see cref="MessageAddEditViewModelBuilder"/>.</returns>
        public MessageAddEditViewModelBuilder WithContent(string content)
        {
            this.messageAddEditViewModel.Content = content;
            return this;
        }
    }
}
