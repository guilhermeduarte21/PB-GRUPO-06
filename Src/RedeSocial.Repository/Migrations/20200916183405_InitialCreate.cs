using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RedeSocial.Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: false),
                    Password = table.Column<string>(maxLength: 150, nullable: false),
                    FotoPerfilUrl = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(maxLength: 150, nullable: false),
                    SobreNome = table.Column<string>(maxLength: 150, nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    ID_RoleID = table.Column<Guid>(nullable: true),
                    AccountID = table.Column<Guid>(nullable: true),
                    AccountID1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_AccountID1",
                        column: x => x.AccountID1,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accounts_Role_ID_RoleID",
                        column: x => x.ID_RoleID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Postagens",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    DataPostagem = table.Column<DateTime>(nullable: false),
                    FotoPostUrl = table.Column<string>(nullable: true),
                    ID_AccountID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postagens", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Postagens_Accounts_ID_AccountID",
                        column: x => x.ID_AccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    ID_AccountID = table.Column<Guid>(nullable: true),
                    ID_PostagemID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comentarios_Accounts_ID_AccountID",
                        column: x => x.ID_AccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comentarios_Postagens_ID_PostagemID",
                        column: x => x.ID_PostagemID,
                        principalTable: "Postagens",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountID",
                table: "Accounts",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountID1",
                table: "Accounts",
                column: "AccountID1");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ID_RoleID",
                table: "Accounts",
                column: "ID_RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_ID_AccountID",
                table: "Comentarios",
                column: "ID_AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_ID_PostagemID",
                table: "Comentarios",
                column: "ID_PostagemID");

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_ID_AccountID",
                table: "Postagens",
                column: "ID_AccountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Postagens");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
