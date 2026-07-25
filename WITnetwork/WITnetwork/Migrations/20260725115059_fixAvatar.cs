using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WITnetwork.Migrations
{
    /// <inheritdoc />
    public partial class fixAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_AlbumImages_AvatarId",
                table: "Profiles");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_AlbumImages_AvatarId",
                table: "Profiles",
                column: "AvatarId",
                principalTable: "AlbumImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_AlbumImages_AvatarId",
                table: "Profiles");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_AlbumImages_AvatarId",
                table: "Profiles",
                column: "AvatarId",
                principalTable: "AlbumImages",
                principalColumn: "Id");
        }
    }
}
