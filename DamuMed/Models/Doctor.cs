using System;
using System.Collections.Generic;
using System.Text;

namespace DamuMed.Models
{
    public class Doctor : Entity
    {
        public string Name { get; set; }
        public Guid DestinationId { get; set; }

        public override string ToString()
        {
            return $"Имя врача: {Name}";
        }

    }



}
