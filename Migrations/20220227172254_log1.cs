using Microsoft.EntityFrameworkCore.Migrations;

namespace my_books.Migrations
{
    public partial class log1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageTemplete",
                table: "Logs",
                newName: "MessageTemplate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageTemplate",
                table: "Logs",
                newName: "MessageTemplete");
        }
    }
}
