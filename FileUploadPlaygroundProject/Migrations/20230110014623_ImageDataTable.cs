using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileUploadPlaygroundProject.Migrations
{
    /// <inheritdoc />
    public partial class ImageDataTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImageData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalFileName = table.Column<string>(type: "text", nullable: false),
                    OriginalType = table.Column<string>(type: "text", nullable: false),
                    OriginalContent = table.Column<byte[]>(type: "bytea", nullable: false),
                    ThumbnailContent = table.Column<byte[]>(type: "bytea", nullable: false),
                    FullscreenContent = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageData");
        }
    }
}
