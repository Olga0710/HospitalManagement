using Dapper;
using HospitalManagement.Models;
using Npgsql;
using System;
using System.Collections.Generic;

public class PatientRepository : IPatientRepository
{
    private readonly NpgsqlConnection _connection;
    private readonly NpgsqlTransaction _transaction;

    public PatientRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
    {
        _connection = connection;
        _transaction = transaction;
    }

    public IEnumerable<PatientDto> GetAllPatients()
    {
        // Вибірка з таблиці пацієнтів
        string query = "SELECT patientid, firstname, lastname, dateofbirth, gender, phone FROM \"Patients\"";
        return _connection.Query<PatientDto>(query, transaction: _transaction);
    }

    public void AddPatient(string firstname, string lastname, DateTime dob, string gender, string phone, string address)
    {
        string sql = "CALL \"sp_addpatient\"(@p_firstname, @p_lastname, @p_dob, @p_gender, @p_phone, @p_address)";

        _connection.Execute(sql, new
        {
            p_firstname = firstname,
            p_lastname = lastname,
            p_dob = dob,
            p_gender = gender,
            p_phone = phone,
            p_address = address
        }, _transaction);
    }
    public void AdmitPatient(int patientId, int bedId)
    {
        string sql = "CALL \"sp_admitpatient\"(@p_patientid, @p_bedid, @p_admissiondate)";
        _connection.Execute(sql, new
        {
            p_patientid = patientId,
            p_bedid = bedId,
            p_admissiondate = DateTime.Now
        }, _transaction);
    }
}