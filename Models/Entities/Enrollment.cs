using System;
using System.ComponentModel.DataAnnotations;

namespace ACME_Api.Models
{
    public class Enrollment
    {
        public int Id { get; set; }             // ID de la inscripción
        public int StudentId { get; set; }      // ID del estudiante
        public int CourseId { get; set; }       // ID del curso
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EnrollmentDate { get; set; }  // Fecha de inscripción
        public bool IsPaymentComplete { get; set; }   // Si el pago fue completado
        // Nuevas propiedades para la validación del pago
        public string PaymentToken { get; set; }    // Token de pago
        public decimal PaymentAmount { get; set; }  // Monto del pago
                
    }        

}
