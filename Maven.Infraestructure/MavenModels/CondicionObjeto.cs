using System;
using System.Collections.Generic;

namespace Maven.Infraestructure.MavenModels;

public partial class CondicionObjeto
{
    public int CondicionObjetoId { get; set; }

    public string NombreCondicion { get; set; } = null!;

    public virtual ICollection<Joya> Joya { get; set; } = new List<Joya>();
}
