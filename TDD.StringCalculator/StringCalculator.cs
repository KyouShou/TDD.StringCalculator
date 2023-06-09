﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TDD.StringCalculator
{
    public class StringCalculator
    {
        private string _splitPattern = ",|\\.|\n";
        Regex _regex;
        private delegate void CheckValidMethods(List<string> splitedNumbers);

        public StringCalculator()
        {
            this._regex = new Regex(_splitPattern);
        }

        public void SetCustomSeparators(string customSeparators)
        {
            _splitPattern = Regex.Escape(customSeparators);
            _regex = new Regex(_splitPattern);
        }

        public string Add(string inputString)
        {
            if (String.IsNullOrEmpty(inputString))
                return "O";

            var splitedNumbers = SplitStringWithPattern(inputString);

            CheckValid(splitedNumbers);

            return CalculateSum(splitedNumbers).ToString();
        }

        private List<string> SplitStringWithPattern(string inputString)
        {
            var splitedNumbers = _regex.Split(inputString).ToList();
            return splitedNumbers;
        }

        private void CheckValid(List<string> splitedNumbers)
        {
            List<CheckValidMethods> checkValidMethodList = GetCheckValidMethodList();

            List<Exception> exceptions = FindExceptions(splitedNumbers, checkValidMethodList);

            if (exceptions.Count > 0)
            {
                throw new AggregateException("發生一個以上的錯誤", exceptions);
            }
        }

        private List<Exception> FindExceptions(List<string> splitedNumbers, List<CheckValidMethods> checkValidMethodList)
        {
            List<Exception> exceptions = new List<Exception>();

            foreach (var method in checkValidMethodList)
            {
                try
                {
                    method(splitedNumbers);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            return exceptions;
        }

        private List<CheckValidMethods> GetCheckValidMethodList()
        {
            return new List<CheckValidMethods>()
            {
              CheckLastCharNotSeparator,
              CheckNoContinuousSeparators,
              CheckInvalidSeparators,
              CheckNegativeNumbers
            };
        }

        private void CheckNegativeNumbers(List<string> splitedNumbers)
        {
            foreach (var num in splitedNumbers)
            {
                if (int.Parse(num) < 0)
                {
                    throw new Exception("輸入的數字不可為負數");
                }
            }
        }

        private void CheckInvalidSeparators(List<string> splitedNumbers)
        {
            var regex = new Regex("^[0-9]*$|-");

            foreach (var num in splitedNumbers)
            {
                if (!regex.IsMatch(num))
                {
                    throw new Exception("含有無法使用的分割字元");
                }
            }
        }

        private void CheckLastCharNotSeparator(List<string> splitedNumbers)
        {
            var lastStringInList = splitedNumbers[splitedNumbers.Count - 1];
            if (string.IsNullOrEmpty(lastStringInList))
            {
                throw new Exception("最後一個字元不可為分割字元");
            }
        }

        private void CheckNoContinuousSeparators(List<string> splitedNumbers)
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
