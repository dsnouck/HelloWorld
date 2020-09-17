// <copyright file="MessageProfileTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApiTests.Profiles
{
    using AutoMapper;
    using FluentAssertions;
    using HelloWorld.TestHelpers.Builders;
    using HelloWorld.ViewModels;
    using HelloWorld.WebApi.Profiles;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="MessageProfile"/>.
    /// </summary>
    public class MessageProfileTests
    {
        private readonly IMapper systemUnderTest;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageProfileTests"/> class.
        /// </summary>
        public MessageProfileTests()
        {
            this.systemUnderTest = new MapperConfiguration(configuration => configuration.AddProfile<MessageProfile>()).CreateMapper();
        }

        /// <summary>
        /// Tests <see cref="MessageProfile"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenMapMessageViewModelIsCalledThenTheMessageIsCorrectlyMapped()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            var messageViewModel = MessageViewModelBuilder
                .ABuilder()
                .WithId(message.Id)
                .WithContent(message.Content)
                .Build();

            // Act.
            var result = this.systemUnderTest.Map<MessageViewModel>(message);

            // Assert.
            result.Should().BeEquivalentTo(messageViewModel);
        }

        /// <summary>
        /// Tests <see cref="MessageProfile"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWhenMapIsCalledThenTheMessageIsCorrectlyUpdated()
        {
            // Arrange.
            var message = MessageBuilder.ABuilder().Build();
            var messageAddEditViewModel = MessageAddEditViewModelBuilder
                .ABuilder()
                .WithContent("Updated")
                .Build();
            var updatedMessage = MessageBuilder
                .ABuilder()
                .WithId(message.Id)
                .WithContent(messageAddEditViewModel.Content)
                .Build();

            // Act.
            this.systemUnderTest.Map(messageAddEditViewModel, message);

            // Assert.
            message.Should().BeEquivalentTo(updatedMessage);
        }
    }
}
