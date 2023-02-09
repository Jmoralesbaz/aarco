using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Examen.Marcas.Data
{
    public partial class CatSubMarcas
    {
        public CatSubMarcas()
        {
            SubMarcas = new HashSet<SubMarcas>();
        }

        public int Id { get; set; }
        public string SubMarca { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<SubMarcas> SubMarcas { get; set; }
    }
}
