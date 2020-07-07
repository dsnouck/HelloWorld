// <copyright file="MessageProfile.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApi.Profiles
{
    using AutoMapper;
    using HelloWorld.Models;

    /// <inheritdoc/>
    public class MessageProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageProfile"/> class.
        /// </summary>
        public MessageProfile()
        {
            this.CreateMap<Message, MessageViewModel>();
            this.CreateMap<MessageAddEditViewModel, Message>();
        }
    }
}
