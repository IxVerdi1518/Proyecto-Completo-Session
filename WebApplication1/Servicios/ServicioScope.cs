using System;

namespace WebApplication1.Servicios
{
    public class ServicioScope
    {
        Guid _guid = Guid.NewGuid();
        public Guid ObtenerGuid ()
        {
           return _guid;
        }
    }
}
