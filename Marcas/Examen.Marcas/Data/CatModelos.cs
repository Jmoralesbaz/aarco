using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Examen.Marcas.Data
{
    public partial class CatModelos
    {
        public CatModelos()
        {
            Descripciones = new HashSet<Descripciones>();
            Modelos = new HashSet<Modelos>();
        }

        public int Id { get; set; }
        public string Modelo { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<Descripciones> Descripciones { get; set; }
        public virtual ICollection<Modelos> Modelos { get; set; }
    }
}
