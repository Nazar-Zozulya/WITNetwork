using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WITnetwork.Migrations
{
    /// <inheritdoc />
    public partial class fix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_AlbumImages_AvatarId",
                table: "Profiles");

            migrationBuilder.AlterColumn<long>(
                name: "AvatarId",
                table: "Profiles",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_AlbumImages_AvatarId",
                table: "Profiles",
                column: "AvatarId",
                principalTable: "AlbumImages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_AlbumImages_AvatarId",
                table: "Profiles");

            migrationBuilder.AlterColumn<long>(
                name: "AvatarId",
                table: "Profiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_AlbumImages_AvatarId",
                table: "Profiles",
                column: "AvatarId",
                principalTable: "AlbumImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
