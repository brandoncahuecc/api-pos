using api_pos_biblioteca.Modelos.Global;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_biblioteca.Controllers
{
    public class CustomeControllerBase : ControllerBase
    {
        public IActionResult RespuestaPerzonalizada<TExito, TMensaje>(Respuesta<TExito, TMensaje> respuesta)
        {
            return StatusCode(respuesta.CodigoEstado, respuesta.Exito ? respuesta.Objeto : respuesta.Mensaje);
        }
    }
}
