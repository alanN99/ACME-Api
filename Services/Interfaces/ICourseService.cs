using ACME_Api.Models;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace ACME_Api.Services
{
    public interface ICourseService
    {
        Task CreateCourse(Course course);
        Task<IEnumerable<Course>> GetAllCourses();
        Task<IEnumerable<Course>> GetCoursesWithinDateRange(DateTime startDate, DateTime endDate);
        Task<Course> GetCourseById(int id);
        // Método para inscribir a un estudiante en un curso
        Task EnrollStudentInCourse(Enrollment enrollment);
    }
}
