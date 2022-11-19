using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practice.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Works",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Works_WorkerId",
                table: "Works",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Workers_WorkerId",
                table: "Works",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "WorkerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Workers_WorkerId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_WorkerId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Works");
        }
    }
}
