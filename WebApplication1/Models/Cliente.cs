using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{

    public class Cliente
    {
        private String identificacion;
        private String nombres;
        private String apellidos;
        private String pais;
        private int edad;
        public Cliente(string identificacion, string nombres, string apellidos, string pais, int edad)
        {
            this.identificacion = identificacion;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.pais = pais;
            this.edad = edad;
        }
        public Cliente()
        {

        }
        [Required(ErrorMessage = "El campo Identificacion es Obligatorio")]
        [StringLength(10, ErrorMessage = "La cedula debe contener 10 digitos", MinimumLength = 10)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo debe ingresar letras, No números")]

        public string Identificacion { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Solo debe ingresar letras, No números")]
        public string Nombres { get; set; }
        [RegularExpression("^[A-Za-z ]+$", ErrorMessage = "Solo debe ingresar letras, No números")]
        public string Apellidos { get; set; } = "";
        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo debe ingresar letras, No números")]
        [Range(0,120)]
        [Display(Name = "Edad con display")]
        public int Edad { get; set; }=0;
        [Required]
        public string Pais { get; set; }="";
    }

}