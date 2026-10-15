using System;

namespace HospitalManagement.Models
{
    public class PatientAppointmentDto
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string SpecializationName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}