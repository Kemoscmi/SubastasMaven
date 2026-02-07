using System;
using System.Collections.Generic;

namespace Maven.Infraestructure.MavenModels;

public partial class JoyaImagen
{
    public int JoyaImagenId { get; set; }

    public int JoyaId { get; set; }

    public string UrlImagen { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public virtual Joya Joya { get; set; } = null!;
}
