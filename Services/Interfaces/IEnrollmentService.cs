using ACME_Api.Models;
using System.Threading.Tasks;

namespace ACME_Api.Services
{
    public interface IEnrollmentService
    {
        Task EnrollStudentInCourse(Enrollment enrollment);
    }
}
