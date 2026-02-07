using System;
using System.Collections.Generic;

namespace Maven.Infraestructure.MavenModels;

public partial class EstadoObjeto
{
    public int EstadoObjetoId { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Joya> Joya { get; set; } = new List<Joya>();
}
