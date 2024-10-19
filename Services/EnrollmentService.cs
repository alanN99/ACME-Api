using ACME_Api.MockDB;
using ACME_Api.Models;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace ACME_Api.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly MockDatabase _mockDatabase;

        public EnrollmentService(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public async Task EnrollStudentInCourse(Enrollment enrollment)
        {
            var data = await _mockDatabase.LoadDataAsync();

            var course = data.Courses.FirstOrDefault(c => c.Id == enrollment.CourseId)
                ?? throw new InvalidOperationException("Curso no encontrado.");
            var student = data.Students.FirstOrDefault(s => s.Id == enrollment.StudentId)
                ?? throw new InvalidOperationException("Estudiante no encontrado.");

            var existingEnrollment = data.Enrollments.FirstOrDefault(e => e.StudentId == enrollment.StudentId && e.CourseId == enrollment.CourseId);

            if (existingEnrollment != null)
                throw new InvalidOperationException("El estudiante ya está inscrito en este curso.");

            if (course.RegistrationFee > 0)
            {
                bool isPaymentValid = ValidatePayment(enrollment.PaymentToken, enrollment.PaymentAmount, course.RegistrationFee);

                if (!isPaymentValid)
                    throw new InvalidOperationException("Validación de pago falló. No se pudo inscribir al estudiante.");
            }           

            await _mockDatabase.SaveDataEnrollmentAsync(enrollment);
        }

        private bool ValidatePayment(string paymentToken, decimal paymentAmount, decimal requiredAmount)
        {
            return paymentAmount >= requiredAmount && !string.IsNullOrWhiteSpace(paymentToken);
        }
    }

}
