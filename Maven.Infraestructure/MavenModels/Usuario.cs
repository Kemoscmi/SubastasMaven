using System;
using System.Collections.Generic;

namespace Maven.Infraestructure.MavenModels;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Correo { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public int RolId { get; set; }

    public int EstadoUsuarioId { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual EstadoUsuario EstadoUsuario { get; set; } = null!;

    public virtual ICollection<Joya> Joya { get; set; } = new List<Joya>();

    public virtual ICollection<Notificacion> Notificacion { get; set; } = new List<Notificacion>();

    public virtual ICollection<Puja> Puja { get; set; } = new List<Puja>();

    public virtual Rol Rol { get; set; } = null!;

    public virtual ICollection<Subasta> Subasta { get; set; } = new List<Subasta>();

    public virtual ICollection<SubastaResultado> SubastaResultado { get; set; } = new List<SubastaResultado>();
}
