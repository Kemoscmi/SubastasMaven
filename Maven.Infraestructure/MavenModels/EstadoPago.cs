using System;
using System.Collections.Generic;

namespace Maven.Infraestructure.MavenModels;

public partial class EstadoPago
{
    public int EstadoPagoId { get; set; }

    public string NombreEstado { get; set; } = null!;

    public virtual ICollection<Pago> Pago { get; set; } = new List<Pago>();
}
