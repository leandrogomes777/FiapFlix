using Microsoft.EntityFrameworkCore.Migrations;

namespace CSCServiceReceive.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechnicalProblems",
                columns: table => new
                {
                    TechnicalProblemsId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalProblems", x => x.TechnicalProblemsId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequest",
                columns: table => new
                {
                    ServiceRequestId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnicalProblemsId = table.Column<long>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequest", x => x.ServiceRequestId);
                    table.ForeignKey(
                        name: "FK_ServiceRequest_TechnicalProblems_TechnicalProblemsId",
                        column: x => x.TechnicalProblemsId,
                        principalTable: "TechnicalProblems",
                        principalColumn: "TechnicalProblemsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequest_TechnicalProblemsId",
                table: "ServiceRequest",
                column: "TechnicalProblemsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRequest");

            migrationBuilder.DropTable(
                name: "TechnicalProblems");
        }
    }
}
