using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinica.Migrations
{
    /// <inheritdoc />
    public partial class RelacionEspecialidadMedico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EspecialidadId",
                table: "Medico",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medico_EspecialidadId",
                table: "Medico",
                column: "EspecialidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medico_Especialidad_EspecialidadId",
                table: "Medico",
                column: "EspecialidadId",
                principalTable: "Especialidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medico_Especialidad_EspecialidadId",
                table: "Medico");

            migrationBuilder.DropIndex(
                name: "IX_Medico_EspecialidadId",
                table: "Medico");

            migrationBuilder.DropColumn(
                name: "EspecialidadId",
                table: "Medico");
        }
    }
}
