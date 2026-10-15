using Dapper;
using Npgsql;
using System;

public interface IUnitOfWork : IDisposable
{
    IPatientRepository Patients { get; }
    IAppointmentRepository Appointments { get; }
    void Commit();
    void Rollback();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly NpgsqlConnection _connection;
    private readonly NpgsqlTransaction _transaction;

    public IPatientRepository Patients { get; }
    public IAppointmentRepository Appointments { get; }

    public UnitOfWork(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();

        _transaction = _connection.BeginTransaction();

        Patients = new PatientRepository(_connection, _transaction);
        Appointments = new AppointmentRepository(_connection, _transaction);
    }

    public void Commit() => _transaction.Commit();
    public void Rollback() => _transaction.Rollback();

    public void Dispose()
    {
        _transaction?.Dispose();
        _connection?.Dispose();
    }
}