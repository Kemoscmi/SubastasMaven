using System;
using System.Collections.Generic;

namespace Maven.Infraestructure.MavenModels;

public partial class EstadoSubasta
{
    public int EstadoSubastaId { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Subasta> Subasta { get; set; } = new List<Subasta>();
}
