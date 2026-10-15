using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Models
{
    public class PatientDto
    {
        public int PatientId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
    }
}
