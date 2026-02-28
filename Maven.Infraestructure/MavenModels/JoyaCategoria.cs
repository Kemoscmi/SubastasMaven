using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maven.Infraestructure.MavenModels
{
    public partial class JoyaCategoria
    {
        public int JoyaId { get; set; }

        public int CategoriaJoyaId { get; set; }

        public virtual CategoriaJoya CategoriaJoya { get; set; } = null!;

        public virtual Joya Joya { get; set; } = null!;
    }
}
