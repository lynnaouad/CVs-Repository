using Microsoft.EntityFrameworkCore.Migrations;

namespace CV_Project2022.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<string>(type: "TEXT", nullable: true),
                    Nationality = table.Column<string>(type: "TEXT", nullable: true),
                    Gender = table.Column<string>(type: "TEXT", nullable: true),
                    Skills = table.Column<string>(type: "TEXT", nullable: true),
                    Photo = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Grade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SKILL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SKILL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CV_SKILL",
                columns: table => new
                {
                    CvId = table.Column<int>(type: "INTEGER", nullable: false),
                    SkillId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV_SKILL", x => new { x.CvId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_CV_SKILL_CV_CvId",
                        column: x => x.CvId,
                        principalTable: "CV",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CV_SKILL_SKILL_SkillId",
                        column: x => x.SkillId,
                        principalTable: "SKILL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CV_SKILL_SkillId",
                table: "CV_SKILL",
                column: "SkillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CV_SKILL");

            migrationBuilder.DropTable(
                name: "CV");

            migrationBuilder.DropTable(
                name: "SKILL");
        }
    }
}
