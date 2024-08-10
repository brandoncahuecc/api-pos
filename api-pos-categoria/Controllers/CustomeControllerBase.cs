using api_pos_categoria.Modelos.Global;
using Microsoft.AspNetCore.Mvc;

namespace api_pos_categoria.Controllers
{
    public class CustomeControllerBase : ControllerBase
    {
        public IActionResult RespuestaPerzonalizada<TExito, TMensaje>(Respuesta<TExito, TMensaje> respuesta)
        {
            return StatusCode(respuesta.CodigoEstado, respuesta.Exito ? respuesta.Objeto : respuesta.Mensaje);
        }
    }
}
