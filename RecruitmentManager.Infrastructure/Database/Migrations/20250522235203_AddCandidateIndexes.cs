using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitmentManager.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidateIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Email",
                schema: "RCM",
                table: "Candidates",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_FirstName",
                schema: "RCM",
                table: "Candidates",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_FirstName_SurName",
                schema: "RCM",
                table: "Candidates",
                columns: new[] { "FirstName", "SurName" });

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_SurName",
                schema: "RCM",
                table: "Candidates",
                column: "SurName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Candidates_Email",
                schema: "RCM",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_FirstName",
                schema: "RCM",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_FirstName_SurName",
                schema: "RCM",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_SurName",
                schema: "RCM",
                table: "Candidates");
        }
    }
}
