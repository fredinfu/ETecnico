using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AfiliacionServicios.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSolicitudAfiliacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "SolicitudAfiliacion",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Servicio",
                table: "SolicitudAfiliacion",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "SolicitudAfiliacion");

            migrationBuilder.DropColumn(
                name: "Servicio",
                table: "SolicitudAfiliacion");
        }
    }
}
