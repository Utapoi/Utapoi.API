using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karaoke.Infrastructure.Persistence.Migrations.KaraokeDb
{
    /// <inheritdoc />
    public partial class UpdateSingerFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "Singers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "Singers",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Singers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "Singers");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Singers");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Singers");
        }
    }
}
