// <copyright file="20200721122106_AddStringLengthAttribute.cs" company="dsnouck">
// Copyright (c) dsnouck. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </copyright>

namespace HelloWorld.Database.Migrations
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using HelloWorld.Models;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <summary>
    /// A <see cref="Migration"/> that adds a <see cref="StringLengthAttribute"/> to <see cref="Message.Content"/>.
    /// </summary>
    public partial class AddStringLengthAttribute : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Message",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Message",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 256);
        }
    }
}
