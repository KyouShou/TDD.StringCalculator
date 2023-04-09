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

            var splitedNumbers = inputString.Split(',', '.', '\n').ToList();

            CheckNoContinuousSplitChar(splitedNumbers);

            return CalculateSum(splitedNumbers).ToString();
        }

        private void CheckNoContinuousSplitChar(List<string> splitedNumbers)
        {
            for (int i = 0; i < splitedNumbers.Count; i++)
            {
                if (string.IsNullOrEmpty(splitedNumbers[i]))
                {
                    int errorIndex = FindErrorIndex(splitedNumbers, i);

                    throw new Exception($"位置{errorIndex}應為數字");
                }
            }
        }

        private int FindErrorIndex(List<string> splitedNumbers, int i)
        {
            var errorIndex = 0;

            for (int j = 0; j < i; j++)
            {
                errorIndex += splitedNumbers[j].Length;
                errorIndex++;
            }

            return errorIndex;
        }

        private decimal CalculateSum(List<string> splitedNumbers)
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
