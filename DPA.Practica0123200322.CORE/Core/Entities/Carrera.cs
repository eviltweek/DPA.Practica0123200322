using System;
using System.Collections.Generic;
using DPA.Practica0123200322.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DPA.Practica0123200322.CORE.Core.Entities;

public class Carrera
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Estudiante> Estudiante { get; set; } = new List<Estudiante>();

    private readonly UniversidadContext _context;

    public Carrera(UniversidadContext context)
    {
        _context = context;
    }

    public IEnumerable<Carrera> GetAll()
    {
        var carreras = _context.Carrera.ToList();
        return carreras;
    }

    public async Task<IEnumerable<Carrera>> GetCarreras()
    {
        var carreras = await _context.Carrera.ToListAsync();
        return carreras;
    }

    public async Task<Carrera> GetCarreraById(int id)
    {
        var category = await _context
                            .Carrera
                            .Where(c => c.Id == id).FirstOrDefaultAsync();

        return category;
    }

    public async Task AddCarrera(Carrera carrera)
    {
        _context.Carrera.Add(carrera);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCarrera(Carrera carrera)
    {
        _context.Carrera.Update(carrera);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCarrera(int id)
    {
        var carrera = await GetCarreraById(id);
        if (carrera != null)
        {
            _context.Carrera.Remove(carrera);
            await _context.SaveChangesAsync();
        }
    }


}
