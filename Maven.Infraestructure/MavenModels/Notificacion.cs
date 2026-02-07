using System;
using System.Collections.Generic;

namespace Maven.Infraestructure.MavenModels;

public partial class Notificacion
{
    public long NotificacionId { get; set; }

    public int UsuarioId { get; set; }

    public int? SubastaId { get; set; }

    public string Tipo { get; set; } = null!;

    public string Mensaje { get; set; } = null!;

    public bool Leida { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Subasta? Subasta { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
