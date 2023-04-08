using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TDD.StringCalculator
{
    public class StringCalculator
    {
        public string Add(string inputString)
        {
            if (String.IsNullOrEmpty(inputString))
                return "O";

            var splitedNumbers = inputString.Split(',', '.').ToList();

            var sum = 0m;
            foreach (var number in splitedNumbers)
            {
                sum += decimal.Parse(number);
            }

            return sum.ToString();
        }
    }
}
