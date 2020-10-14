// <copyright file="MessageProfile.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApi.Profiles
{
    using AutoMapper;
    using HelloWorld.Models;
    using HelloWorld.ViewModels;

    /// <inheritdoc/>
    public class MessageProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageProfile"/> class.
        /// </summary>
        public MessageProfile()
        {
            // Externally, the external id is known as just the id.
            this.CreateMap<Message, MessageViewModel>()
                .ForMember(messageViewModel => messageViewModel.Id, options => options.MapFrom(message => message.ExternalId));
            this.CreateMap<MessageAddEditViewModel, Message>();
        }
    }
}
