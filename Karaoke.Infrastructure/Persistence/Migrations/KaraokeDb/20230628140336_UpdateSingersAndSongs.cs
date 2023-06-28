using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karaoke.Infrastructure.Persistence.Migrations.KaraokeDb
{
    /// <inheritdoc />
    public partial class UpdateSingersAndSongs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OriginalFileId",
                table: "Songs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SingerId",
                table: "LocalizedString",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SingerId1",
                table: "LocalizedString",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_OriginalFileId",
                table: "Songs",
                column: "OriginalFileId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedString_SingerId",
                table: "LocalizedString",
                column: "SingerId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedString_SingerId1",
                table: "LocalizedString",
                column: "SingerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedString_Singers_SingerId",
                table: "LocalizedString",
                column: "SingerId",
                principalTable: "Singers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedString_Singers_SingerId1",
                table: "LocalizedString",
                column: "SingerId1",
                principalTable: "Singers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Files_OriginalFileId",
                table: "Songs",
                column: "OriginalFileId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedString_Singers_SingerId",
                table: "LocalizedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedString_Singers_SingerId1",
                table: "LocalizedString");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Files_OriginalFileId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_OriginalFileId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_LocalizedString_SingerId",
                table: "LocalizedString");

            migrationBuilder.DropIndex(
                name: "IX_LocalizedString_SingerId1",
                table: "LocalizedString");

            migrationBuilder.DropColumn(
                name: "OriginalFileId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "SingerId",
                table: "LocalizedString");

            migrationBuilder.DropColumn(
                name: "SingerId1",
                table: "LocalizedString");
        }
    }
}
