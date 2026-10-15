using System;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string connectionString = "Host=localhost;Port=5432;Database=hospital_db;Username=berezhna;Password=student";

        Console.WriteLine("=== СИСТЕМА УПРАВЛІННЯ ЛІКАРНЕЮ ===");
        Console.WriteLine("Підключення до бази даних PostgreSQL...\n");

        using (var uow = new UnitOfWork(connectionString))
        {
            try
            {
               
                Console.WriteLine("1. Додавання нового пацієнта...");
                uow.Patients.AddPatient("Олена", "Ковальчук", new DateTime(1995, 8, 12), "Female", "+380671112233", "м. Київ, вул. Хрещатик, 1");

                Console.WriteLine("2. Виконання збереженої процедури госпіталізації (sp_admitpatient)...");
                uow.Patients.AdmitPatient(1, 1); 

            
                Console.WriteLine("3. Список пацієнтів у базі даних:");
                var patients = uow.Patients.GetAllPatients();
                foreach (var p in patients)
                {
                    Console.WriteLine($"   - Пацієнт: {p.Firstname} {p.Lastname} (Тел: {p.Phone})");
                }
                Console.WriteLine();

                
                Console.WriteLine("4. Читання даних про прийоми пацієнтів через View (vw_patientappointments):");
                var appointments = uow.Appointments.GetPatientAppointments(1);
                foreach (var app in appointments)
                {
                    Console.WriteLine($"   -> Прийом ID: {app.AppointmentId} | Пацієнт: {app.PatientName} | Лікар: {app.DoctorName} ({app.SpecializationName}) | Дата: {app.AppointmentDate} | Статус: {app.Status}");
                }

                uow.Commit();
                Console.WriteLine("\n-> Транзакцію успішно зафіксовано (Commit)!");
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Console.WriteLine($"\n Помилка виконання транзакції: {ex.Message}");
            }
        }

        Console.WriteLine("\nРоботу програми завершено.");
    }
}