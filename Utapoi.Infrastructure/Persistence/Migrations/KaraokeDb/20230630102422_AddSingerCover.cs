#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Utapoi.Infrastructure.Persistence.Migrations.KaraokeDb
{
    /// <inheritdoc />
    public partial class AddSingerCover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ProfilePictureId",
                table: "Singers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "CoverId",
                table: "Singers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Singers_CoverId",
                table: "Singers",
                column: "CoverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Singers_Files_CoverId",
                table: "Singers",
                column: "CoverId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Singers_Files_CoverId",
                table: "Singers");

            migrationBuilder.DropIndex(
                name: "IX_Singers_CoverId",
                table: "Singers");

            migrationBuilder.DropColumn(
                name: "CoverId",
                table: "Singers");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfilePictureId",
                table: "Singers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
