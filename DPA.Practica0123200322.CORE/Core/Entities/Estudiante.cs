using System;
using System.Collections.Generic;

namespace DPA.Practica0123200322.CORE.Core.Entities;

public partial class Estudiante
{
    public int Id { get; set; }

    public string Paterno { get; set; } = null!;

    public string? Materno { get; set; }

    public string Nombres { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string Correo { get; set; } = null!;

    public int CarreraId { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual Carrera Carrera { get; set; } = null!;
}
