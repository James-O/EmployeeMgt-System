using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeMgtSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class Jobrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobRoleId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRoles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_JobRoleId",
                table: "AspNetUsers",
                column: "JobRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_JobRoles_JobRoleId",
                table: "AspNetUsers",
                column: "JobRoleId",
                principalTable: "JobRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_JobRoles_JobRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "JobRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_JobRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "JobRoleId",
                table: "AspNetUsers");
        }
    }
}
