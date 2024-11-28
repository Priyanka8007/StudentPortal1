using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPortal1.Migrations.ChallanDb
{
    /// <inheritdoc />
    public partial class InitialCreatechallan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PoDtls",
                columns: table => new
                {
                    Pono = table.Column<int>(type: "int", nullable: false),
                    Posrno = table.Column<int>(type: "int", nullable: false),
                    Cono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MfgUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoDtls", x => new { x.Pono, x.Posrno });
                });

            migrationBuilder.CreateTable(
                name: "DlvChlns",
                columns: table => new
                {
                    Challanno = table.Column<int>(type: "int", nullable: false),
                    Challansrno = table.Column<int>(type: "int", nullable: false),
                    Pono = table.Column<int>(type: "int", nullable: false),
                    Posrno = table.Column<int>(type: "int", nullable: false),
                    Itemcode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DlvChlns", x => new { x.Challanno, x.Challansrno });
                    table.ForeignKey(
                        name: "FK_DlvChlns_PoDtls_Pono_Posrno",
                        columns: x => new { x.Pono, x.Posrno },
                        principalTable: "PoDtls",
                        principalColumns: new[] { "Pono", "Posrno" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DlvChlns_Pono_Posrno",
                table: "DlvChlns",
                columns: new[] { "Pono", "Posrno" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DlvChlns");

            migrationBuilder.DropTable(
                name: "PoDtls");
        }
    }
}
