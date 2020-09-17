// <copyright file="MessageAddEditViewModelValidatorTests.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.WebApiTests.Validators
{
    using FluentAssertions;
    using HelloWorld.TestHelpers.Builders;
    using HelloWorld.WebApi.Validators;
    using Xunit;

    /// <summary>
    /// Provides tests for <see cref="MessageAddEditViewModelValidator"/>.
    /// </summary>
    public class MessageAddEditViewModelValidatorTests
    {
        private readonly MessageAddEditViewModelValidator systemUnderTest;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageAddEditViewModelValidatorTests"/> class.
        /// </summary>
        public MessageAddEditViewModelValidatorTests()
        {
            this.systemUnderTest = new MessageAddEditViewModelValidator();
        }

        /// <summary>
        /// Tests <see cref="MessageAddEditViewModelValidator"/>.
        /// </summary>
        [Fact]
        public void GivenAValidMessageWhenValidateIsCalledThenTheMessageIsFoundToBeValid()
        {
            // Arrange.
            var messageAddEditViewModel = MessageAddEditViewModelBuilder.ABuilder().Build();

            // Act.
            var result = this.systemUnderTest.Validate(messageAddEditViewModel);

            // Assert.
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        /// <summary>
        /// Tests <see cref="MessageAddEditViewModelValidator"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWithContentNullWhenValidateIsCalledThenTheMessageIsFoundToBeInvalid()
        {
            // Arrange.
            var content = NullBuilder.Build<string>();
            var messageAddEditViewModel = MessageAddEditViewModelBuilder
                .ABuilder()
                .WithContent(content)
                .Build();

            // Act.
            var result = this.systemUnderTest.Validate(messageAddEditViewModel);

            // Assert.
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
        }

        /// <summary>
        /// Tests <see cref="MessageAddEditViewModelValidator"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWithContentEmptyWhenValidateIsCalledThenTheMessageIsFoundToBeInvalid()
        {
            // Arrange.
            var messageAddEditViewModel = MessageAddEditViewModelBuilder
                .ABuilder()
                .WithContent(string.Empty)
                .Build();

            // Act.
            var result = this.systemUnderTest.Validate(messageAddEditViewModel);

            // Assert.
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
        }

        /// <summary>
        /// Tests <see cref="MessageAddEditViewModelValidator"/>.
        /// </summary>
        [Fact]
        public void GivenAMessageWithContentTooLongWhenValidateIsCalledThenTheMessageIsFoundToBeInvalid()
        {
            // Arrange.
            var messageAddEditViewModel = MessageAddEditViewModelBuilder
                .ABuilder()
                .WithContent(new string('x', 257))
                .Build();

            // Act.
            var result = this.systemUnderTest.Validate(messageAddEditViewModel);

            // Assert.
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
        }
    }
}
