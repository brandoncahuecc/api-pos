using api_pos_biblioteca.Modelos.Global;
using api_pos_biblioteca.Modelos;
using api_pos_persona.Persistencia;

namespace api_pos_persona.Servicios
{
    public class PersonaServicio : IPersonaServicio
    {
        private readonly IPersonaPersistencia _persistencia;

        public PersonaServicio(IPersonaPersistencia persistencia)
        {
            _persistencia = persistencia;
        }

        public async Task<Respuesta<Persona, Mensaje>> ActualizarPersona(Persona request)
        {
            var resultado = await _persistencia.ActualizarPersona(request);
            return resultado;
        }

        public async Task<Respuesta<Persona, Mensaje>> CrearPersona(Persona request)
        {
            var resultado = await _persistencia.CrearPersona(request);
            return resultado;
        }

        public async Task<Respuesta<Mensaje, Mensaje>> EliminarPersona(int id)
        {
            var resultado = await _persistencia.EliminarPersona(id);
            return resultado;
        }

        public async Task<Respuesta<Persona, Mensaje>> ObtenerPersona(int id)
        {
            var resultado = await _persistencia.ObtenerPersona(id);
            return resultado;
        }

        public async Task<Respuesta<List<Persona>, Mensaje>> ObtenerPersonas()
        {
            var resultado = await _persistencia.ObtenerPersonas();
            return resultado;
        }
    }

    public interface IPersonaServicio
    {
        Task<Respuesta<Persona, Mensaje>> ActualizarPersona(Persona request);
        Task<Respuesta<Persona, Mensaje>> CrearPersona(Persona request);
        Task<Respuesta<Mensaje, Mensaje>> EliminarPersona(int id);
        Task<Respuesta<Persona, Mensaje>> ObtenerPersona(int id);
        Task<Respuesta<List<Persona>, Mensaje>> ObtenerPersonas();
    }
}
