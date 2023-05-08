using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArticlesViewer.Infrastructure.Migrations
{
    public partial class PKAddedToHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ArticleUserHistories_ArticleId",
                table: "ArticleUserHistories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleUserHistories",
                table: "ArticleUserHistories",
                columns: new[] { "ArticleId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleUserHistories",
                table: "ArticleUserHistories");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleUserHistories_ArticleId",
                table: "ArticleUserHistories",
                column: "ArticleId");
        }
    }
}
