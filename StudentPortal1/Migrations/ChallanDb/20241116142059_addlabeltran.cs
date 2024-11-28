using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentPortal1.Migrations.ChallanDb
{
    /// <inheritdoc />
    public partial class addlabeltran : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChallanSplitResults",
                columns: table => new
                {
                    Challanno = table.Column<int>(type: "int", nullable: false),
                    Challansrno = table.Column<int>(type: "int", nullable: false),
                    Pono = table.Column<int>(type: "int", nullable: false),
                    Posrno = table.Column<int>(type: "int", nullable: false),
                    Itemcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabelQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    labelno = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "LabeTrans",
                columns: table => new
                {
                    LabelSrNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ChallanNo = table.Column<int>(type: "int", nullable: false),
                    ChallanSrNo = table.Column<int>(type: "int", nullable: false),
                    PONo = table.Column<int>(type: "int", nullable: false),
                    POSrNo = table.Column<int>(type: "int", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LabelNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabeTrans", x => x.LabelSrNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallanSplitResults");

            migrationBuilder.DropTable(
                name: "LabeTrans");
        }
    }
}
