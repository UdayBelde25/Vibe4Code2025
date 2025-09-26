namespace Server.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Specialization { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public double? Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
    }

    public class LogEntry
    {
        public int Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}


