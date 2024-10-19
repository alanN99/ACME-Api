using System;
using System.ComponentModel.DataAnnotations;

namespace ACME_Api.Models
{
    public class CourseDTO
    {
        public string Name { get; set; }
        public decimal RegistrationFee { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }
    }
}
