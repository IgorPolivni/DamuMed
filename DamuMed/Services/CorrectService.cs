using System;
using System.Collections.Generic;
using System.Text;

namespace DamuMed.Services
{
    public class CorrectService
    {
        public static bool CorrectIIN(string IIN)
        {
            if (IIN.Length == 12)   return true;
            else                    return false;
        }
    }
}
