using System.Text.RegularExpressions;
using Server.Models;
using Server.Repositories.Interfaces;
using Server.Services.Interfaces;
using Server.Services.Models;

namespace Server.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<ServiceResult<Patient>> RegisterAsync(PatientRegistrationRequest request, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return ServiceResult<Patient>.Fail("Name is required.");

            if (request.Age < 0 || request.Age > 130)
                return ServiceResult<Patient>.Fail("Age must be between 0 and 130.");

            if (!string.IsNullOrWhiteSpace(request.Email) && !IsValidEmail(request.Email))
                return ServiceResult<Patient>.Fail("Invalid email format.");

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                var exists = await _patientRepository.EmailExistsAsync(request.Email, cancellationToken);
                if (exists)
                    return ServiceResult<Patient>.Fail("Email already exists.");
            }

            var patient = new Patient
            {
                Name = request.Name.Trim(),
                Age = request.Age,
                PhoneNumber = request.PhoneNumber?.Trim(),
                Address = request.Address?.Trim(),
                Email = request.Email?.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            await _patientRepository.AddAsync(patient, cancellationToken);
            await _patientRepository.SaveChangesAsync(cancellationToken);

            return ServiceResult<Patient>.Ok(patient);
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


