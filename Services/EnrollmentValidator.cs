using ACME_Api.MockDB;
using ACME_Api.Models;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace ACME_Api.Services
{
    public class EnrollmentValidator
    {
        private readonly MockDatabase _mockDatabase;

        public EnrollmentValidator(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }

        public async Task ValidateEnrollment(Enrollment enrollment)
        {
            var data = await _mockDatabase.LoadDataAsync();

            var course = data.Courses.FirstOrDefault(c => c.Id == enrollment.CourseId)
                ?? throw new InvalidOperationException("Curso no encontrado.");

            var student = data.Students.FirstOrDefault(c => c.Id == enrollment.StudentId)
                ?? throw new InvalidOperationException("Estudiante no encontrado.");

            var existingEnrollment = data.Enrollments.FirstOrDefault(e =>
                e.StudentId == enrollment.StudentId && e.CourseId == enrollment.CourseId);

            if (existingEnrollment != null)
                throw new InvalidOperationException("El estudiante ya está inscrito en este curso.");
        }
    }

}
