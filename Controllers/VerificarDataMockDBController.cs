using ACME_Api.MockDB;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]

public class VerificarDataMockDBController : ControllerBase
{
    private readonly MockDatabase _mockDatabase;

    public VerificarDataMockDBController(MockDatabase mockDatabase)
    {
        _mockDatabase = mockDatabase;
    }
    
    [HttpGet]
    [Route("VerificarCarga")]
    public async Task<IActionResult> VerificarCarga()
    {
        var data = await _mockDatabase.LoadDataAsync();
        if (data == null || !data.Students.Any())
        {
            return NotFound("No se pudo cargar la base de datos o no hay estudiantes.");
        }

        return Ok(data.Students);
    }    
}
