using HospitalManagement.Models;
using System.Collections.Generic;

public interface IPatientRepository
{
    IEnumerable<PatientDto> GetAllPatients();
    void AddPatient(string firstname, string lastname, DateTime dob, string gender, string phone, string address);
    void AdmitPatient(int patientId, int bedId);
}