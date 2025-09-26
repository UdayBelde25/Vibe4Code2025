using Server.Models;

namespace Server.Repositories.Interfaces
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    }
}


