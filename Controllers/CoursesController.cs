using ACME_Api.Models;
using ACME_Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICourseService _courseService;
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;

    public CoursesController(ICourseService courseService, IStudentService studentService, IMapper mapper)
    {
        _courseService = courseService;
        _studentService = studentService;
        _mapper = mapper;

    }

    // Endpoint para crear un curso
    [HttpPost]
    [Route("CrearCurso")]
    public IActionResult CreateCourse([FromBody] CourseDTO courseDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);        

        var course = _mapper.Map<Course>(courseDto);
        try
        {
            _courseService.CreateCourse(course);
            return Ok("Curso creado correctamente.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Endpoint para obtener la lista de todos los cursos
    [HttpGet]
    [Route("ObtenerCursos")]
    public IActionResult GetCourses()
    {
        var courses = _courseService.GetAllCourses();
        var coursesDto = _mapper.Map<IEnumerable<CourseDTO>>(courses.Result);

        return Ok(coursesDto);
    }

    // Endpoint para obtener cursos en un rango de fechas determinado
    [HttpGet]
    [Route("ObtenerCursosPorRangoFechas")]
    public async Task<IActionResult> GetCoursesWithinDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        try
        {
            var courses = await _courseService.GetCoursesWithinDateRange(startDate, endDate);
            return Ok(courses);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Endpoint para inscribir un estudiante a un curso
    [HttpPost]
    [Route("InscribirEstudiante")]
    public async Task<IActionResult> EnrollStudent([FromBody] EnrollRequestDTO enrollRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Mapear DTO a la clase de dominio `Enrollment`
        var enrollment = _mapper.Map<Enrollment>(enrollRequest);
        enrollment.EnrollmentDate = DateTime.UtcNow;
        string formattedDate = enrollment.EnrollmentDate.ToString("yyyy-MM-dd");

        try
        {
            await _courseService.EnrollStudentInCourse(enrollment);

            // Retornar un objeto detallado con la inscripción
            var enrollmentResponse = new
            {
                StudentId = enrollment.StudentId,
                CourseId = enrollment.CourseId,
                EnrollmentDate = formattedDate,
                Message = "Estudiante inscripto correctamente"
            };

            return Ok(enrollmentResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message });
        }
    }
}
