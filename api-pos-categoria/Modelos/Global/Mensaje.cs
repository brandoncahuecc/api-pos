namespace api_pos_categoria.Modelos.Global
{
    public class Mensaje
    {
        public string CodigoInterno { get; set; } = string.Empty;
        public string MensajeUsuario { get; set; } = string.Empty;
        public string InformacionTecnica { get; set; } = string.Empty;

        public Mensaje(string codigoInterno, string mensajeUsuario)
        {
            CodigoInterno = codigoInterno;
            MensajeUsuario = mensajeUsuario;
        }

        public Mensaje(string codigoInterno, string mensajeUsuario, Exception exception)
        {
            CodigoInterno = codigoInterno;
            MensajeUsuario = mensajeUsuario;
            InformacionTecnica = exception.Message;
        }
    }
}
