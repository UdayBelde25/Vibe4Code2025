using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Server.Services.Models;

namespace Server.Services
{
    public class LogService : ILogService
    {
        private readonly IGenericRepository<LogEntry> _logRepository;

        public LogService(IGenericRepository<LogEntry> logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<ServiceResult<LogEntry>> CreateAsync(CreateLogRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Action))
                return ServiceResult<LogEntry>.Fail("Action is required.");

            if (string.IsNullOrWhiteSpace(request.UserType))
                return ServiceResult<LogEntry>.Fail("UserType is required.");

            if (request.UserId <= 0)
                return ServiceResult<LogEntry>.Fail("UserId must be positive.");

            var log = new LogEntry
            {
                Action = request.Action.Trim(),
                UserType = request.UserType.Trim(),
                UserId = request.UserId,
                Timestamp = DateTime.UtcNow
            };

            await _logRepository.AddAsync(log, cancellationToken);
            await _logRepository.SaveChangesAsync(cancellationToken);
            return ServiceResult<LogEntry>.Ok(log);
        }
    }
}


