using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AP.quejasymasquejas.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quejas_Usuarios_UsuarioID",
                table: "Quejas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "Quejas",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Quejas_UsuarioID",
                table: "Quejas",
                newName: "IX_Quejas_UsuarioId");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Quejas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Quejas",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Quejas_AspNetUsers_UsuarioId",
                table: "Quejas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quejas_AspNetUsers_UsuarioId",
                table: "Quejas");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Quejas",
                newName: "UsuarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Quejas_UsuarioId",
                table: "Quejas",
                newName: "IX_Quejas_UsuarioID");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioID",
                table: "Quejas",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Quejas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Quejas_Usuarios_UsuarioID",
                table: "Quejas",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "UsuarioID");
        }
    }
}
