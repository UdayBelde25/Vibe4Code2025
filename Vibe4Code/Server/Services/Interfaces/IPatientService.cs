using Server.Models;
using Server.Services.Models;

namespace Server.Services.Interfaces
{
    public interface IPatientService
    {
        Task<ServiceResult<Patient>> RegisterAsync(PatientRegistrationRequest request, CancellationToken cancellationToken = default);
    }
}


