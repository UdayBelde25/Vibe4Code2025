using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;
using Server.Services.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] PatientRegistrationRequest request, CancellationToken cancellationToken)
        {
            var result = await _patientService.RegisterAsync(request, cancellationToken);
            if (!result.Success)
                return BadRequest(new { error = result.Error });
            return CreatedAtAction(nameof(Register), new { id = result.Data!.Id }, result.Data);
        }
    }
}


