using System;
using System.Collections.Generic;

namespace Maven.Infraestructure.MavenModels;

public partial class CategoriaJoya
{
    public int CategoriaJoyaId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Joya> Joya { get; set; } = new List<Joya>();
}
