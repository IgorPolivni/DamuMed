using DamuMed.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DamuMed.Data
{
    public class DoctorDataAccess : DbDataAccess<Doctor>
    {
        public override void Insert(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public List<Doctor> SelectBy()
        {
            List<Doctor> doctors = new List<Doctor>();

            var selectSqlScript = $"SELECT * FROM Doctors";

            var command = factory.CreateCommand();
            command.CommandText = selectSqlScript;
            command.Connection = connection;

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    doctors.Add(new Doctor
                    {
                        Id = Guid.Parse(dataReader["doctorId"].ToString()),
                        Name = dataReader["name"].ToString(),
                        DestinationId = Guid.Parse(dataReader["destinationId"].ToString()),
                    });
                }
            }

            command.Dispose();

            return doctors;
        }
    }
}
