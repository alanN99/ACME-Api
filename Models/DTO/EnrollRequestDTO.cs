namespace ACME_Api.Models
{
    public class EnrollRequestDTO
    {
        public int StudentId { get; set; }  // ID del estudiante
        public int CourseId { get; set; }   // ID del curso

        // Información relacionada con el pago
        public bool IsPaymentRequired { get; set; }  // Si es necesario pagar la tarifa
        public decimal PaymentAmount { get; set; }   // Monto de la inscripción
        public string PaymentToken { get; set; }     // Token de pago o ID de transacción de la pasarela de pago
        public string PaymentMethod { get; set; }    // Método de pago (tarjeta de crédito, débito, etc.)
    }

}
