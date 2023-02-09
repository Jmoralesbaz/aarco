using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Examen.Marcas.Data
{
    public partial class Descripciones
    {
        public int Id { get; set; }
        public int Descripcion { get; set; }
        public int Modelo { get; set; }
        public int SubMarca { get; set; }
        public bool? Activo { get; set; }

        public virtual CatDescripciones DescripcionNavigation { get; set; }
        public virtual CatModelos ModeloNavigation { get; set; }
        public virtual SubMarcas SubMarcaNavigation { get; set; }
    }
}
