using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MangaStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeImageNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_Images_ImageId",
                table: "Mangas");

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_Images_ImageId",
                table: "Mangas",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mangas_Images_ImageId",
                table: "Mangas");

            migrationBuilder.AddForeignKey(
                name: "FK_Mangas_Images_ImageId",
                table: "Mangas",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
