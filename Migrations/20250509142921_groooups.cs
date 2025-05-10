using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class groooups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationRoomUser_Rooms_ConversationRoomsRoomName",
                table: "ConversationRoomUser");

            migrationBuilder.RenameColumn(
                name: "ConversationRoomsRoomName",
                table: "ConversationRoomUser",
                newName: "RoomsRoomName");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationRoomUser_Rooms_RoomsRoomName",
                table: "ConversationRoomUser",
                column: "RoomsRoomName",
                principalTable: "Rooms",
                principalColumn: "RoomName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationRoomUser_Rooms_RoomsRoomName",
                table: "ConversationRoomUser");

            migrationBuilder.RenameColumn(
                name: "RoomsRoomName",
                table: "ConversationRoomUser",
                newName: "ConversationRoomsRoomName");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationRoomUser_Rooms_ConversationRoomsRoomName",
                table: "ConversationRoomUser",
                column: "ConversationRoomsRoomName",
                principalTable: "Rooms",
                principalColumn: "RoomName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
