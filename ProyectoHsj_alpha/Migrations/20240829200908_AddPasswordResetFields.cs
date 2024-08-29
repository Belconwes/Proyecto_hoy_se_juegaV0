using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoHsj_alpha.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordResetFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CANCHA",
                columns: table => new
                {
                    ID_cancha = table.Column<int>(type: "int", nullable: false),
                    Nombre_Cancha = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Ubicacion_Cancha = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CANCHA__A2D3DBCF31AC53D9", x => x.ID_cancha);
                });

            migrationBuilder.CreateTable(
                name: "ESTADO_RESERVA",
                columns: table => new
                {
                    ID_estado_reserva = table.Column<int>(type: "int", nullable: false),
                    Nombre_Estado_Reserva = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ESTADO_R__746A84F0163347B6", x => x.ID_estado_reserva);
                });

            migrationBuilder.CreateTable(
                name: "PERMISO",
                columns: table => new
                {
                    ID_permiso = table.Column<int>(type: "int", nullable: false),
                    Nombre_Permiso = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PERMISO__74B1E2192888FD4F", x => x.ID_permiso);
                });

            migrationBuilder.CreateTable(
                name: "REPORTE",
                columns: table => new
                {
                    ID_reporte = table.Column<int>(type: "int", nullable: false),
                    Mes_Reporte = table.Column<byte>(type: "tinyint", nullable: false),
                    Anio_Reporte = table.Column<int>(type: "int", nullable: false),
                    Usuarios_Registrados_Reporte = table.Column<int>(type: "int", nullable: true),
                    Reservas_Realizadas_Reporte = table.Column<int>(type: "int", nullable: true),
                    Fecha_De_Reporte = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__REPORTE__41AEEB64EA3D5483", x => x.ID_reporte);
                });

            migrationBuilder.CreateTable(
                name: "ROL",
                columns: table => new
                {
                    ID_rol = table.Column<int>(type: "int", nullable: false),
                    Nombre_Rol = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ROL__182A5412735EDC29", x => x.ID_rol);
                });

            migrationBuilder.CreateTable(
                name: "HORARIO_DISPONIBLE",
                columns: table => new
                {
                    ID_horario_disponible = table.Column<int>(type: "int", nullable: false),
                    ID_cancha = table.Column<int>(type: "int", nullable: false),
                    Fecha_Horario = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    Hora_Inicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    Hora_Fin = table.Column<TimeOnly>(type: "time", nullable: false),
                    Disponible_Horario = table.Column<bool>(type: "bit", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HORARIO___7D5601B713FD2B44", x => x.ID_horario_disponible);
                    table.ForeignKey(
                        name: "FK__HORARIO_D__ID_ca__4D94879B",
                        column: x => x.ID_cancha,
                        principalTable: "CANCHA",
                        principalColumn: "ID_cancha");
                });

            migrationBuilder.CreateTable(
                name: "PERMISO_ROL",
                columns: table => new
                {
                    ID_rol = table.Column<int>(type: "int", nullable: false),
                    ID_permiso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PERMISO___9F614A338C7DB628", x => new { x.ID_rol, x.ID_permiso });
                    table.ForeignKey(
                        name: "FK__PERMISO_R__ID_pe__4222D4EF",
                        column: x => x.ID_permiso,
                        principalTable: "PERMISO",
                        principalColumn: "ID_permiso");
                    table.ForeignKey(
                        name: "FK__PERMISO_R__ID_ro__4316F928",
                        column: x => x.ID_rol,
                        principalTable: "ROL",
                        principalColumn: "ID_rol");
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID_usuario = table.Column<int>(type: "int", nullable: false),
                    Nombre_Usuario = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Apellido_Usuario = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Correo_Usuario = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Contrasenia_Usuario = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    Telefono_Usuario = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    ID_rol = table.Column<int>(type: "int", nullable: false),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordResetTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__USUARIO__DF3D4252EC4CA997", x => x.ID_usuario);
                    table.ForeignKey(
                        name: "FK__USUARIO__ID_rol__3D5E1FD2",
                        column: x => x.ID_rol,
                        principalTable: "ROL",
                        principalColumn: "ID_rol");
                });

            migrationBuilder.CreateTable(
                name: "AUDITORIA",
                columns: table => new
                {
                    ID_auditoria = table.Column<int>(type: "int", nullable: false),
                    ID_usuario = table.Column<int>(type: "int", nullable: true),
                    Fecha_Auditoria = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Accion_Realizada = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Descripcion_De_Accion = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AUDITORI__F6FFFB8CD089E2B5", x => x.ID_auditoria);
                    table.ForeignKey(
                        name: "FK__AUDITORIA__ID_us__46E78A0C",
                        column: x => x.ID_usuario,
                        principalTable: "USUARIO",
                        principalColumn: "ID_usuario");
                });

            migrationBuilder.CreateTable(
                name: "RESERVA",
                columns: table => new
                {
                    ID_reserva = table.Column<int>(type: "int", nullable: false),
                    ID_usuario = table.Column<int>(type: "int", nullable: false),
                    ID_cancha = table.Column<int>(type: "int", nullable: false),
                    ID_horario_disponible = table.Column<int>(type: "int", nullable: false),
                    Fecha_Reserva = table.Column<DateOnly>(type: "date", nullable: false),
                    ID_estado_reserva = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RESERVA__CD692CB04CD77469", x => x.ID_reserva);
                    table.ForeignKey(
                        name: "FK__RESERVA__ID_canc__5441852A",
                        column: x => x.ID_cancha,
                        principalTable: "CANCHA",
                        principalColumn: "ID_cancha");
                    table.ForeignKey(
                        name: "FK__RESERVA__ID_esta__52593CB8",
                        column: x => x.ID_estado_reserva,
                        principalTable: "ESTADO_RESERVA",
                        principalColumn: "ID_estado_reserva");
                    table.ForeignKey(
                        name: "FK__RESERVA__ID_hora__5535A963",
                        column: x => x.ID_horario_disponible,
                        principalTable: "HORARIO_DISPONIBLE",
                        principalColumn: "ID_horario_disponible");
                    table.ForeignKey(
                        name: "FK__RESERVA__ID_usua__534D60F1",
                        column: x => x.ID_usuario,
                        principalTable: "USUARIO",
                        principalColumn: "ID_usuario");
                });

            migrationBuilder.CreateTable(
                name: "NOTIFICACION",
                columns: table => new
                {
                    ID_notificacion = table.Column<int>(type: "int", nullable: false),
                    ID_usuario = table.Column<int>(type: "int", nullable: false),
                    ID_reserva = table.Column<int>(type: "int", nullable: false),
                    Mensaje_Notificacion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Fecha_Envio_Notificacion = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Titulo_Notificacion = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NOTIFICA__99BC7E5EE9FDA64F", x => x.ID_notificacion);
                    table.ForeignKey(
                        name: "FK__NOTIFICAC__ID_re__5DCAEF64",
                        column: x => x.ID_reserva,
                        principalTable: "RESERVA",
                        principalColumn: "ID_reserva");
                    table.ForeignKey(
                        name: "FK__NOTIFICAC__ID_us__5CD6CB2B",
                        column: x => x.ID_usuario,
                        principalTable: "USUARIO",
                        principalColumn: "ID_usuario");
                });

            migrationBuilder.CreateTable(
                name: "PAGO",
                columns: table => new
                {
                    ID_pago = table.Column<int>(type: "int", nullable: false),
                    ID_reserva = table.Column<int>(type: "int", nullable: false),
                    Monto_Pago = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Fecha_Pago = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PAGO__808903ECD6745E92", x => x.ID_pago);
                    table.ForeignKey(
                        name: "FK__PAGO__ID_reserva__59063A47",
                        column: x => x.ID_reserva,
                        principalTable: "RESERVA",
                        principalColumn: "ID_reserva");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AUDITORIA_ID_usuario",
                table: "AUDITORIA",
                column: "ID_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_HORARIO_DISPONIBLE_ID_cancha",
                table: "HORARIO_DISPONIBLE",
                column: "ID_cancha");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICACION_ID_reserva",
                table: "NOTIFICACION",
                column: "ID_reserva");

            migrationBuilder.CreateIndex(
                name: "IX_NOTIFICACION_ID_usuario",
                table: "NOTIFICACION",
                column: "ID_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_PAGO_ID_reserva",
                table: "PAGO",
                column: "ID_reserva");

            migrationBuilder.CreateIndex(
                name: "IX_PERMISO_ROL_ID_permiso",
                table: "PERMISO_ROL",
                column: "ID_permiso");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVA_ID_cancha",
                table: "RESERVA",
                column: "ID_cancha");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVA_ID_estado_reserva",
                table: "RESERVA",
                column: "ID_estado_reserva");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVA_ID_horario_disponible",
                table: "RESERVA",
                column: "ID_horario_disponible");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVA_ID_usuario",
                table: "RESERVA",
                column: "ID_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_ID_rol",
                table: "USUARIO",
                column: "ID_rol");

            migrationBuilder.CreateIndex(
                name: "UQ__USUARIO__A71263111919BEEC",
                table: "USUARIO",
                column: "Correo_Usuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AUDITORIA");

            migrationBuilder.DropTable(
                name: "NOTIFICACION");

            migrationBuilder.DropTable(
                name: "PAGO");

            migrationBuilder.DropTable(
                name: "PERMISO_ROL");

            migrationBuilder.DropTable(
                name: "REPORTE");

            migrationBuilder.DropTable(
                name: "RESERVA");

            migrationBuilder.DropTable(
                name: "PERMISO");

            migrationBuilder.DropTable(
                name: "ESTADO_RESERVA");

            migrationBuilder.DropTable(
                name: "HORARIO_DISPONIBLE");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "CANCHA");

            migrationBuilder.DropTable(
                name: "ROL");
        }
    }
}
