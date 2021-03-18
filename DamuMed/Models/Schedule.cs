using System;
using System.Collections.Generic;
using System.Text;

namespace DamuMed.Models
{
    public class Schedule : Entity
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
