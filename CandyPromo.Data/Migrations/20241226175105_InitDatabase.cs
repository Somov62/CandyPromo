using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandyPromo.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: true),
                    Password = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Promocodes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    PrizeId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocodes", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Promocodes_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Prizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PromocodeId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prizes_Promocodes_PromocodeId",
                        column: x => x.PromocodeId,
                        principalTable: "Promocodes",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prizes_PromocodeId",
                table: "Prizes",
                column: "PromocodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promocodes_OwnerId",
                table: "Promocodes",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prizes");

            migrationBuilder.DropTable(
                name: "Promocodes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
