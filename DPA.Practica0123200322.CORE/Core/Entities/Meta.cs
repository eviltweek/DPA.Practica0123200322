using System;
using System.Collections.Generic;

namespace DPA.Practica0123200322.CORE.Core.Entities;

public partial class Meta
{
    public string Clave { get; set; } = null!;

    public string Valor { get; set; } = null!;

    public DateTime Fecha { get; set; }
}
