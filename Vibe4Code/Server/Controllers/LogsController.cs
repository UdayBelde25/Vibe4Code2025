using Microsoft.AspNetCore.Mvc;
using Server.Services.Interfaces;
using Server.Services.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLogRequest request, CancellationToken cancellationToken)
        {
            var result = await _logService.CreateAsync(request, cancellationToken);
            if (!result.Success)
                return BadRequest(new { error = result.Error });
            return CreatedAtAction(nameof(Create), new { id = result.Data!.Id }, result.Data);
        }
    }
}


