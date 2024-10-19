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
        private readonly EnrollmentValidator _enrollmentValidator;

        public EnrollmentService(MockDatabase mockDatabase, EnrollmentValidator enrollmentValidator)
        {
            _mockDatabase = mockDatabase;
            _enrollmentValidator = enrollmentValidator;
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

            int lastId = data.Enrollments.Any() ? data.Enrollments.Max(e => e.Id) : 0;
            enrollment.Id = lastId + 1;
            enrollment.IsPaymentComplete = true;

            await _mockDatabase.SaveDataEnrollmentAsync(enrollment);
        }

        private bool ValidatePayment(string paymentToken, decimal paymentAmount, decimal requiredAmount)
        {
            return paymentAmount >= requiredAmount && !string.IsNullOrWhiteSpace(paymentToken);
        }
    }

}
