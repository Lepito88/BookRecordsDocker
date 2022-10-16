using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRecords.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    Idauthor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci"),
                    Lastname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Idauthor);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Idbook = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BookName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci"),
                    ReleaseYear = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Type = table.Column<string>(type: "enum('Hardcover','Paperback','Digital','Comicbook')", nullable: true, collation: "utf8_general_ci"),
                    Isbn = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, collation: "utf8_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Idbook);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Idcategory = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Idcategory);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Iduser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8_general_ci"),
                    Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci"),
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8_general_ci"),
                    Firstname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8_general_ci"),
                    Lastname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Iduser);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "AuthorBooks",
                columns: table => new
                {
                    Idauthor = table.Column<int>(type: "int", nullable: false),
                    Idbook = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.Idauthor, x.Idbook })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_author_authorbooks",
                        column: x => x.Idauthor,
                        principalTable: "Author",
                        principalColumn: "Idauthor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_book_authorbooks",
                        column: x => x.Idbook,
                        principalTable: "Book",
                        principalColumn: "Idbook",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "BookCategories",
                columns: table => new
                {
                    Idbook = table.Column<int>(type: "int", nullable: false),
                    Idcategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.Idbook, x.Idcategory })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_book_bookcategories",
                        column: x => x.Idbook,
                        principalTable: "Book",
                        principalColumn: "Idbook",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_category_bookcategories",
                        column: x => x.Idcategory,
                        principalTable: "Category",
                        principalColumn: "Idcategory",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    IdRefreshToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Iduser = table.Column<int>(type: "int", nullable: false),
                    TokenHash = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false, collation: "utf8_general_ci"),
                    TokenSalt = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false, collation: "utf8_general_ci"),
                    timestamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdRefreshToken);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User",
                        column: x => x.Iduser,
                        principalTable: "User",
                        principalColumn: "Iduser");
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "UserBooks",
                columns: table => new
                {
                    Iduser = table.Column<int>(type: "int", nullable: false),
                    Idbook = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.Iduser, x.Idbook })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_book_userbooks",
                        column: x => x.Idbook,
                        principalTable: "Book",
                        principalColumn: "Idbook",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_userbooks",
                        column: x => x.Iduser,
                        principalTable: "User",
                        principalColumn: "Iduser",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "fk_author_authorbooks_idx",
                table: "AuthorBooks",
                column: "Idauthor");

            migrationBuilder.CreateIndex(
                name: "fk_book_authorbooks_idx",
                table: "AuthorBooks",
                column: "Idbook");

            migrationBuilder.CreateIndex(
                name: "fk_book_bookcategories_idx",
                table: "BookCategories",
                column: "Idbook");

            migrationBuilder.CreateIndex(
                name: "fk_category_bookcategories_idx",
                table: "BookCategories",
                column: "Idcategory");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_Iduser",
                table: "RefreshToken",
                column: "Iduser");

            migrationBuilder.CreateIndex(
                name: "Username_UNIQUE",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_book_userbooks_idx",
                table: "UserBooks",
                column: "Idbook");

            migrationBuilder.CreateIndex(
                name: "fk_user_userbooks_idx",
                table: "UserBooks",
                column: "Iduser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBooks");

            migrationBuilder.DropTable(
                name: "BookCategories");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "UserBooks");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
