using Microsoft.EntityFrameworkCore.Migrations;

namespace Route.C4.G02.DAL.Data.Migrations
{
    public partial class EmployeeDepartmentRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Empolyees",
                type: "int",
                nullable: true);

           

            migrationBuilder.CreateIndex(
                name: "IX_Empolyees_DepartmentId",
                table: "Empolyees",
                column: "DepartmentId");

           

            migrationBuilder.AddForeignKey(
                name: "FK_Empolyees_Departments_DepartmentId",
                table: "Empolyees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

             
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empolyees_Departments_DepartmentId",
                table: "Empolyees");


            migrationBuilder.DropIndex(
                name: "IX_Empolyees_DepartmentId",
                table: "Empolyees");

           

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Empolyees");

             
        }
    }
}
