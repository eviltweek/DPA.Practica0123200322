using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA.Practica0123200322.CORE.Core.Entities;
using DPA.Practica0123200322.CORE.Core.Interfaces;
using DPA.Practica0123200322.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DPA.Practica0123200322.CORE.Infrastructure.Repositories
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly UniversidadContext _context;

        public EstudianteRepository(UniversidadContext context)
        {
            _context = context;
        }

        public IEnumerable<Estudiante> GetAll()
        {
            var estudiantes = _context.Estudiante.ToList();
            return estudiantes;
        }

        public async Task<IEnumerable<Estudiante>> GetEstudiantes()
        {
            var estudiantes = await _context.Estudiante.ToListAsync();
            return estudiantes;
        }

        public async Task<Estudiante> GetEstudianteById(int id)
        {
            var estudiante = await _context
                                .Estudiante
                                .Where(e => e.Id == id).FirstOrDefaultAsync();
            return estudiante;
        }

        public async Task AddEstudiante(Estudiante estudiante)
        {
            _context.Estudiante.Add(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEstudiante(Estudiante estudiante)
        {
            _context.Estudiante.Update(estudiante);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEstudiante(int id)
        {
            var estudiante = await GetEstudianteById(id);
            if (estudiante != null)
            {
                _context.Estudiante.Remove(estudiante);
                await _context.SaveChangesAsync();
            }
        }



    }
}
