using System;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace ACME_Api.Models
{
    public class CourseDTO
    {
        public string Name { get; set; }
        //[(decimal.MinValue > 0, ErrorMessage = "La edad debe ser mayor o igual a 18.")]
        public decimal RegistrationFee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
