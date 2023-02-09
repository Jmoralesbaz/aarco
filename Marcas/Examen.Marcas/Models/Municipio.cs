namespace Examen.Marcas.Models
{
    public class Municipio
    {
        public int iIdMunicipio { get; set; }
        public int iMunicipioEstado { get; set; }
        public int iClaveMunicipioCepomex { get; set; }
        public string sMunicipio { get; set; }
        public Estado Estado { get; set; }
    }
}