using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Examen.Marcas.Data
{
    public partial class Datos
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Submarca { get; set; }
        public long? Modelo { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionId { get; set; }
    }
}
