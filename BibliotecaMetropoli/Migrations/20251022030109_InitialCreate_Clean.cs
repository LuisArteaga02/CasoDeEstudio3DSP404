using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaMetropoli.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_Clean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    idAutor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.idAutor);
                });

            migrationBuilder.CreateTable(
                name: "Editorial",
                columns: table => new
                {
                    IdEdit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editorial", x => x.IdEdit);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    IdPais = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.IdPais);
                });

            migrationBuilder.CreateTable(
                name: "TipoRecurso",
                columns: table => new
                {
                    IdTipoR = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoRecurso", x => x.IdTipoR);
                });

            migrationBuilder.CreateTable(
                name: "Recurso",
                columns: table => new
                {
                    IdRec = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPais = table.Column<int>(type: "int", nullable: false),
                    IdTipoR = table.Column<int>(type: "int", nullable: false),
                    IdEdit = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    annopublic = table.Column<int>(type: "int", nullable: true),
                    Edicion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PalabrasBusqueda = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    AutoridAutor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recurso", x => x.IdRec);
                    table.ForeignKey(
                        name: "FK_Recurso_Autor_AutoridAutor",
                        column: x => x.AutoridAutor,
                        principalTable: "Autor",
                        principalColumn: "idAutor");
                    table.ForeignKey(
                        name: "FK_Recurso_Editorial_IdEdit",
                        column: x => x.IdEdit,
                        principalTable: "Editorial",
                        principalColumn: "IdEdit",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recurso_Pais_IdPais",
                        column: x => x.IdPais,
                        principalTable: "Pais",
                        principalColumn: "IdPais",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recurso_TipoRecurso_IdTipoR",
                        column: x => x.IdTipoR,
                        principalTable: "TipoRecurso",
                        principalColumn: "IdTipoR",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AutoresRecursos",
                columns: table => new
                {
                    IdRec = table.Column<int>(type: "int", nullable: false),
                    idAutor = table.Column<int>(type: "int", nullable: false),
                    EsPrincipal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoresRecursos", x => new { x.IdRec, x.idAutor });
                    table.ForeignKey(
                        name: "FK_AutoresRecursos_Autor_idAutor",
                        column: x => x.idAutor,
                        principalTable: "Autor",
                        principalColumn: "idAutor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AutoresRecursos_Recurso_IdRec",
                        column: x => x.IdRec,
                        principalTable: "Recurso",
                        principalColumn: "IdRec",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AutoresRecursos_idAutor",
                table: "AutoresRecursos",
                column: "idAutor");

            migrationBuilder.CreateIndex(
                name: "IX_Recurso_AutoridAutor",
                table: "Recurso",
                column: "AutoridAutor");

            migrationBuilder.CreateIndex(
                name: "IX_Recurso_IdEdit",
                table: "Recurso",
                column: "IdEdit");

            migrationBuilder.CreateIndex(
                name: "IX_Recurso_IdPais",
                table: "Recurso",
                column: "IdPais");

            migrationBuilder.CreateIndex(
                name: "IX_Recurso_IdTipoR",
                table: "Recurso",
                column: "IdTipoR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoresRecursos");

            migrationBuilder.DropTable(
                name: "Recurso");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Editorial");

            migrationBuilder.DropTable(
                name: "Pais");

            migrationBuilder.DropTable(
                name: "TipoRecurso");
        }
    }
}
