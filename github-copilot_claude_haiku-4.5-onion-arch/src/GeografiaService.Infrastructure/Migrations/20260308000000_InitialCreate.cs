using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GeografiaService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Nome = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CodigoISO3 = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    CodigoONU = table.Column<int>(type: "integer", nullable: false),
                    CodigoDDI = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CodigoMoeda = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    DefaultLocale = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    PaisId = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Nome = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Sigla = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estados_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    EstadoId = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    Nome = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CodigoPostal = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cidades_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cidades_CodigoPostal",
                table: "Cidades",
                column: "CodigoPostal",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cidades_Nome",
                table: "Cidades",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_Nome",
                table: "Estados",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_PaisId",
                table: "Estados",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Estados_Sigla",
                table: "Estados",
                column: "Sigla");

            migrationBuilder.CreateIndex(
                name: "IX_Paises_Nome",
                table: "Paises",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cidades");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Paises");
        }
    }
}

