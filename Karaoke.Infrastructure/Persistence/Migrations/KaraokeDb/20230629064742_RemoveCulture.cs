using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karaoke.Infrastructure.Persistence.Migrations.KaraokeDb
{
    /// <inheritdoc />
    public partial class RemoveCulture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedString_Singers_SingerId",
                table: "LocalizedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedString_Singers_SingerId1",
                table: "LocalizedString");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Cultures_OriginalLanguageId",
                table: "Songs");

            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Files_OriginalFileId",
                table: "Songs");

            migrationBuilder.DropTable(
                name: "Cultures");

            migrationBuilder.DropIndex(
                name: "IX_Songs_OriginalLanguageId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_LocalizedString_SingerId",
                table: "LocalizedString");

            migrationBuilder.DropIndex(
                name: "IX_LocalizedString_SingerId1",
                table: "LocalizedString");

            migrationBuilder.DropColumn(
                name: "SingerId",
                table: "LocalizedString");

            migrationBuilder.DropColumn(
                name: "SingerId1",
                table: "LocalizedString");

            migrationBuilder.RenameColumn(
                name: "OriginalLanguageId",
                table: "Songs",
                newName: "OriginalLanguage");

            migrationBuilder.CreateTable(
                name: "LocalizedStringSinger2",
                columns: table => new
                {
                    DescriptionsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Singer2Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedStringSinger2", x => new { x.DescriptionsId, x.Singer2Id });
                    table.ForeignKey(
                        name: "FK_LocalizedStringSinger2_LocalizedString_DescriptionsId",
                        column: x => x.DescriptionsId,
                        principalTable: "LocalizedString",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocalizedStringSinger2_Singers_Singer2Id",
                        column: x => x.Singer2Id,
                        principalTable: "Singers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalizedStringSinger3",
                columns: table => new
                {
                    ActivitiesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Singer3Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedStringSinger3", x => new { x.ActivitiesId, x.Singer3Id });
                    table.ForeignKey(
                        name: "FK_LocalizedStringSinger3_LocalizedString_ActivitiesId",
                        column: x => x.ActivitiesId,
                        principalTable: "LocalizedString",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocalizedStringSinger3_Singers_Singer3Id",
                        column: x => x.Singer3Id,
                        principalTable: "Singers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStringSinger2_Singer2Id",
                table: "LocalizedStringSinger2",
                column: "Singer2Id");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStringSinger3_Singer3Id",
                table: "LocalizedStringSinger3",
                column: "Singer3Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Files_OriginalFileId",
                table: "Songs",
                column: "OriginalFileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Files_OriginalFileId",
                table: "Songs");

            migrationBuilder.DropTable(
                name: "LocalizedStringSinger2");

            migrationBuilder.DropTable(
                name: "LocalizedStringSinger3");

            migrationBuilder.RenameColumn(
                name: "OriginalLanguage",
                table: "Songs",
                newName: "OriginalLanguageId");

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

            migrationBuilder.CreateTable(
                name: "Cultures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Tag = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cultures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cultures_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Songs_OriginalLanguageId",
                table: "Songs",
                column: "OriginalLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedString_SingerId",
                table: "LocalizedString",
                column: "SingerId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedString_SingerId1",
                table: "LocalizedString",
                column: "SingerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cultures_UserId",
                table: "Cultures",
                column: "UserId");

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
                name: "FK_Songs_Cultures_OriginalLanguageId",
                table: "Songs",
                column: "OriginalLanguageId",
                principalTable: "Cultures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Files_OriginalFileId",
                table: "Songs",
                column: "OriginalFileId",
                principalTable: "Files",
                principalColumn: "Id");
        }
    }
}
