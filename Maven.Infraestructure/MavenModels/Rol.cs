using System;
using System.Collections.Generic;

namespace Maven.Infraestructure.MavenModels;

public partial class Rol
{
    public int RolId { get; set; }

    public string NombreRol { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
