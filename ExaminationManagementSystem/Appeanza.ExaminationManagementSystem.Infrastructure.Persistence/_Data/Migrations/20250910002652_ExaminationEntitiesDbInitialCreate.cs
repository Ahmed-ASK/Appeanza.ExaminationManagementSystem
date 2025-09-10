using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appeanza.ExaminationManagementSystem.Infrastructure.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExaminationEntitiesDbInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "DATETIME", nullable: false, computedColumnSql: "SYSUTCDATETIME()"),
                    TeacherId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    TimeInminutes = table.Column<int>(type: "INT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "DATETIME", nullable: false, computedColumnSql: "SYSUTCDATETIME()"),
                    PublishedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Status = table.Column<int>(type: "INT", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.Id);
                    table.CheckConstraint("CK_Exam_Status", "[Status] >= 0 AND [Status] <= 4");
                    table.ForeignKey(
                        name: "FK_Exam_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentGroup",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JoinedDate = table.Column<DateTime>(type: "DATETIME", nullable: false, computedColumnSql: "SYSUTCDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroup", x => new { x.StudentId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_StudentGroup_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<string>(type: "NVARCHAR", nullable: false),
                    Marks = table.Column<decimal>(type: "DECIMAL(4,2)", nullable: false),
                    QuestionNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentsExams",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    SubmitAt = table.Column<DateTime>(type: "DATETIME", nullable: false, computedColumnSql: "SYSUTCDATETIME()"),
                    Score = table.Column<decimal>(type: "DECIMAL(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsExams", x => new { x.UserId, x.ExamId });
                    table.ForeignKey(
                        name: "FK_StudentsExams_Exam_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "NVARCHAR", nullable: false),
                    IsRightAnswer = table.Column<bool>(type: "BIT", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answer_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_GroupId",
                table: "Exam",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_ExamId",
                table: "Question",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroup_GroupId",
                table: "StudentGroup",
                column: "GroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentsExams_ExamId",
                table: "StudentsExams",
                column: "ExamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "StudentGroup");

            migrationBuilder.DropTable(
                name: "StudentsExams");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "Group");
        }
    }
}
