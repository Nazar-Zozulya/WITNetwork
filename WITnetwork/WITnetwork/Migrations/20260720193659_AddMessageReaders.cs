using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WITnetwork.Migrations
{
    /// <inheritdoc />
    public partial class AddMessageReaders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Messages_MessageId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MessageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "MessageReaders",
                columns: table => new
                {
                    MessageId = table.Column<long>(type: "bigint", nullable: false),
                    ReadersId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReaders", x => new { x.MessageId, x.ReadersId });
                    table.ForeignKey(
                        name: "FK_MessageReaders_AspNetUsers_ReadersId",
                        column: x => x.ReadersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageReaders_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageReaders_ReadersId",
                table: "MessageReaders",
                column: "ReadersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageReaders");

            migrationBuilder.AddColumn<long>(
                name: "MessageId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MessageId",
                table: "AspNetUsers",
                column: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Messages_MessageId",
                table: "AspNetUsers",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id");
        }
    }
}
