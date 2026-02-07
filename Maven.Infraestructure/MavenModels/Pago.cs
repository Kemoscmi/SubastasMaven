using System;
using System.Collections.Generic;

namespace Maven.Infraestructure.MavenModels;

public partial class Pago
{
    public int SubastaId { get; set; }

    public int CompradorId { get; set; }

    public int VendedorId { get; set; }

    public decimal Monto { get; set; }

    public int EstadoPagoId { get; set; }

    public DateTime FechaRegistro { get; set; }

    public DateTime? FechaConfirmacion { get; set; }

    public virtual EstadoPago EstadoPago { get; set; } = null!;

    public virtual Subasta Subasta { get; set; } = null!;
}
