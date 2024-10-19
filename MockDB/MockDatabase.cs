using ACME_Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ACME_Api.MockDB
{
    public class MockDatabase
    {
        private readonly string _filePath = "MockDB/school_management.json";

        public async Task<MockDBModel> LoadDataAsync()
        {
            if (!File.Exists(_filePath))
                return new MockDBModel(); // Retorna un nuevo modelo si el archivo no existe

            try
            {
                var json = await File.ReadAllTextAsync(_filePath);
                return JsonSerializer.Deserialize<MockDBModel>(json) ?? new MockDBModel();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                throw new InvalidOperationException("Error loading data.");
            }
        }

        public async Task SaveDataStudentsAsync(Student student)
        {
            MockDBModel mockData = await LoadDataAsync(); // Carga los datos existentes

            int lastId = mockData.Students.Any() ? mockData.Students.Max(s => s.Id) : 0;
            student.Id = lastId + 1;
            student.IsAdult = true;

            // Añadir el nuevo estudiante a la lista de estudiantes
            mockData.Students.Add(student);

            // Serializar la estructura completa de MockDatabaseModel nuevamente a JSON
            var json = JsonSerializer.Serialize(mockData, new JsonSerializerOptions { WriteIndented = true });

            // Guardar los datos actualizados en el archivo JSON
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task SaveDataCoursesAsync(Course course)
        {
            MockDBModel mockData = await LoadDataAsync(); // Carga los datos existentes

            int lastId = mockData.Courses.Any() ? mockData.Courses.Max(s => s.Id) : 0;
            course.Id = lastId + 1;
            // Añadir el nuevo curso a la lista de cursos
            mockData.Courses.Add(course);

            // Serializar la estructura completa de MockDatabaseModel nuevamente a JSON
            var json = JsonSerializer.Serialize(mockData, new JsonSerializerOptions { WriteIndented = true });

            // Guardar los datos actualizados en el archivo JSON
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task SaveDataEnrollmentAsync(Enrollment enrollment)
        {
            MockDBModel mockData = await LoadDataAsync(); // Carga los datos existentes

            int lastId = mockData.Enrollments.Any() ? mockData.Enrollments.Max(e => e.Id) : 0;
            enrollment.Id = lastId + 1;
            enrollment.IsPaymentComplete = true;

            // Añadir la nueva inscripción a la lista de inscripciones
            mockData.Enrollments.Add(enrollment);

            // Serializar la estructura completa de MockDatabaseModel nuevamente a JSON
            var json = JsonSerializer.Serialize(mockData, new JsonSerializerOptions { WriteIndented = true });

            // Guardar los datos actualizados en el archivo JSON
            await File.WriteAllTextAsync(_filePath, json);
        }
    }

    public class MockDBModel
    {
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }

}
