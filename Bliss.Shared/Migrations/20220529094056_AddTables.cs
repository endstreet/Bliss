using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bliss.Shared.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cruises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ActiveWaypoint = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cruises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WayPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CruiseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Sequence = table.Column<int>(type: "INTEGER", nullable: false),
                    StartLat = table.Column<double>(type: "REAL", nullable: false),
                    StartLng = table.Column<double>(type: "REAL", nullable: false),
                    EndLat = table.Column<double>(type: "REAL", nullable: false),
                    EndLng = table.Column<double>(type: "REAL", nullable: false),
                    Departure = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Arrival = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WayPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WayPoints_Cruises_CruiseId",
                        column: x => x.CruiseId,
                        principalTable: "Cruises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WayPoints_CruiseId",
                table: "WayPoints",
                column: "CruiseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WayPoints");

            migrationBuilder.DropTable(
                name: "Cruises");
        }
    }
}
