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
                name: "book",
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
                name: "category",
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
                name: "user",
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
                name: "author_books",
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
                        principalTable: "author",
                        principalColumn: "Idauthor",
                        onUpdate:ReferentialAction.Cascade,
                        onDelete:ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_book_authorbooks",
                        column: x => x.Idbook,
                        principalTable: "book",
                        principalColumn: "Idbook",
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "book_categories",
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
                        principalTable: "book",
                        principalColumn: "Idbook",
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_category_bookcategories",
                        column: x => x.Idcategory,
                        principalTable: "category",
                        principalColumn: "Idcategory",
                        onUpdate: ReferentialAction.Cascade,
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
                        principalColumn: "Iduser");
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "user_books",
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
                        principalTable: "book",
                        principalColumn: "Idbook",
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_userbooks",
                        column: x => x.Iduser,
                        principalTable: "user",
                        principalColumn: "Iduser",
                        onUpdate: ReferentialAction.Cascade,
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "fk_author_authorbooks_idx",
                table: "author_books",
                column: "Idauthor");

            migrationBuilder.CreateIndex(
                name: "fk_book_authorbooks_idx",
                table: "author_books",
                column: "Idbook");

            migrationBuilder.CreateIndex(
                name: "fk_book_bookcategories_idx",
                table: "book_categories",
                column: "Idbook");

            migrationBuilder.CreateIndex(
                name: "fk_category_bookcategories_idx",
                table: "book_categories",
                column: "Idcategory");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_Iduser",
                table: "RefreshToken",
                column: "Iduser");

            migrationBuilder.CreateIndex(
                name: "username_UNIQUE",
                table: "user",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_book_userbooks_idx",
                table: "user_books",
                column: "Idbook");

            migrationBuilder.CreateIndex(
                name: "fk_user_userbooks_idx",
                table: "user_books",
                column: "Iduser");
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
