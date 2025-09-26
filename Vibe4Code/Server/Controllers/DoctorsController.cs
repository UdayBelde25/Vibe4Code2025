using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;
using Server.Services.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DoctorRegistrationRequest request, CancellationToken cancellationToken)
        {
            var result = await _doctorService.RegisterAsync(request, cancellationToken);
            if (!result.Success)
                return BadRequest(new { error = result.Error });
            return CreatedAtAction(nameof(Register), new { id = result.Data!.Id }, result.Data);
        }
    }
}


