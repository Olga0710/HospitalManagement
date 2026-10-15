using Dapper;
using HospitalManagement.Models;
using Npgsql;
using System.Collections.Generic;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly NpgsqlConnection _connection;
    private readonly NpgsqlTransaction _transaction;

    public AppointmentRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
    {
        _connection = connection;
        _transaction = transaction;
    }

    public IEnumerable<PatientAppointmentDto> GetPatientAppointments(int patientId)
    {
        // Читання суворо через розріз даних (View), створений у базі
        string query = "SELECT * FROM \"vw_patientappointments\" WHERE patientid = @PatientId";
        return _connection.Query<PatientAppointmentDto>(query, new { PatientId = patientId }, _transaction);
    }
}