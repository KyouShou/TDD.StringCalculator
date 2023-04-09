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

            var splitedNumbers = inputString.Split(',', '.' , '\n').ToList();

            for (int i = 0; i < splitedNumbers.Count; i++)
            {
                string? num = splitedNumbers[i];
                if (string.IsNullOrEmpty(num))
                {
                    var errorIndex = 0;

                    for (int j = 0; j < i; j++)
                    {
                        errorIndex += splitedNumbers[j].Length;
                        errorIndex++;
                    }

                    throw new Exception($"位置{errorIndex}應為數字");
                }
            }

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
