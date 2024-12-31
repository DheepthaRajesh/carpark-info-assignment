using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPark_Info.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarParks",
                columns: table => new
                {
                    car_park_no = table.Column<string>(type: "TEXT", nullable: false),
                    address = table.Column<string>(type: "TEXT", nullable: false),
                    x_coord = table.Column<double>(type: "REAL", nullable: false),
                    y_coord = table.Column<double>(type: "REAL", nullable: false),
                    car_park_type = table.Column<string>(type: "TEXT", nullable: false),
                    type_of_parking_system = table.Column<string>(type: "TEXT", nullable: false),
                    short_term_parking = table.Column<string>(type: "TEXT", nullable: false),
                    free_parking = table.Column<string>(type: "TEXT", nullable: false),
                    night_parking = table.Column<bool>(type: "INTEGER", nullable: false),
                    car_park_decks = table.Column<int>(type: "INTEGER", nullable: false),
                    gantry_height = table.Column<double>(type: "REAL", nullable: false),
                    car_park_basement = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarParks", x => x.car_park_no);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    username = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    car_park_no = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => new { x.UserId, x.car_park_no });
                    table.ForeignKey(
                        name: "FK_Favourites_CarParks_car_park_no",
                        column: x => x.car_park_no,
                        principalTable: "CarParks",
                        principalColumn: "car_park_no",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favourites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_car_park_no",
                table: "Favourites",
                column: "car_park_no");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropTable(
                name: "CarParks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
