using ACME_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACME_Api.Services
{
    public interface IStudentService
    {
        Task RegisterStudent(Student student);
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student> GetStudentById(int id);
    }
}
