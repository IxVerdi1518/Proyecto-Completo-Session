namespace WebApplication1.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool KeepLogeedIn { get; set; }
    }
}
