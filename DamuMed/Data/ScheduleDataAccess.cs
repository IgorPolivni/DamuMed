using DamuMed.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DamuMed.Data
{
    public class ScheduleDataAccess : DbDataAccess<Schedule>
    {
        public override void Insert(Schedule entity)
        {
            var selectSqlScript = $"insert into schedules(scheduleId ,doctorId ,patientId ,[year] ,[month] ,[day] ,[hour] ) values('{entity.Id}','{entity.DoctorId}','{entity.PatientId}','{entity.DateTime.Year}','{entity.DateTime.Month}','{entity.DateTime.Day}','{entity.DateTime.Hour});";

            var command = factory.CreateCommand();
            command.CommandText = selectSqlScript;
            command.Connection = connection;

            command.ExecuteNonQuery();
            command.Dispose();
        }


        public List<Schedule> SelectBy(Guid doctorId, DateTime date)
        {
            List<Schedule> schedules = new List<Schedule>();

            var selectSqlScript = $"SELECT * FROM schedules WHERE doctorId = '{doctorId}' And date = {date.Day} And year = {date.Year} AND month = {date.Month} AND hour = {date.hour};";

            var command = factory.CreateCommand();
            command.CommandText = selectSqlScript;
            command.Connection = connection;

            using (var dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    DateTime scheduleDatetime = new DateTime();
                    
                    scheduleDatetime.AddDays(int.Parse(dataReader["day"].ToString()));
                    scheduleDatetime.AddYears(int.Parse(dataReader["year"].ToString()));
                    scheduleDatetime.AddMonths(int.Parse(dataReader["month"].ToString()));
                    scheduleDatetime.AddHours(int.Parse(dataReader["hour"].ToString()));

                    schedules.Add(new Schedule
                    {
                        Id = Guid.Parse(dataReader["scheduleId"].ToString()),
                        DoctorId = Guid.Parse(dataReader["doctorId"].ToString()),
                        PatientId = Guid.Parse(dataReader["patientId"].ToString()),
                        DateTime =  scheduleDatetime
                    });
                }
            }

            command.Dispose();

            return schedules;
        }

    }
}
