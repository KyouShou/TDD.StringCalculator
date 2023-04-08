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

            return CalculateSum(splitedNumbers).ToString();
        }

        private static decimal CalculateSum(List<string> splitedNumbers)
        {
            var result = 0m;

            foreach (var number in splitedNumbers)
            {
                result += decimal.Parse(number);
            }

            return result;
        }
    }
}
