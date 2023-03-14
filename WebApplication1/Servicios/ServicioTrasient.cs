namespace WebApplication1.Servicios
{
    public class ServicioTrasient
    {
        Guid _guid = Guid.NewGuid();
        public Guid ObtenerGuid()
        {
            return _guid;
        }
    }
}
