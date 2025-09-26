using Server.Models;
using Server.Services.Models;

namespace Server.Services.Interfaces
{
    public interface ILogService
    {
        Task<ServiceResult<LogEntry>> CreateAsync(CreateLogRequest request, CancellationToken cancellationToken = default);
    }
}


