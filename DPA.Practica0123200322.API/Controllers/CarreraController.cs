using DPA.Practica0123200322.CORE.Core.Entities;
using DPA.Practica0123200322.CORE.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Practica0123200322.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarreraController : ControllerBase
    {
        private readonly Carrera _context;

        public CarreraController(Carrera context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarreras()
        {
            var carreras = await _context.GetCarreras();
            return Ok(carreras);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarreraById(int id)
        {
            var carrera = await _context.GetCarreraById(id);
            if (carrera == null)
            {
                return NotFound();
            }
            return Ok(carrera);
        }

        [HttpPost]
        public async Task<IActionResult> AddCarrera([FromBody] Carrera carrera)
        {
            await _context.AddCarrera(carrera);
            return CreatedAtAction(nameof(GetCarreraById), new { id = carrera.Id }, carrera);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarrera(int id, [FromBody] Carrera carrera)
        {
            if (id != carrera.Id)
            {
                return BadRequest();
            }
            await _context.UpdateCarrera(carrera);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrera(int id)
        {
            await _context.DeleteCarrera(id);
            return NoContent();
        }

    }
}
