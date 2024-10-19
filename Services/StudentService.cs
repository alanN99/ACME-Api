using ACME_Api.MockDB;
using ACME_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACME_Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly MockDatabase _mockDatabase;

        public StudentService(MockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
                
        public async Task RegisterStudent(Student student)
        {
            await _mockDatabase.LoadDataAsync();            

            await _mockDatabase.SaveDataStudentsAsync(student);          
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var data = await _mockDatabase.LoadDataAsync();
            return data.Students;
        }

        public async Task<Student> GetStudentById(int id)
        {
            var data = await _mockDatabase.LoadDataAsync();
            return data.Students.FirstOrDefault(s => s.Id == id) ?? throw new InvalidOperationException("El estudiante no existe.");
        }
    }
}
