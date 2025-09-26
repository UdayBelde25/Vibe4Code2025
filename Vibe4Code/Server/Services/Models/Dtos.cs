namespace Server.Services.Models
{
    public class PatientRegistrationRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
    }

    public class DoctorRegistrationRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Specialization { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public double? Rating { get; set; }
    }

    public class CreateLogRequest
    {
        public string Action { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public int UserId { get; set; }
    }

    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public T? Data { get; set; }

        public static ServiceResult<T> Ok(T data) => new ServiceResult<T> { Success = true, Data = data };
        public static ServiceResult<T> Fail(string error) => new ServiceResult<T> { Success = false, Error = error };
    }
}


