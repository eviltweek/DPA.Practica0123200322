using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA.Practica0123200322.CORE.Core.DTOs
{
    public class EstudianteDTO
    {
        public int Id { get; set; }

        public string? Nombres { get; set; }

        public string? Paterno { get; set; }

        public string? CarreraId { get; set; }

        public class EstudianteListDTO
        {
            public int Id { get; set; }
            public string? Nombres { get; set; }
            public string? Paterno { get; set; }
        }

        public class EstudianteCreateDTO
        {
            public string? Nombres { get; set; }
        }
    }
}
