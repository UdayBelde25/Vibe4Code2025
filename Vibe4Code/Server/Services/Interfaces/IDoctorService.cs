using Server.Models;
using Server.Services.Models;

namespace Server.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<ServiceResult<Doctor>> RegisterAsync(DoctorRegistrationRequest request, CancellationToken cancellationToken = default);
    }
}


