using WebApplication1.Models;
using WebApplication1.Validador;

namespace WebApplication1.Sesion
{
    public class ServicioSingleton
    {
        Guid _guid = Guid.NewGuid();
        public Guid ObtenerGuid()
        {
            return _guid;
        }
    }
}
