using System;
using System.Collections.Generic;

namespace Maven.Infraestructure.MavenModels;

public partial class Puja
{
    public long PujaId { get; set; }

    public int SubastaId { get; set; }

    public int CompradorId { get; set; }

    public decimal MontoOfertado { get; set; }

    public DateTime FechaHora { get; set; }

    public virtual Usuario Comprador { get; set; } = null!;

    public virtual Subasta Subasta { get; set; } = null!;

    public virtual ICollection<SubastaResultado> SubastaResultado { get; set; } = new List<SubastaResultado>();
}
