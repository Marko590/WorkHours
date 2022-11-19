using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practice.Migrations
{
    /// <inheritdoc />
    public partial class AddedWorksiteWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorksiteId",
                table: "Works",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Works_WorksiteId",
                table: "Works",
                column: "WorksiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Worksites_WorksiteId",
                table: "Works",
                column: "WorksiteId",
                principalTable: "Worksites",
                principalColumn: "WorksiteId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Worksites_WorksiteId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_WorksiteId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "WorksiteId",
                table: "Works");
        }
    }
}
