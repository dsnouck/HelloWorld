// <copyright file="MessageAddEditViewModelValidator.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApi.Validators
{
    using FluentValidation;
    using HelloWorld.ViewModels;

    /// <summary>
    /// Represents a validator for <see cref="MessageAddEditViewModel"/>s.
    /// </summary>
    public class MessageAddEditViewModelValidator : AbstractValidator<MessageAddEditViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageAddEditViewModelValidator"/> class.
        /// </summary>
        public MessageAddEditViewModelValidator()
        {
            this.RuleFor(message => message.Content)
                .NotEmpty()
                .MaximumLength(256);
        }
    }
}
