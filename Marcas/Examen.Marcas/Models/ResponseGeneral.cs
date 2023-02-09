using System.Text;

namespace Examen.Marcas.Models
{
    public class ResponseGeneral<T>
    {
        private bool existeError = false;
        private StringBuilder errorDescription = new StringBuilder();
        public int Codigo { get; set; }
        public bool ExisteError
        {
            get { return (DescripcionError.ToString().Length > 0); }
            set { existeError = value; }
        }
        public string DescripcionError
        {
            get { return errorDescription.ToString().Trim(); }
            set { if (value.Trim().Length > 0) errorDescription.AppendLine(value); }
        }
        public T ContenidoAdicional { get; set; }

        public void AsignaInformacionErrores<TExterno>(ResponseGeneral<TExterno> respuestaGeneralExterna)
        {
            if (respuestaGeneralExterna != null)
            {
                DescripcionError = respuestaGeneralExterna.DescripcionError;
                Codigo = respuestaGeneralExterna.Codigo;
                if (ExisteError)
                {
                    ContenidoAdicional = default(T);
                }
            }
        }
        public void AsignaObjetoRespuestaGeneral<TExterno>(ResponseGeneral<TExterno> respuestaGeneralExterna)
        {
            if (respuestaGeneralExterna != null)
            {
                ContenidoAdicional = (T)(object)respuestaGeneralExterna.ContenidoAdicional;
                DescripcionError = respuestaGeneralExterna.DescripcionError;
                Codigo = respuestaGeneralExterna.Codigo;
            }
            else
            {
                DescripcionError = respuestaGeneralExterna.DescripcionError;
                Codigo = respuestaGeneralExterna.Codigo;
            }
        }

    }
}
