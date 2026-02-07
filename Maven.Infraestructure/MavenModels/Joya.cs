using System;
using System.Collections.Generic;

namespace Maven.Infraestructure.MavenModels;

public partial class Joya
{
    public int JoyaId { get; set; }

    public int VendedorId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int EstadoObjetoId { get; set; }

    public int CondicionObjetoId { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual CondicionObjeto CondicionObjeto { get; set; } = null!;

    public virtual EstadoObjeto EstadoObjeto { get; set; } = null!;

    public virtual ICollection<JoyaImagen> JoyaImagen { get; set; } = new List<JoyaImagen>();

    public virtual ICollection<Subasta> Subasta { get; set; } = new List<Subasta>();

    public virtual Usuario Vendedor { get; set; } = null!;

    public virtual ICollection<CategoriaJoya> CategoriaJoya { get; set; } = new List<CategoriaJoya>();
}
