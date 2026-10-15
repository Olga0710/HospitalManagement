using HospitalManagement.Models;
using System.Collections.Generic;

public interface IAppointmentRepository
{
    IEnumerable<PatientAppointmentDto> GetPatientAppointments(int patientId);
}