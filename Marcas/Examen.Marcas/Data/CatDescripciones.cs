using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Examen.Marcas.Data
{
    public partial class CatDescripciones
    {
        public CatDescripciones()
        {
            Descripciones = new HashSet<Descripciones>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionId { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<Descripciones> Descripciones { get; set; }
    }
}
