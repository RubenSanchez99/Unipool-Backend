using Microsoft.EntityFrameworkCore.Migrations;

namespace UniPool.Migrations
{
    public partial class Trips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoordinatesLatitude",
                table: "Trips",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoordinatesLongitude",
                table: "Trips",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Trips",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentTrip",
                columns: table => new
                {
                    TripId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTrip", x => new { x.TripId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentTrip_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTrip_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentTrip_StudentId",
                table: "StudentTrip",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentTrip");

            migrationBuilder.DropColumn(
                name: "CoordinatesLatitude",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "CoordinatesLongitude",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Trips");
        }
    }
}
