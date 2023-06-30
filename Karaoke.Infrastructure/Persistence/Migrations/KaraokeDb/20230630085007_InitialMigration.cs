using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karaoke.Infrastructure.Persistence.Migrations.KaraokeDb
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ProfilePicture = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Composer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Composer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Extension = table.Column<string>(type: "TEXT", nullable: false),
                    MimeType = table.Column<string>(type: "TEXT", nullable: false),
                    Size = table.Column<long>(type: "INTEGER", nullable: false),
                    Hash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SongWriter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongWriter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    ProfilePicture = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccessToken = table.Column<string>(type: "TEXT", nullable: false),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: false),
                    UsageCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Token_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CoverId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Files_CoverId",
                        column: x => x.CoverId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Singers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProfilePictureId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Birthday = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BloodType = table.Column<string>(type: "TEXT", nullable: false),
                    Height = table.Column<float>(type: "REAL", nullable: false),
                    Nationality = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Singers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Singers_Files_ProfilePictureId",
                        column: x => x.ProfilePictureId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Duration = table.Column<long>(type: "INTEGER", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OriginalLanguage = table.Column<string>(type: "TEXT", nullable: false),
                    OriginalFileId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ThumbnailId = table.Column<Guid>(type: "TEXT", nullable: true),
                    VocalId = table.Column<Guid>(type: "TEXT", nullable: true),
                    InstrumentalId = table.Column<Guid>(type: "TEXT", nullable: true),
                    PreviewId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_Files_InstrumentalId",
                        column: x => x.InstrumentalId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Songs_Files_OriginalFileId",
                        column: x => x.OriginalFileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Songs_Files_PreviewId",
                        column: x => x.PreviewId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Songs_Files_ThumbnailId",
                        column: x => x.ThumbnailId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Songs_Files_VocalId",
                        column: x => x.VocalId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TokenId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: false),
                    UsageCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Token_TokenId",
                        column: x => x.TokenId,
                        principalTable: "Token",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlbumSinger",
                columns: table => new
                {
                    AlbumsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SingersId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumSinger", x => new { x.AlbumsId, x.SingersId });
                    table.ForeignKey(
                        name: "FK_AlbumSinger_Albums_AlbumsId",
                        column: x => x.AlbumsId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumSinger_Singers_SingersId",
                        column: x => x.SingersId,
                        principalTable: "Singers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlbumSong",
                columns: table => new
                {
                    AlbumsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SongsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumSong", x => new { x.AlbumsId, x.SongsId });
                    table.ForeignKey(
                        name: "FK_AlbumSong_Albums_AlbumsId",
                        column: x => x.AlbumsId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumSong_Songs_SongsId",
                        column: x => x.SongsId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollectionSong",
                columns: table => new
                {
                    CollectionsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SongsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionSong", x => new { x.CollectionsId, x.SongsId });
                    table.ForeignKey(
                        name: "FK_CollectionSong_Collection_CollectionsId",
                        column: x => x.CollectionsId,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionSong_Songs_SongsId",
                        column: x => x.SongsId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComposerSong",
                columns: table => new
                {
                    ComposersId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SongsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComposerSong", x => new { x.ComposersId, x.SongsId });
                    table.ForeignKey(
                        name: "FK_ComposerSong_Composer_ComposersId",
                        column: x => x.ComposersId,
                        principalTable: "Composer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComposerSong_Songs_SongsId",
                        column: x => x.SongsId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KaraokeInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    SingersCount = table.Column<int>(type: "INTEGER", nullable: false),
                    SongId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaraokeInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KaraokeInfo_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KaraokeInfo_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lyrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    SongId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lyrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lyrics_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SingerSong",
                columns: table => new
                {
                    SingersId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SongsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingerSong", x => new { x.SingersId, x.SongsId });
                    table.ForeignKey(
                        name: "FK_SingerSong_Singers_SingersId",
                        column: x => x.SingersId,
                        principalTable: "Singers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SingerSong_Songs_SongsId",
                        column: x => x.SongsId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SongSongWriter",
                columns: table => new
                {
                    SongWritersId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SongsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongSongWriter", x => new { x.SongWritersId, x.SongsId });
                    table.ForeignKey(
                        name: "FK_SongSongWriter_SongWriter_SongWritersId",
                        column: x => x.SongWritersId,
                        principalTable: "SongWriter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongSongWriter_Songs_SongsId",
                        column: x => x.SongsId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CollectionId = table.Column<Guid>(type: "TEXT", nullable: true),
                    SongId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tags_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Work",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SongId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    LastModified = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Work", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Work_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KaraokeInfoUser",
                columns: table => new
                {
                    CreatorsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    KaraokeId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KaraokeInfoUser", x => new { x.CreatorsId, x.KaraokeId });
                    table.ForeignKey(
                        name: "FK_KaraokeInfoUser_KaraokeInfo_KaraokeId",
                        column: x => x.KaraokeId,
                        principalTable: "KaraokeInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KaraokeInfoUser_User_CreatorsId",
                        column: x => x.CreatorsId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalizedStrings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    Language = table.Column<string>(type: "TEXT", nullable: false),
                    AlbumId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CollectionId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ComposerId = table.Column<Guid>(type: "TEXT", nullable: true),
                    ComposerId1 = table.Column<Guid>(type: "TEXT", nullable: true),
                    SongId = table.Column<Guid>(type: "TEXT", nullable: true),
                    SongWriterId = table.Column<Guid>(type: "TEXT", nullable: true),
                    SongWriterId1 = table.Column<Guid>(type: "TEXT", nullable: true),
                    WorkId = table.Column<Guid>(type: "TEXT", nullable: true),
                    WorkId1 = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedStrings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalizedStrings_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LocalizedStrings_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LocalizedStrings_Composer_ComposerId",
                        column: x => x.ComposerId,
                        principalTable: "Composer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LocalizedStrings_Composer_ComposerId1",
                        column: x => x.ComposerId1,
                        principalTable: "Composer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LocalizedStrings_SongWriter_SongWriterId",
                        column: x => x.SongWriterId,
                        principalTable: "SongWriter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LocalizedStrings_SongWriter_SongWriterId1",
                        column: x => x.SongWriterId1,
                        principalTable: "SongWriter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LocalizedStrings_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LocalizedStrings_Work_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Work",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LocalizedStrings_Work_WorkId1",
                        column: x => x.WorkId1,
                        principalTable: "Work",
                        principalColumn: "Id");
                });

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
                name: "IX_Albums_CoverId",
                table: "Albums",
                column: "CoverId");

            migrationBuilder.CreateIndex(
                name: "IX_AlbumSinger_SingersId",
                table: "AlbumSinger",
                column: "SingersId");

            migrationBuilder.CreateIndex(
                name: "IX_AlbumSong_SongsId",
                table: "AlbumSong",
                column: "SongsId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionSong_SongsId",
                table: "CollectionSong",
                column: "SongsId");

            migrationBuilder.CreateIndex(
                name: "IX_ComposerSong_SongsId",
                table: "ComposerSong",
                column: "SongsId");

            migrationBuilder.CreateIndex(
                name: "IX_KaraokeInfo_FileId",
                table: "KaraokeInfo",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_KaraokeInfo_SongId",
                table: "KaraokeInfo",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_KaraokeInfoUser_KaraokeId",
                table: "KaraokeInfoUser",
                column: "KaraokeId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStrings_AlbumId",
                table: "LocalizedStrings",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStrings_CollectionId",
                table: "LocalizedStrings",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStrings_ComposerId",
                table: "LocalizedStrings",
                column: "ComposerId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStrings_ComposerId1",
                table: "LocalizedStrings",
                column: "ComposerId1");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStrings_SongId",
                table: "LocalizedStrings",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStrings_SongWriterId",
                table: "LocalizedStrings",
                column: "SongWriterId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStrings_SongWriterId1",
                table: "LocalizedStrings",
                column: "SongWriterId1");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStrings_WorkId",
                table: "LocalizedStrings",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStrings_WorkId1",
                table: "LocalizedStrings",
                column: "WorkId1");

            migrationBuilder.CreateIndex(
                name: "IX_Lyrics_SongId",
                table: "Lyrics",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_TokenId",
                table: "RefreshToken",
                column: "TokenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Singers_ProfilePictureId",
                table: "Singers",
                column: "ProfilePictureId");

            migrationBuilder.CreateIndex(
                name: "IX_SingerSong_SongsId",
                table: "SingerSong",
                column: "SongsId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_InstrumentalId",
                table: "Songs",
                column: "InstrumentalId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_OriginalFileId",
                table: "Songs",
                column: "OriginalFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_PreviewId",
                table: "Songs",
                column: "PreviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ThumbnailId",
                table: "Songs",
                column: "ThumbnailId");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_VocalId",
                table: "Songs",
                column: "VocalId");

            migrationBuilder.CreateIndex(
                name: "IX_SongSongWriter_SongsId",
                table: "SongSongWriter",
                column: "SongsId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CollectionId",
                table: "Tags",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_SongId",
                table: "Tags",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_Token_UserId",
                table: "Token",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Work_SongId",
                table: "Work",
                column: "SongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumSinger");

            migrationBuilder.DropTable(
                name: "AlbumSong");

            migrationBuilder.DropTable(
                name: "CollectionSong");

            migrationBuilder.DropTable(
                name: "ComposerSong");

            migrationBuilder.DropTable(
                name: "KaraokeInfoUser");

            migrationBuilder.DropTable(
                name: "Lyrics");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "SingerActivitiesLocalizedString");

            migrationBuilder.DropTable(
                name: "SingerDescriptionsLocalizedString");

            migrationBuilder.DropTable(
                name: "SingerNamesLocalizedString");

            migrationBuilder.DropTable(
                name: "SingerNicknamesLocalizedString");

            migrationBuilder.DropTable(
                name: "SingerSong");

            migrationBuilder.DropTable(
                name: "SongSongWriter");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "KaraokeInfo");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "LocalizedStrings");

            migrationBuilder.DropTable(
                name: "Singers");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropTable(
                name: "Composer");

            migrationBuilder.DropTable(
                name: "SongWriter");

            migrationBuilder.DropTable(
                name: "Work");

            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
