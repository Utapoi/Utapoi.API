using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karaoke.Infrastructure.Persistence.Migrations.KaraokeDb
{
    /// <inheritdoc />
    public partial class UpdateSingerLocalizedStringsRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalizedStringSinger");

            migrationBuilder.DropTable(
                name: "LocalizedStringSinger1");

            migrationBuilder.DropTable(
                name: "LocalizedStringSinger2");

            migrationBuilder.DropTable(
                name: "LocalizedStringSinger3");

            migrationBuilder.CreateTable(
                name: "SingerActivitiesLocalizedString",
                columns: table => new
                {
                    ActivitiesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Singer3Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingerActivitiesLocalizedString", x => new { x.ActivitiesId, x.Singer3Id });
                    table.ForeignKey(
                        name: "FK_SingerActivitiesLocalizedString_LocalizedStrings_ActivitiesId",
                        column: x => x.ActivitiesId,
                        principalTable: "LocalizedStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SingerActivitiesLocalizedString_Singers_Singer3Id",
                        column: x => x.Singer3Id,
                        principalTable: "Singers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingerDescriptionsLocalizedString",
                columns: table => new
                {
                    DescriptionsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Singer2Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingerDescriptionsLocalizedString", x => new { x.DescriptionsId, x.Singer2Id });
                    table.ForeignKey(
                        name: "FK_SingerDescriptionsLocalizedString_LocalizedStrings_DescriptionsId",
                        column: x => x.DescriptionsId,
                        principalTable: "LocalizedStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SingerDescriptionsLocalizedString_Singers_Singer2Id",
                        column: x => x.Singer2Id,
                        principalTable: "Singers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingerNamesLocalizedString",
                columns: table => new
                {
                    NamesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SingerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingerNamesLocalizedString", x => new { x.NamesId, x.SingerId });
                    table.ForeignKey(
                        name: "FK_SingerNamesLocalizedString_LocalizedStrings_NamesId",
                        column: x => x.NamesId,
                        principalTable: "LocalizedStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SingerNamesLocalizedString_Singers_SingerId",
                        column: x => x.SingerId,
                        principalTable: "Singers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingerNicknamesLocalizedString",
                columns: table => new
                {
                    NicknamesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Singer1Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingerNicknamesLocalizedString", x => new { x.NicknamesId, x.Singer1Id });
                    table.ForeignKey(
                        name: "FK_SingerNicknamesLocalizedString_LocalizedStrings_NicknamesId",
                        column: x => x.NicknamesId,
                        principalTable: "LocalizedStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SingerNicknamesLocalizedString_Singers_Singer1Id",
                        column: x => x.Singer1Id,
                        principalTable: "Singers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SingerActivitiesLocalizedString_Singer3Id",
                table: "SingerActivitiesLocalizedString",
                column: "Singer3Id");

            migrationBuilder.CreateIndex(
                name: "IX_SingerDescriptionsLocalizedString_Singer2Id",
                table: "SingerDescriptionsLocalizedString",
                column: "Singer2Id");

            migrationBuilder.CreateIndex(
                name: "IX_SingerNamesLocalizedString_SingerId",
                table: "SingerNamesLocalizedString",
                column: "SingerId");

            migrationBuilder.CreateIndex(
                name: "IX_SingerNicknamesLocalizedString_Singer1Id",
                table: "SingerNicknamesLocalizedString",
                column: "Singer1Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SingerActivitiesLocalizedString");

            migrationBuilder.DropTable(
                name: "SingerDescriptionsLocalizedString");

            migrationBuilder.DropTable(
                name: "SingerNamesLocalizedString");

            migrationBuilder.DropTable(
                name: "SingerNicknamesLocalizedString");

            migrationBuilder.CreateTable(
                name: "LocalizedStringSinger",
                columns: table => new
                {
                    NamesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SingerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedStringSinger", x => new { x.NamesId, x.SingerId });
                    table.ForeignKey(
                        name: "FK_LocalizedStringSinger_LocalizedStrings_NamesId",
                        column: x => x.NamesId,
                        principalTable: "LocalizedStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocalizedStringSinger_Singers_SingerId",
                        column: x => x.SingerId,
                        principalTable: "Singers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalizedStringSinger1",
                columns: table => new
                {
                    NicknamesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Singer1Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedStringSinger1", x => new { x.NicknamesId, x.Singer1Id });
                    table.ForeignKey(
                        name: "FK_LocalizedStringSinger1_LocalizedStrings_NicknamesId",
                        column: x => x.NicknamesId,
                        principalTable: "LocalizedStrings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocalizedStringSinger1_Singers_Singer1Id",
                        column: x => x.Singer1Id,
                        principalTable: "Singers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        name: "FK_LocalizedStringSinger2_LocalizedStrings_DescriptionsId",
                        column: x => x.DescriptionsId,
                        principalTable: "LocalizedStrings",
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
                        name: "FK_LocalizedStringSinger3_LocalizedStrings_ActivitiesId",
                        column: x => x.ActivitiesId,
                        principalTable: "LocalizedStrings",
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
                name: "IX_LocalizedStringSinger_SingerId",
                table: "LocalizedStringSinger",
                column: "SingerId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStringSinger1_Singer1Id",
                table: "LocalizedStringSinger1",
                column: "Singer1Id");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStringSinger2_Singer2Id",
                table: "LocalizedStringSinger2",
                column: "Singer2Id");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStringSinger3_Singer3Id",
                table: "LocalizedStringSinger3",
                column: "Singer3Id");
        }
    }
}
