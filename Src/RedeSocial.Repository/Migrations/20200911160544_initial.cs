using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RedeSocial.Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Perfis",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    FotoPerfilUrl = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(maxLength: 150, nullable: false),
                    SobreNome = table.Column<string>(maxLength: 150, nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    PerfilID = table.Column<Guid>(nullable: true),
                    PerfilID1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Perfis_Perfis_PerfilID",
                        column: x => x.PerfilID,
                        principalTable: "Perfis",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Perfis_Perfis_PerfilID1",
                        column: x => x.PerfilID1,
                        principalTable: "Perfis",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "Portagens",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    FotoPostUrl = table.Column<string>(nullable: true),
                    ID_PerfilID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portagens", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Portagens_Perfis_ID_PerfilID",
                        column: x => x.ID_PerfilID,
                        principalTable: "Perfis",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: false),
                    Password = table.Column<string>(maxLength: 150, nullable: false),
                    ID_PerfilID = table.Column<Guid>(nullable: true),
                    ID_RoleID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounts_Perfis_ID_PerfilID",
                        column: x => x.ID_PerfilID,
                        principalTable: "Perfis",
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
                name: "Comentarios",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    ID_PerfilID = table.Column<Guid>(nullable: true),
                    ID_PostagemID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comentarios_Perfis_ID_PerfilID",
                        column: x => x.ID_PerfilID,
                        principalTable: "Perfis",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comentarios_Portagens_ID_PostagemID",
                        column: x => x.ID_PostagemID,
                        principalTable: "Portagens",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ID_PerfilID",
                table: "Accounts",
                column: "ID_PerfilID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ID_RoleID",
                table: "Accounts",
                column: "ID_RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_ID_PerfilID",
                table: "Comentarios",
                column: "ID_PerfilID");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_ID_PostagemID",
                table: "Comentarios",
                column: "ID_PostagemID");

            migrationBuilder.CreateIndex(
                name: "IX_Perfis_PerfilID",
                table: "Perfis",
                column: "PerfilID");

            migrationBuilder.CreateIndex(
                name: "IX_Perfis_PerfilID1",
                table: "Perfis",
                column: "PerfilID1");

            migrationBuilder.CreateIndex(
                name: "IX_Portagens_ID_PerfilID",
                table: "Portagens",
                column: "ID_PerfilID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Portagens");

            migrationBuilder.DropTable(
                name: "Perfis");
        }
    }
}
