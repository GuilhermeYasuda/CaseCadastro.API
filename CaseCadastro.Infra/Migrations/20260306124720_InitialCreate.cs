using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CaseCadastro.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Cep = table.Column<string>(type: "TEXT", maxLength: 9, nullable: false),
                    Logradouro = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Complemento = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Unidade = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Bairro = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Localidade = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Uf = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    Estado = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PessoasFisicas",
                columns: table => new
                {
                    Cpf = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                    Id = table.Column<string>(type: "TEXT", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    EnderecoId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoasFisicas", x => x.Cpf);
                    table.ForeignKey(
                        name: "FK_PessoasFisicas_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PessoasJuridicas",
                columns: table => new
                {
                    Cnpj = table.Column<string>(type: "TEXT", maxLength: 18, nullable: false),
                    RazaoSocial = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    EnderecoId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoasJuridicas", x => x.Cnpj);
                    table.ForeignKey(
                        name: "FK_PessoasJuridicas_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PessoasFisicas_EnderecoId",
                table: "PessoasFisicas",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoasJuridicas_EnderecoId",
                table: "PessoasJuridicas",
                column: "EnderecoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PessoasFisicas");

            migrationBuilder.DropTable(
                name: "PessoasJuridicas");

            migrationBuilder.DropTable(
                name: "Enderecos");
        }
    }
}
