using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karaoke.Infrastructure.Persistence.Migrations.KaraokeDb
{
    /// <inheritdoc />
    public partial class AddLocalizedStrings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedString_Albums_AlbumId",
                table: "LocalizedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedString_Collection_CollectionId",
                table: "LocalizedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedString_Composer_ComposerId",
                table: "LocalizedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedString_Composer_ComposerId1",
                table: "LocalizedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedString_SongWriter_SongWriterId",
                table: "LocalizedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedString_SongWriter_SongWriterId1",
                table: "LocalizedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedString_Songs_SongId",
                table: "LocalizedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedString_Work_WorkId",
                table: "LocalizedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedString_Work_WorkId1",
                table: "LocalizedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStringSinger_LocalizedString_NamesId",
                table: "LocalizedStringSinger");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStringSinger1_LocalizedString_NicknamesId",
                table: "LocalizedStringSinger1");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStringSinger2_LocalizedString_DescriptionsId",
                table: "LocalizedStringSinger2");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStringSinger3_LocalizedString_ActivitiesId",
                table: "LocalizedStringSinger3");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocalizedString",
                table: "LocalizedString");

            migrationBuilder.RenameTable(
                name: "LocalizedString",
                newName: "LocalizedStrings");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedString_WorkId1",
                table: "LocalizedStrings",
                newName: "IX_LocalizedStrings_WorkId1");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedString_WorkId",
                table: "LocalizedStrings",
                newName: "IX_LocalizedStrings_WorkId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedString_SongWriterId1",
                table: "LocalizedStrings",
                newName: "IX_LocalizedStrings_SongWriterId1");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedString_SongWriterId",
                table: "LocalizedStrings",
                newName: "IX_LocalizedStrings_SongWriterId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedString_SongId",
                table: "LocalizedStrings",
                newName: "IX_LocalizedStrings_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedString_ComposerId1",
                table: "LocalizedStrings",
                newName: "IX_LocalizedStrings_ComposerId1");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedString_ComposerId",
                table: "LocalizedStrings",
                newName: "IX_LocalizedStrings_ComposerId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedString_CollectionId",
                table: "LocalizedStrings",
                newName: "IX_LocalizedStrings_CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedString_AlbumId",
                table: "LocalizedStrings",
                newName: "IX_LocalizedStrings_AlbumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocalizedStrings",
                table: "LocalizedStrings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStrings_Albums_AlbumId",
                table: "LocalizedStrings",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStrings_Collection_CollectionId",
                table: "LocalizedStrings",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStrings_Composer_ComposerId",
                table: "LocalizedStrings",
                column: "ComposerId",
                principalTable: "Composer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStrings_Composer_ComposerId1",
                table: "LocalizedStrings",
                column: "ComposerId1",
                principalTable: "Composer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStrings_SongWriter_SongWriterId",
                table: "LocalizedStrings",
                column: "SongWriterId",
                principalTable: "SongWriter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStrings_SongWriter_SongWriterId1",
                table: "LocalizedStrings",
                column: "SongWriterId1",
                principalTable: "SongWriter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStrings_Songs_SongId",
                table: "LocalizedStrings",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStrings_Work_WorkId",
                table: "LocalizedStrings",
                column: "WorkId",
                principalTable: "Work",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStrings_Work_WorkId1",
                table: "LocalizedStrings",
                column: "WorkId1",
                principalTable: "Work",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStringSinger_LocalizedStrings_NamesId",
                table: "LocalizedStringSinger",
                column: "NamesId",
                principalTable: "LocalizedStrings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStringSinger1_LocalizedStrings_NicknamesId",
                table: "LocalizedStringSinger1",
                column: "NicknamesId",
                principalTable: "LocalizedStrings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStringSinger2_LocalizedStrings_DescriptionsId",
                table: "LocalizedStringSinger2",
                column: "DescriptionsId",
                principalTable: "LocalizedStrings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStringSinger3_LocalizedStrings_ActivitiesId",
                table: "LocalizedStringSinger3",
                column: "ActivitiesId",
                principalTable: "LocalizedStrings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStrings_Albums_AlbumId",
                table: "LocalizedStrings");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStrings_Collection_CollectionId",
                table: "LocalizedStrings");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStrings_Composer_ComposerId",
                table: "LocalizedStrings");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStrings_Composer_ComposerId1",
                table: "LocalizedStrings");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStrings_SongWriter_SongWriterId",
                table: "LocalizedStrings");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStrings_SongWriter_SongWriterId1",
                table: "LocalizedStrings");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStrings_Songs_SongId",
                table: "LocalizedStrings");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStrings_Work_WorkId",
                table: "LocalizedStrings");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStrings_Work_WorkId1",
                table: "LocalizedStrings");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStringSinger_LocalizedStrings_NamesId",
                table: "LocalizedStringSinger");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStringSinger1_LocalizedStrings_NicknamesId",
                table: "LocalizedStringSinger1");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStringSinger2_LocalizedStrings_DescriptionsId",
                table: "LocalizedStringSinger2");

            migrationBuilder.DropForeignKey(
                name: "FK_LocalizedStringSinger3_LocalizedStrings_ActivitiesId",
                table: "LocalizedStringSinger3");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocalizedStrings",
                table: "LocalizedStrings");

            migrationBuilder.RenameTable(
                name: "LocalizedStrings",
                newName: "LocalizedString");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedStrings_WorkId1",
                table: "LocalizedString",
                newName: "IX_LocalizedString_WorkId1");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedStrings_WorkId",
                table: "LocalizedString",
                newName: "IX_LocalizedString_WorkId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedStrings_SongWriterId1",
                table: "LocalizedString",
                newName: "IX_LocalizedString_SongWriterId1");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedStrings_SongWriterId",
                table: "LocalizedString",
                newName: "IX_LocalizedString_SongWriterId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedStrings_SongId",
                table: "LocalizedString",
                newName: "IX_LocalizedString_SongId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedStrings_ComposerId1",
                table: "LocalizedString",
                newName: "IX_LocalizedString_ComposerId1");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedStrings_ComposerId",
                table: "LocalizedString",
                newName: "IX_LocalizedString_ComposerId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedStrings_CollectionId",
                table: "LocalizedString",
                newName: "IX_LocalizedString_CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_LocalizedStrings_AlbumId",
                table: "LocalizedString",
                newName: "IX_LocalizedString_AlbumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocalizedString",
                table: "LocalizedString",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedString_Albums_AlbumId",
                table: "LocalizedString",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedString_Collection_CollectionId",
                table: "LocalizedString",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedString_Composer_ComposerId",
                table: "LocalizedString",
                column: "ComposerId",
                principalTable: "Composer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedString_Composer_ComposerId1",
                table: "LocalizedString",
                column: "ComposerId1",
                principalTable: "Composer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedString_SongWriter_SongWriterId",
                table: "LocalizedString",
                column: "SongWriterId",
                principalTable: "SongWriter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedString_SongWriter_SongWriterId1",
                table: "LocalizedString",
                column: "SongWriterId1",
                principalTable: "SongWriter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedString_Songs_SongId",
                table: "LocalizedString",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedString_Work_WorkId",
                table: "LocalizedString",
                column: "WorkId",
                principalTable: "Work",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedString_Work_WorkId1",
                table: "LocalizedString",
                column: "WorkId1",
                principalTable: "Work",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStringSinger_LocalizedString_NamesId",
                table: "LocalizedStringSinger",
                column: "NamesId",
                principalTable: "LocalizedString",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStringSinger1_LocalizedString_NicknamesId",
                table: "LocalizedStringSinger1",
                column: "NicknamesId",
                principalTable: "LocalizedString",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStringSinger2_LocalizedString_DescriptionsId",
                table: "LocalizedStringSinger2",
                column: "DescriptionsId",
                principalTable: "LocalizedString",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizedStringSinger3_LocalizedString_ActivitiesId",
                table: "LocalizedStringSinger3",
                column: "ActivitiesId",
                principalTable: "LocalizedString",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
