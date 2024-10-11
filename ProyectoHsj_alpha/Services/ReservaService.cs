using ProyectoHsj_alpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoHsj_alpha.Services
{
    public class ReservaService
    {
        private readonly HoySeJuegaContext _context;

        public ReservaService(HoySeJuegaContext context)
        {
            _context = context;
        }

        // Obtener todas las canchas disponibles
        public List<Cancha> ObtenerCanchas()
        {
            return _context.Canchas.ToList();
        }

        // Obtener los horarios disponibles para una fecha y cancha específica
        public List<HorarioDisponible> ObtenerHorariosDisponibles(int canchaId, DateTime fecha)
        {
            var reservas = _context.Reservas
                .Where(r => r.IdCancha == canchaId && r.FechaReserva == DateOnly.FromDateTime(fecha))
                .Select(r => r.IdHorarioDisponible)
                .ToList();

            // Obtener todos los horarios disponibles que no están reservados
            return _context.HorariosDisponibles
                           .Where(h => h.IdCancha == canchaId && h.FechaHorario == DateOnly.FromDateTime(fecha) && !reservas.Contains(h.IdCancha))
                           .ToList();
        }

        // Crear una nueva reserva
        public bool CrearReserva(Reserva nuevaReserva)
        {
            try
            {
                _context.Reservas.Add(nuevaReserva);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // Aquí puedes registrar el error en un log
                Console.WriteLine($"Error al crear la reserva: {ex.Message}");
                return false;
            }
        }

        // Verificar si ya existe una reserva para el mismo horario y cancha
        public bool ExisteReserva(int canchaId, DateTime fecha, int horarioId)
        {
            return _context.Reservas.Any(r =>
                r.IdCancha == canchaId &&
                r.FechaReserva == DateOnly.FromDateTime(fecha) &&
                r.IdHorarioDisponible == horarioId);
        }

        // Obtener todas las reservas
        public List<Reserva> ObtenerReservas()
        {
            return _context.Reservas.ToList();
        }

        // Obtener una reserva por ID
        public Reserva ObtenerReservaPorId(int id)
        {
            return _context.Reservas.Find(id);
        }

        // Eliminar una reserva
        public void EliminarReserva(int id)
        {
            var reserva = ObtenerReservaPorId(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
                _context.SaveChanges();
            }
        }
    }
}
