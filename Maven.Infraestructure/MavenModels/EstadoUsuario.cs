using System;
using System.Collections.Generic;

namespace Maven.Infraestructure.MavenModels;

public partial class EstadoUsuario
{
    public int EstadoUsuarioId { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
