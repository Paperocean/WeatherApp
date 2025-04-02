using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeatherApi.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Temp = table.Column<double>(type: "REAL", nullable: false),
                    Feels_like = table.Column<double>(type: "REAL", nullable: false),
                    Temp_min = table.Column<double>(type: "REAL", nullable: false),
                    Temp_max = table.Column<double>(type: "REAL", nullable: false),
                    Pressure = table.Column<int>(type: "INTEGER", nullable: false),
                    Humidity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherDatas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WeatherDatas",
                columns: new[] { "Id", "Feels_like", "Humidity", "Name", "Pressure", "Temp", "Temp_max", "Temp_min" },
                values: new object[,]
                {
                    { 1, 15.0, 70, "Wrocław", 1013, 15.5, 18.0, 10.0 },
                    { 2, 12.0, 75, "Warszawa", 1015, 12.5, 15.0, 8.0 },
                    { 3, 14.0, 72, "Kraków", 1012, 14.5, 17.0, 9.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherDatas");
        }
    }
}
