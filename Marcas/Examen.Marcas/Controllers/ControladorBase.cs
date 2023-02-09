using Examen.Marcas.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using Examen.Marcas.Enums;
using Newtonsoft.Json.Serialization;
using ContentType = Examen.Marcas.Models.ContentType;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Examen.Marcas.Data;

namespace Examen.Marcas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MensageError))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MensageError))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(MensageError))]
    public class ControladorBase : ControllerBase
    {
        protected ContextExamen contextExamen;
        private JsonSerializerSettings jsonSettingsCamelCase;
        public ControladorBase(ContextExamen _contextExamen) {
            contextExamen = _contextExamen;
            jsonSettingsCamelCase = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
        protected IActionResult ResponseHttp<TRespuesta>(ResponseGeneral<TRespuesta> respuestaGeneral)
        {
            IActionResult result;
            switch (respuestaGeneral.Codigo)
            {
                case 200:
                    result = Ok((object)respuestaGeneral.ContenidoAdicional);
                    break;
                case 201:
                    result = StatusCode(201);
                    break;
                case 204:
                    result = NoContent();
                    break;
                default:
                    result = StatusCode(respuestaGeneral.Codigo, new MensageError { Mensaje = respuestaGeneral.DescripcionError });
                    break;
            }
            return result;
        }

        public ResponseGeneral<TSalida> ejecutaServicioRest<TEntrada, TSalida>(HttpVervos method, string contentType, string urlSend, WebHeaderCollection headers, TEntrada body, bool setCamelCaseBody)
        {
            string vervoS = Enum.GetName(typeof(HttpVervos), method).ToUpper();
            string contenido = "";
            string StatusDescription = "";
            ResponseGeneral<TSalida> respuesta = new ResponseGeneral<TSalida>();
            try
            {
                HttpWebRequest request;

                request = WebRequest.Create(urlSend) as HttpWebRequest;
                request.Timeout = 1300000;
                request.Headers = headers ?? new WebHeaderCollection { };
                request.Method = vervoS;
                request.ContentType = contentType;
                if ((method == HttpVervos.POST || method == HttpVervos.PUT) && body != null)
                {
                    switch (contentType)
                    {
                        case ContentType.Json: contenido = ((setCamelCaseBody) ? JsonConvert.SerializeObject(body, jsonSettingsCamelCase) : JsonConvert.SerializeObject(body)); break;
                        case ContentType.Plain:
                        case ContentType.UrlEncode: contenido = body.ToString(); break;

                    }
                    byte[] data = UTF8Encoding.UTF8.GetBytes(contenido);
                    request.ContentLength = data.Length;
                    using (var postStream = request.GetRequestStream())
                    {
                        postStream.Write(data, 0, data.Length);
                    }
                }
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(response.GetResponseStream());
                contenido = reader.ReadToEnd();
                respuesta.Codigo = (int)response.StatusCode;
                StatusDescription = response.StatusDescription;
            }
            catch (WebException wex)
            {
                if (wex.Response is null)
                {
                    respuesta.Codigo = 500;
                    respuesta.DescripcionError = "{url:" + urlSend + ",mensaje:" + wex.Message + "}";
                }
                else
                {
                    var response = (HttpWebResponse)(object)wex.Response;
                    respuesta.Codigo = (int)response.StatusCode;
                    StatusDescription = response.StatusDescription;
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    contenido = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                respuesta.Codigo = 500;
                respuesta.DescripcionError = ex.Message;
            }
            switch (respuesta.Codigo)
            {
                case 200:
                case 201:
                case 202:
                case 204:
                    respuesta.ContenidoAdicional = typeof(TSalida) == typeof(string) ? ((TSalida)(object)contenido) : JsonConvert.DeserializeObject<TSalida>(contenido);
                    break;
                default:
                    respuesta.DescripcionError = $"{StatusDescription} {contenido}";
                    break;
            }
            return respuesta;
        }
    }

}