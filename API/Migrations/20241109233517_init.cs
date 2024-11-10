using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DateAppApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DateDateIdea",
                columns: table => new
                {
                    DateIdeasId = table.Column<int>(type: "int", nullable: false),
                    DatesPresentOnId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateDateIdea", x => new { x.DateIdeasId, x.DatesPresentOnId });
                });

            migrationBuilder.CreateTable(
                name: "DateIdeas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatingUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateIdeas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatingUserId = table.Column<int>(type: "int", nullable: false),
                    OtherUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PictureOfDateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Dates_PictureOfDateId",
                        column: x => x.PictureOfDateId,
                        principalTable: "Dates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    ProfilePictureId = table.Column<int>(type: "int", nullable: true),
                    TimeJoined = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Images_ProfilePictureId",
                        column: x => x.ProfilePictureId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DateDateIdea_DatesPresentOnId",
                table: "DateDateIdea",
                column: "DatesPresentOnId");

            migrationBuilder.CreateIndex(
                name: "IX_DateIdeas_CreatingUserId",
                table: "DateIdeas",
                column: "CreatingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dates_CreatingUserId",
                table: "Dates",
                column: "CreatingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Dates_OtherUserId",
                table: "Dates",
                column: "OtherUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_PictureOfDateId",
                table: "Images",
                column: "PictureOfDateId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfilePictureId",
                table: "Users",
                column: "ProfilePictureId",
                unique: true,
                filter: "[ProfilePictureId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DateDateIdea_DateIdeas_DateIdeasId",
                table: "DateDateIdea",
                column: "DateIdeasId",
                principalTable: "DateIdeas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DateDateIdea_Dates_DatesPresentOnId",
                table: "DateDateIdea",
                column: "DatesPresentOnId",
                principalTable: "Dates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DateIdeas_Users_CreatingUserId",
                table: "DateIdeas",
                column: "CreatingUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dates_Users_CreatingUserId",
                table: "Dates",
                column: "CreatingUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dates_Users_OtherUserId",
                table: "Dates",
                column: "OtherUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Dates_PictureOfDateId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "DateDateIdea");

            migrationBuilder.DropTable(
                name: "DateIdeas");

            migrationBuilder.DropTable(
                name: "Dates");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
