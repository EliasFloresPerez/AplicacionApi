using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AplicacionApi.Migrations
{
    /// <inheritdoc />
    public partial class firtsmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Cedula = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    Edad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Cedula);
                });

            migrationBuilder.CreateTable(
                name: "Seguros",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    SumaAsegurada = table.Column<float>(type: "real", nullable: false),
                    Prima = table.Column<float>(type: "real", nullable: false),
                    ClienteCedula = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguros", x => x.Codigo);
                    table.ForeignKey(
                        name: "FK_Seguros_Clientes_ClienteCedula",
                        column: x => x.ClienteCedula,
                        principalTable: "Clientes",
                        principalColumn: "Cedula");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seguros_ClienteCedula",
                table: "Seguros",
                column: "ClienteCedula");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seguros");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
