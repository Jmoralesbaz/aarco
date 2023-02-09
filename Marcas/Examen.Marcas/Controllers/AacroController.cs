using Examen.Marcas.Data;
using Examen.Marcas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Examen.Marcas.Controllers
{
    public class AacroController : ControladorBase
    {
        public AacroController(ContextExamen _contextExamen) : base(_contextExamen)
        {
        }

        [HttpGet("Marcas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseMarca>))]
        public IActionResult Marcas() {
            var r = new ResponseGeneral<List<ResponseMarca>>() { Codigo = 200 };

            try
            {
                r.ContenidoAdicional = this.contextExamen.Marcas.Where(w => w.Activo ?? false).Select(s => new ResponseMarca { Id = s.Id, Marca = s.Marca }).ToList();
            }
            catch (Exception ex)
            {
                r.Codigo = 500;
                r.DescripcionError = ex.Message;
            }

            return this.ResponseHttp(r);
        }

        [HttpGet("SubMarcas/{marca}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseSubmarca>))]
        public IActionResult SubMarcas(int marca)
        {
            var r = new ResponseGeneral<List<ResponseSubmarca>>() { Codigo = 200 };

            try
            {
                r.ContenidoAdicional = this.contextExamen.SubMarcas.Where(w => (w.Activo ?? false) == true && w.Marca == marca).Select(s => s.SubMarca).Distinct().Join(contextExamen.CatSubMarcas, a => a, b => b.Id, (a, b) => new ResponseSubmarca { Id = b.Id, SubMarca = b.SubMarca }).OrderBy(d => d.SubMarca).ToList();
            }
            catch (Exception ex)
            {
                r.Codigo = 500;
                r.DescripcionError = ex.Message;
            }

            return this.ResponseHttp(r);
        }
        [HttpGet("Modelos/{submarca}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseModelo>))]
        public IActionResult Modelos(int submarca)
        {
            var r = new ResponseGeneral<List<ResponseModelo>>() { Codigo = 200 };

            try
            {
                r.ContenidoAdicional = this.contextExamen.Modelos.Where(w => (w.Activo ?? false) == true && w.SubMarca == submarca).Select(s => s.Modelo).Distinct().Join(contextExamen.CatModelos, a => a, b => b.Id, (a, b) => new ResponseModelo { Id = b.Id, Modelo = b.Modelo }).OrderBy(d => d.Modelo).ToList();
            }
            catch (Exception ex)
            {
                r.Codigo = 500;
                r.DescripcionError = ex.Message;
            }

            return this.ResponseHttp(r);
        }
        [HttpGet("Descripciones/{submarca}/{modelo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseDescripcion>))]
        public IActionResult Descripciones(int submarca, int modelo)
        {
            var r = new ResponseGeneral<List<ResponseDescripcion>>() { Codigo = 200 };

            try
            {
                r.ContenidoAdicional = this.contextExamen.Descripciones.Where(w => (w.Activo ?? false) == true && w.SubMarca == submarca && w.Modelo == modelo).Select(s => s.Descripcion).Distinct().Join(contextExamen.CatDescripciones, a => a, b => b.Id, (a, b) => new ResponseDescripcion { Id = b.Id, Descripcion = b.Descripcion, DescripcionId = b.DescripcionId }).OrderBy(d => d.Descripcion).ToList();
            }
            catch (Exception ex)
            {
                r.Codigo = 500;
                r.DescripcionError = ex.Message;
            }

            return this.ResponseHttp(r);
        }

        [HttpGet("Consultacp/{cp}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Sepomex>))]
        public IActionResult Consultacp(int cp)
        {
            var r = new ResponseGeneral<List<Sepomex>>() { Codigo = 404 };

            try
            {
               var rsepo = this.ejecutaServicioRest<object,ResponseSepomex>(Enums.HttpVervos.GET, ContentType.Json, "https://api-test.aarco.com.mx/api-examen/api/examen/sepomex/" + cp, null, null, true);                
                r.AsignaInformacionErrores(rsepo);
                if (!rsepo.ExisteError) {
                    if (rsepo.ContenidoAdicional.CatalogoJsonString.Trim().Length > 0)
                    {
                        r.Codigo = 200;
                        r.ContenidoAdicional = JsonConvert.DeserializeObject<List<Sepomex>>(rsepo.ContenidoAdicional.CatalogoJsonString);
                    }
                    else {
                        r.Codigo = 404;
                        r.DescripcionError = rsepo.ContenidoAdicional.Error.Descripcion;
                    }
                }
            }
            catch (Exception ex)
            {
                r.Codigo = 500;
                r.DescripcionError = ex.Message;
            }

            return this.ResponseHttp(r);
        }

        

    }
}
