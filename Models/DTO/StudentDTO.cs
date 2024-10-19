using System.ComponentModel.DataAnnotations;

namespace ACME_Api.Models
{
    public class StudentDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Name { get; set; }
        [Range(18, int.MaxValue, ErrorMessage = "La edad debe ser mayor o igual a 18.")]
        public int Age { get; set; }

    }
}
