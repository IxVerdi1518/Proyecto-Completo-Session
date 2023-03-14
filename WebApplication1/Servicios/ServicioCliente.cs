using WebApplication1.Models;
using WebApplication1.Repositorio;
using WebApplication1.Validador;

namespace WebApplication1.Servicios
{
    public class ServicioCliente
    {
        public void GuardarCliente(Cliente cliente)
        {
            ValidadorCliente.ValidaCliente(cliente);
            //}RepositorioCliente.InsertarCliente(cliente);
        }
    }
}
