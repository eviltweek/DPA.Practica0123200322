using DPA.Practica0123200322.CORE.Core.Entities;
using DPA.Practica0123200322.CORE.Core.Interfaces;
using DPA.Practica0123200322.CORE.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Practica0123200322.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteRepository _context;
        public EstudianteController(IEstudianteRepository context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstudiantes()
        {
            var estudiantes = await _context.GetEstudiantes();
            return Ok(estudiantes);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstudianteById(int id)
        {
            var estudiante = await _context.GetEstudianteById(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            return Ok(estudiante);
        }
        [HttpPost]
        public async Task<IActionResult> AddEstudiante([FromBody] Estudiante estudiante)
        {
            await _context.AddEstudiante(estudiante);
            return CreatedAtAction(nameof(GetEstudianteById), new { id = estudiante.Id }, estudiante);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstudiante(int id, [FromBody] Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                return BadRequest();
            }
            await _context.UpdateEstudiante(estudiante);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            await _context.DeleteEstudiante(id);
            return NoContent();
        }
    }
}
