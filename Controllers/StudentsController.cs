using ACME_Api.Models;
using ACME_Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;

    public StudentsController(IStudentService studentService, IMapper mapper)
    {
        _studentService = studentService;
        _mapper = mapper;
    }

    // Endpoint para obtener listado de estudiantes registrados
    [HttpGet]
    [Route("ObtenerEstudiantes")]
    public IActionResult GetAllStudents()
    {
        var students = _studentService.GetAllStudents();
        var studentsDTO = _mapper.Map<IEnumerable<StudentDTO>>(students.Result);

        return Ok(studentsDTO);
    }

    // Endpoint para obtener un estudiante determinado por su id numérico
    [HttpGet]
    [Route("ObtenerEstudiante/{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var student = await _studentService.GetStudentById(id);

        if (student == null || student.Id == 0)
            return NotFound($"No se encontró un estudiante con Id {id}");
        
        var studentDTO = _mapper.Map<StudentDTO>(student);
        return Ok(studentDTO);
    }

    //Endpoint para registrar un nuevo estudiante
    [HttpPost]
    [Route("RegistrarEstudiante")]
    public IActionResult RegisterStudent([FromBody] StudentDTO request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var student = _mapper.Map<Student>(request);
        try
        {
            _studentService.RegisterStudent(student);
            return Ok("Estudiante registrado correctamente.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
