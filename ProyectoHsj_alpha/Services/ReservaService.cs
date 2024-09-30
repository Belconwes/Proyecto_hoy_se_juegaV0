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
            // Lógica para obtener los horarios disponibles, por ejemplo:
            return _context.HorariosDisponibles
                           .Where(h => h.IdCancha == canchaId && h.FechaHorario == DateOnly.FromDateTime(fecha))
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
            catch (Exception)
            {
                return false;
            }
        }

        // Verificar si ya existe una reserva para el mismo horario y cancha
        public bool ExisteReserva(int canchaId, DateTime fecha, int horarioId)
        {
            return _context.Reservas.Any(r => r.IdCancha == canchaId && r.FechaReserva == DateOnly.FromDateTime(fecha) && r.IdHorarioDisponible == horarioId);
        }
    }

    
}
