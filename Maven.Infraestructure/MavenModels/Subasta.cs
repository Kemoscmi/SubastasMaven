using System;
using System.Collections.Generic;

namespace Maven.Infraestructure.MavenModels;

public partial class Subasta
{
    public int SubastaId { get; set; }

    public int JoyaId { get; set; }

    public int VendedorId { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaCierre { get; set; }

    public decimal PrecioBase { get; set; }

    public decimal IncrementoMinimo { get; set; }

    public int EstadoSubastaId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaPublicacion { get; set; }

    public virtual EstadoSubasta EstadoSubasta { get; set; } = null!;

    public virtual Joya Joya { get; set; } = null!;

    public virtual ICollection<Notificacion> Notificacion { get; set; } = new List<Notificacion>();

    public virtual Pago? Pago { get; set; }

    public virtual ICollection<Puja> Puja { get; set; } = new List<Puja>();

    public virtual SubastaResultado? SubastaResultado { get; set; }

    public virtual Usuario Vendedor { get; set; } = null!;
}
