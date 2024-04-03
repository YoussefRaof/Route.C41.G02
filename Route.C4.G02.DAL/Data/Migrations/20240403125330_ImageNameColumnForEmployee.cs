using Microsoft.EntityFrameworkCore.Migrations;

namespace Route.C4.G02.DAL.Data.Migrations
{
    public partial class ImageNameColumnForEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Empolyees",
                type: "nvarchar(max)",
                nullable: true);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Empolyees");


        }
    }
}
