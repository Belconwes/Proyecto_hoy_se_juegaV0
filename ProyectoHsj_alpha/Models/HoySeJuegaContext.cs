using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProyectoHsj_alpha.Models;

public partial class HoySeJuegaContext : DbContext
{
    public HoySeJuegaContext()
    {
    }

    public HoySeJuegaContext(DbContextOptions<HoySeJuegaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditorium> Auditoria { get; set; }

    public virtual DbSet<Cancha> Canchas { get; set; }

    public virtual DbSet<EstadoReserva> EstadoReservas { get; set; }

    public virtual DbSet<HorarioDisponible> HorarioDisponibles { get; set; }

    public virtual DbSet<Notificacion> Notificacions { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){ }
  //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("Server=localhost; DataBase= HOY_SE_JUEGA; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditorium>(entity =>
        {
            entity.HasKey(e => e.IdAuditoria).HasName("PK__AUDITORI__F6FFFB8CD089E2B5");

            entity.ToTable("AUDITORIA");

            entity.Property(e => e.IdAuditoria)
                .ValueGeneratedNever()
                .HasColumnName("ID_auditoria");
            entity.Property(e => e.AccionRealizada)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Accion_Realizada");
            entity.Property(e => e.DescripcionDeAccion)
                .IsUnicode(false)
                .HasColumnName("Descripcion_De_Accion");
            entity.Property(e => e.FechaAuditoria)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Auditoria");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_usuario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__AUDITORIA__ID_us__46E78A0C");
        });

        modelBuilder.Entity<Cancha>(entity =>
        {
            entity.HasKey(e => e.IdCancha).HasName("PK__CANCHA__A2D3DBCF31AC53D9");

            entity.ToTable("CANCHA");

            entity.Property(e => e.IdCancha)
                .ValueGeneratedNever()
                .HasColumnName("ID_cancha");
            entity.Property(e => e.NombreCancha)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Nombre_Cancha");
            entity.Property(e => e.UbicacionCancha)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("Ubicacion_Cancha");
        });

        modelBuilder.Entity<EstadoReserva>(entity =>
        {
            entity.HasKey(e => e.IdEstadoReserva).HasName("PK__ESTADO_R__746A84F0163347B6");

            entity.ToTable("ESTADO_RESERVA");

            entity.Property(e => e.IdEstadoReserva)
                .ValueGeneratedNever()
                .HasColumnName("ID_estado_reserva");
            entity.Property(e => e.NombreEstadoReserva)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Nombre_Estado_Reserva");
        });

        modelBuilder.Entity<HorarioDisponible>(entity =>
        {
            entity.HasKey(e => e.IdHorarioDisponible).HasName("PK__HORARIO___7D5601B713FD2B44");

            entity.ToTable("HORARIO_DISPONIBLE");

            entity.Property(e => e.IdHorarioDisponible)
                .ValueGeneratedNever()
                .HasColumnName("ID_horario_disponible");
            entity.Property(e => e.DisponibleHorario)
                .HasDefaultValue(true)
                .HasColumnName("Disponible_Horario");
            entity.Property(e => e.FechaHorario)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("Fecha_Horario");
            entity.Property(e => e.HoraFin).HasColumnName("Hora_Fin");
            entity.Property(e => e.HoraInicio).HasColumnName("Hora_Inicio");
            entity.Property(e => e.IdCancha).HasColumnName("ID_cancha");

            entity.HasOne(d => d.IdCanchaNavigation).WithMany(p => p.HorarioDisponibles)
                .HasForeignKey(d => d.IdCancha)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HORARIO_D__ID_ca__4D94879B");
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.IdNotificacion).HasName("PK__NOTIFICA__99BC7E5EE9FDA64F");

            entity.ToTable("NOTIFICACION");

            entity.Property(e => e.IdNotificacion)
                .ValueGeneratedNever()
                .HasColumnName("ID_notificacion");
            entity.Property(e => e.FechaEnvioNotificacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Envio_Notificacion");
            entity.Property(e => e.IdReserva).HasColumnName("ID_reserva");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_usuario");
            entity.Property(e => e.MensajeNotificacion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Mensaje_Notificacion");
            entity.Property(e => e.TituloNotificacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Titulo_Notificacion");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Notificacions)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NOTIFICAC__ID_re__5DCAEF64");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Notificacions)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NOTIFICAC__ID_us__5CD6CB2B");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__PAGO__808903ECD6745E92");

            entity.ToTable("PAGO");

            entity.Property(e => e.IdPago)
                .ValueGeneratedNever()
                .HasColumnName("ID_pago");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Pago");
            entity.Property(e => e.IdReserva).HasColumnName("ID_reserva");
            entity.Property(e => e.MontoPago)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Monto_Pago");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PAGO__ID_reserva__59063A47");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__PERMISO__74B1E2192888FD4F");

            entity.ToTable("PERMISO");

            entity.Property(e => e.IdPermiso)
                .ValueGeneratedNever()
                .HasColumnName("ID_permiso");
            entity.Property(e => e.NombrePermiso)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Nombre_Permiso");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.IdReporte).HasName("PK__REPORTE__41AEEB64EA3D5483");

            entity.ToTable("REPORTE");

            entity.Property(e => e.IdReporte)
                .ValueGeneratedNever()
                .HasColumnName("ID_reporte");
            entity.Property(e => e.AnioReporte).HasColumnName("Anio_Reporte");
            entity.Property(e => e.FechaDeReporte)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_De_Reporte");
            entity.Property(e => e.MesReporte).HasColumnName("Mes_Reporte");
            entity.Property(e => e.ReservasRealizadasReporte).HasColumnName("Reservas_Realizadas_Reporte");
            entity.Property(e => e.UsuariosRegistradosReporte).HasColumnName("Usuarios_Registrados_Reporte");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__RESERVA__CD692CB04CD77469");

            entity.ToTable("RESERVA");

            entity.Property(e => e.IdReserva)
                .ValueGeneratedNever()
                .HasColumnName("ID_reserva");
            entity.Property(e => e.FechaReserva).HasColumnName("Fecha_Reserva");
            entity.Property(e => e.IdCancha).HasColumnName("ID_cancha");
            entity.Property(e => e.IdEstadoReserva).HasColumnName("ID_estado_reserva");
            entity.Property(e => e.IdHorarioDisponible).HasColumnName("ID_horario_disponible");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_usuario");

            entity.HasOne(d => d.IdCanchaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdCancha)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RESERVA__ID_canc__5441852A");

            entity.HasOne(d => d.IdEstadoReservaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdEstadoReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RESERVA__ID_esta__52593CB8");

            entity.HasOne(d => d.IdHorarioDisponibleNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdHorarioDisponible)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RESERVA__ID_hora__5535A963");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RESERVA__ID_usua__534D60F1");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__ROL__182A5412735EDC29");

            entity.ToTable("ROL");

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnName("ID_rol");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Nombre_Rol");

            entity.HasMany(d => d.IdPermisos).WithMany(p => p.IdRols)
                .UsingEntity<Dictionary<string, object>>(
                    "PermisoRol",
                    r => r.HasOne<Permiso>().WithMany()
                        .HasForeignKey("IdPermiso")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PERMISO_R__ID_pe__4222D4EF"),
                    l => l.HasOne<Rol>().WithMany()
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PERMISO_R__ID_ro__4316F928"),
                    j =>
                    {
                        j.HasKey("IdRol", "IdPermiso").HasName("PK__PERMISO___9F614A338C7DB628");
                        j.ToTable("PERMISO_ROL");
                        j.IndexerProperty<int>("IdRol").HasColumnName("ID_rol");
                        j.IndexerProperty<int>("IdPermiso").HasColumnName("ID_permiso");
                    });
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO__DF3D4252EC4CA997");

            entity.ToTable("USUARIO");

            entity.HasIndex(e => e.CorreoUsuario, "UQ__USUARIO__A71263111919BEEC").IsUnique();

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("ID_usuario");
            entity.Property(e => e.ApellidoUsuario)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Apellido_Usuario");
            entity.Property(e => e.ContraseniaUsuario)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("Contrasenia_Usuario");
            entity.Property(e => e.CorreoUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Correo_Usuario");
            entity.Property(e => e.IdRol).HasColumnName("ID_rol");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Nombre_Usuario");
            entity.Property(e => e.TelefonoUsuario)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Telefono_Usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USUARIO__ID_rol__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
