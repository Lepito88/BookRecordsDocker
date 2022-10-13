using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRecords.Migrations
{
    public partial class refreshtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    idauthor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    firstname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci"),
                    lastname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idauthor);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    idbook = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    book_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci"),
                    release_year = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    type = table.Column<string>(type: "enum('Hardcover','Paperback','Digital','Comicbook')", nullable: true, collation: "utf8_general_ci"),
                    isbn = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, collation: "utf8_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idbook);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    idcategory = table.Column<int>(type: "int", nullable: false),
                    category_name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idcategory);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    iduser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8_general_ci"),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci"),
                    email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8_general_ci"),
                    firstname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8_general_ci"),
                    lastname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.iduser);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "author_books",
                columns: table => new
                {
                    idauthor = table.Column<int>(type: "int", nullable: false),
                    idbook = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.idauthor, x.idbook })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_author_authorbooks",
                        column: x => x.idauthor,
                        principalTable: "author",
                        principalColumn: "idauthor");
                    table.ForeignKey(
                        name: "fk_book_authorbooks",
                        column: x => x.idbook,
                        principalTable: "book",
                        principalColumn: "idbook");
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "book_categories",
                columns: table => new
                {
                    idbook = table.Column<int>(type: "int", nullable: false),
                    idcategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.idbook, x.idcategory })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_book_bookcategories",
                        column: x => x.idbook,
                        principalTable: "book",
                        principalColumn: "idbook");
                    table.ForeignKey(
                        name: "fk_category_bookcategories",
                        column: x => x.idcategory,
                        principalTable: "category",
                        principalColumn: "idcategory");
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
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.IdRefreshToken);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User",
                        column: x => x.Iduser,
                        principalTable: "user",
                        principalColumn: "iduser");
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "user_books",
                columns: table => new
                {
                    iduser = table.Column<int>(type: "int", nullable: false),
                    idbook = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.iduser, x.idbook })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_book_userbooks",
                        column: x => x.idbook,
                        principalTable: "book",
                        principalColumn: "idbook");
                    table.ForeignKey(
                        name: "fk_user_userbooks",
                        column: x => x.iduser,
                        principalTable: "user",
                        principalColumn: "iduser");
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "fk_author_authorbooks_idx",
                table: "author_books",
                column: "idauthor");

            migrationBuilder.CreateIndex(
                name: "fk_book_authorbooks_idx",
                table: "author_books",
                column: "idbook");

            migrationBuilder.CreateIndex(
                name: "fk_book_bookcategories_idx",
                table: "book_categories",
                column: "idbook");

            migrationBuilder.CreateIndex(
                name: "fk_category_bookcategories_idx",
                table: "book_categories",
                column: "idcategory");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_Iduser",
                table: "RefreshToken",
                column: "Iduser");

            migrationBuilder.CreateIndex(
                name: "username_UNIQUE",
                table: "user",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_book_userbooks_idx",
                table: "user_books",
                column: "idbook");

            migrationBuilder.CreateIndex(
                name: "fk_user_userbooks_idx",
                table: "user_books",
                column: "iduser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "author_books");

            migrationBuilder.DropTable(
                name: "book_categories");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "user_books");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
