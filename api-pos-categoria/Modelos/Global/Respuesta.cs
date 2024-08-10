namespace api_pos_categoria.Modelos.Global
{
    public class Respuesta<TExito, TMensaje>
    {
        public bool Exito { get; set; }
        public int CodigoEstado { get; set; }
        public TExito Objeto { get; set; }
        public TMensaje Mensaje { get; set; }

        public Respuesta<TExito, TMensaje> RespuestaExito(TExito exito)
        {
            return new Respuesta<TExito, TMensaje>
            {
                Exito = true,
                CodigoEstado = 200,
                Objeto = exito
            };
        }

        public Respuesta<TExito, TMensaje> RespuestaError(int codigoEstado, TMensaje mensaje)
        {
            return new Respuesta<TExito, TMensaje>
            {
                Exito = false,
                CodigoEstado = codigoEstado,
                Mensaje = mensaje
            };
        }
    }
}
