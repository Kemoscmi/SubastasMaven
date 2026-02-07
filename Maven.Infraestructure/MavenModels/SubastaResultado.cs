using System;
using System.Collections.Generic;

namespace Maven.Infraestructure.MavenModels;

public partial class SubastaResultado
{
    public int SubastaId { get; set; }

    public int? GanadorId { get; set; }

    public long? PujaGanadoraId { get; set; }

    public decimal? MontoFinal { get; set; }

    public DateTime FechaCierre { get; set; }

    public virtual Usuario? Ganador { get; set; }

    public virtual Puja? PujaGanadora { get; set; }

    public virtual Subasta Subasta { get; set; } = null!;
}
