using Server.Models;

namespace Server.Repositories.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    }
}


