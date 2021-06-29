using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StringCalculator
{
    public class StringCalculator
    {
        private const string delimiterLineStartMarker = "//";
        private const string delimiterLineEndMarker = "\n";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pNumbers"></param>
        /// <returns>int</returns>
        /// <author>Yaseen</author>
        public int Add(string pNumbers)
        {
            //var integerNumbers = null;
            //try
            //{
            //pNumbers =  pNumbers.Replace("\\", @"\");
                if (IsNullEmptyOrWhitespaceFilled(pNumbers))
                    return 0;

                var runtimedelimiter = GetStoredDelimiter(pNumbers);
                pNumbers = RemoveDelimiterDataFrom(pNumbers);

                //Set Max value
                const int MaxNumberToCalculate = 1000;

                var integerNumbers
                    = pNumbers
                        .Split(runtimedelimiter, StringSplitOptions.RemoveEmptyEntries)
                        .Where(stringNumber => Convert.ToInt32(stringNumber) <= MaxNumberToCalculate)
                        .Select(stringNumber => Convert.ToInt32(stringNumber));

                validation(integerNumbers);
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            

            return integerNumbers.Sum();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pNumbers"></param>
        /// <author>Yaseen</author>
        private void validation(IEnumerable<Int32> pNumbers)
        {
            var negativeNumbers
                 = pNumbers
                    .Where(number => number < 0)
                    .Select(number => number)
                    .ToList();

            if( negativeNumbers.Any())
            {
                const string ExceptionMainMessage = "Negatives Not Allowed";

                var exceptionMessage 
                    = $"{ExceptionMainMessage} = {string.Join(",", negativeNumbers)}";

                MessageBox.Show(ExceptionMainMessage, "Error provider",
    MessageBoxButtons.OK, MessageBoxIcon.Error);

               // throw new ArgumentException(exceptionMessage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        /// <author>Yaseen</author>
        private bool IsNullEmptyOrWhitespaceFilled(string numbers)
        {
            return string.IsNullOrEmpty(numbers) || string.IsNullOrWhiteSpace(numbers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        /// <author>Yaseen</author>
        private string[] GetStoredDelimiter(string numbers)
        {
            if(NoDelimiterDataExistsIn(numbers))
                return new string[] { ",", "\n" };

            const string DelimiterOpeningBracket = "[";
            const string DelimiterEndBracket = "[";
            const int DelimiterDataStartPosition = 2;

            int delimiterDataLength 
                = numbers.IndexOf(delimiterLineEndMarker) - DelimiterDataStartPosition;

            var delimiterData = numbers.Substring(DelimiterDataStartPosition, delimiterDataLength);

            if(delimiterData.Contains(DelimiterOpeningBracket) || 
               delimiterData.Contains(DelimiterEndBracket))
            {
                const string DelimiterListSeperator = "][";
                const int DelimiterListStartPosition = 1;
                const int DelimiterListEndAdjustment = 2;

                delimiterData = delimiterData
                    .Substring(DelimiterListStartPosition, delimiterData.Length - DelimiterListEndAdjustment);

                return delimiterData
                        .Split(new string[] { DelimiterListSeperator }, StringSplitOptions.RemoveEmptyEntries);
            }

            const int DefaultDelimiterLength = 1;
            var delimiterString = numbers.Substring(DelimiterDataStartPosition, DefaultDelimiterLength);

            return new string[] { delimiterString };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        /// <author>Yaseen</author> 
        private bool NoDelimiterDataExistsIn(string numbers)
        {
            return !numbers.StartsWith(delimiterLineStartMarker);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        /// <author>Yaseen</author> 
        private string RemoveDelimiterDataFrom(string numbers)
        {
            if (numbers.StartsWith(delimiterLineStartMarker))
            {
                int start = numbers.IndexOf(delimiterLineEndMarker) + 1;
                return numbers.Substring(start);
            }

            return numbers;
        }

    }
}
