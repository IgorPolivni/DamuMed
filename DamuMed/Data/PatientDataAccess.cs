using DamuMed.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DamuMed.Data
{
    public class PatientDataAccess : DbDataAccess<Patient>
    {

        public override void Insert(Patient entity)
        {
            var selectSqlScript = $"INSERT INTO Patients(patientId, [Name], IIN) VALUES ('{entity.Id}', '{entity.Name}', '{entity.IIN}');";

            var command = factory.CreateCommand();
            command.CommandText = selectSqlScript;
            command.Connection = connection;

            command.ExecuteNonQuery();
            command.Dispose();
        }

        public ICollection<Patient> SelectBy(string name, string IIN)
        {
            List<Patient> patients = new List<Patient>();

            var selectSqlScript = $"SELECT * FROM Patients WHERE [Name] = '{name}' And IIN = '{IIN}'";

            var command = factory.CreateCommand();
            command.CommandText = selectSqlScript;
            command.Connection = connection;

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    patients.Add(new Patient
                    {
                        Id = Guid.Parse(dataReader["PatientId"].ToString()),
                        Name = dataReader["name"].ToString(),
                        IIN = dataReader["IIN"].ToString(),
                    });
                }
            }

            command.Dispose();

            return patients;
        }

    }
}
