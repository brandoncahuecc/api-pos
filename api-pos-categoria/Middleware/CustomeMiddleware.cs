using api_pos_categoria.Modelos.Global;
using Newtonsoft.Json;

namespace api_pos_categoria.Middleware
{
    public class CustomeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomeMiddleware> _logger;

        public CustomeMiddleware(RequestDelegate next, ILogger<CustomeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation("Solicitud api: " + context.Request.Method + " " + context.Request.Path);
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tuvimos problemas no controlados en la aplicación");
                Mensaje mensaje = new("", "Tuvimos problemas no controlados, contacte al administrador", ex);

                var respuesta = context.Response;
                respuesta.StatusCode = 500;
                respuesta.ContentType = "application/json";
                await respuesta.WriteAsync(JsonConvert.SerializeObject(mensaje));
            }
            _logger.LogInformation("Respuesta api: " + context.Response.StatusCode);
        }
    }
}
