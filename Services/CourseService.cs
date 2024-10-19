using ACME_Api.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using ACME_Api.MockDB;
using System.Threading.Tasks;

namespace ACME_Api.Services
{
    public class CourseService : ICourseService
    {
        private readonly MockDatabase _mockDatabase;
        private readonly IStudentService _studentService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly EnrollmentValidator _enrollmentValidator;

        public CourseService(IStudentService studentService, MockDatabase mockDatabase, IEnrollmentService enrollmentService, EnrollmentValidator enrollmentValidator)
        {
            _mockDatabase = mockDatabase;
            _studentService = studentService;
            _enrollmentService = enrollmentService;
            _enrollmentValidator = enrollmentValidator;
        }

        public async Task CreateCourse(Course course)
        {
            await _mockDatabase.LoadDataAsync();

            if (course.StartDate >= course.EndDate)
                throw new InvalidOperationException("La fecha de inicio debe ser anterior a la fecha de fin del curso.");            

            await _mockDatabase.SaveDataCoursesAsync(course);
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            var data = await _mockDatabase.LoadDataAsync();
            return data.Courses;
        }

        public async Task<IEnumerable<Course>> GetCoursesWithinDateRange(DateTime startDate, DateTime endDate)
        {
            var data = await _mockDatabase.LoadDataAsync();
            return data.Courses.Where(c => c.StartDate >= startDate && c.EndDate <= endDate) ?? throw new InvalidOperationException("Curso no encontrado.");
        }

        public async Task<Course> GetCourseById(int id)
        {
            var data = await _mockDatabase.LoadDataAsync();
            return data.Courses.FirstOrDefault(c => c.Id == id) ?? throw new InvalidOperationException("Curso no encontrado.");
        }

        public async Task EnrollStudentInCourse(Enrollment enrollment)
        {
            await _enrollmentService.EnrollStudentInCourse(enrollment);
        }
    }
}
