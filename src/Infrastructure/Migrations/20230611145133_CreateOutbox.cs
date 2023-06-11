using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateOutbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OutBoxMessage",
                schema: "ShortUrl",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "varchar(100)", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    OccurredOn = table.Column<DateTime>(type: "timestamp", nullable: false),
                    ProcessedOn = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Error = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutBoxMessage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutBoxMessage_Id",
                schema: "ShortUrl",
                table: "OutBoxMessage",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutBoxMessage",
                schema: "ShortUrl");
        }
    }
}
