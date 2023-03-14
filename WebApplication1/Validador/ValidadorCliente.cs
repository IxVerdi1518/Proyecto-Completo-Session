using WebApplication1.Helper;
using WebApplication1.Models;

namespace WebApplication1.Validador
{
    public class ValidadorCliente
    {
        public static void ValidaCliente(Cliente cliente)
        {
            ValidateRequest verificar = new ValidateRequest();
            if (!verificar.ValidarCedula(cliente.Identificacion))
            {
                throw new Exception("Cedula Invalida");
            }
            if (cliente.Nombres == "aaa")
            {
                throw new Exception("Nombre Invalido");
            }
        }
    }
}
