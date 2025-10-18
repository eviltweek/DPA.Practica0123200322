using DPA.Practica0123200322.CORE.Core.Entities;

namespace DPA.Practica0123200322.CORE.Core.Interfaces
{
    public interface IEstudianteRepository
    {
        Task AddEstudiante(Estudiante estudiante);
        Task DeleteEstudiante(int id);
        IEnumerable<Estudiante> GetAll();
        Task<Estudiante> GetEstudianteById(int id);
        Task<IEnumerable<Estudiante>> GetEstudiantes();
        Task UpdateEstudiante(Estudiante estudiante);
    }
}