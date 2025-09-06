using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booket.Modules.UserManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_EnqueueDate_to_InternalCommands : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EnqueueDate",
                schema: "users",
                table: "InternalCommands",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnqueueDate",
                schema: "users",
                table: "InternalCommands");
        }
    }
}
