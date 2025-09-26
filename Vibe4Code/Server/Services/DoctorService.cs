using System.Text.RegularExpressions;
using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Server.Services.Models;

namespace Server.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<ServiceResult<Doctor>> RegisterAsync(DoctorRegistrationRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return ServiceResult<Doctor>.Fail("Name is required.");

            if (!string.IsNullOrWhiteSpace(request.Email) && !IsValidEmail(request.Email))
                return ServiceResult<Doctor>.Fail("Invalid email format.");

            if (request.Rating is < 0 or > 5)
                return ServiceResult<Doctor>.Fail("Rating must be between 0 and 5.");

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                var exists = await _doctorRepository.EmailExistsAsync(request.Email, cancellationToken);
                if (exists)
                    return ServiceResult<Doctor>.Fail("Email already exists.");
            }

            var doctor = new Doctor
            {
                Name = request.Name.Trim(),
                Specialization = request.Specialization?.Trim(),
                PhoneNumber = request.PhoneNumber?.Trim(),
                Email = request.Email?.Trim(),
                Rating = request.Rating,
                CreatedAt = DateTime.UtcNow
            };

            await _doctorRepository.AddAsync(doctor, cancellationToken);
            await _doctorRepository.SaveChangesAsync(cancellationToken);

            return ServiceResult<Doctor>.Ok(doctor);
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}


