using System;
using System.Collections.Generic;
using System.Text;

namespace DamuMed.Models
{
    public class Patient : Entity
    {
        public string Name { get; set; }
        public string IIN { get; set; }
    }
}
