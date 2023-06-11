using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations.OutboxDb
{
    /// <inheritdoc />
    public partial class InitialOutBox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Outbox");

            migrationBuilder.CreateTable(
                name: "Url",
                schema: "Outbox",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Hits = table.Column<int>(type: "integer", nullable: false),
                    OriginalUrl = table.Column<string>(type: "varchar(255)", nullable: false),
                    ShortUrl = table.Column<string>(type: "varchar(30)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Url", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Url_Id",
                schema: "Outbox",
                table: "Url",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Url_ShortUrl",
                schema: "Outbox",
                table: "Url",
                column: "ShortUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Url",
                schema: "Outbox");
        }
    }
}
