using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD.StringCalculator
{
    public class StringCalculator
    {
        public string Add(string inputString)
        {
            if (String.IsNullOrEmpty(inputString))
                return "O";
            throw new Exception("Should not be here.");
        }
    }
}
