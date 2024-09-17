using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPortal1.Migrations.StudentAuthDb
{
    /// <inheritdoc />
    public partial class UserActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ControllerName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserActivities");
        }

    }
}
