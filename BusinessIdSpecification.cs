using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CGI
{
    internal class BusinessIdSpecification : ISpecification<string>
    {
        public IEnumerable<string> ReasonsForDissatisfaction => errors;
        private List<string> errors = new List<string>();

        public bool IsSatisfiedBy(string input)
        {
            int checkDigit;
            int[] coefficients = { 7, 9, 10, 5, 8, 4, 2 };
            int[] multiplicationOfArrays = new int[7];
            int sumOfValues = 0;
            int sumMod = 0;
            int counter = 0;
            bool isCorrect = true;

            Regex rxFormat = new Regex(@"^.{7}-.{1}$");
            Regex rxOnlyNumbersOrDash = new Regex(@"^[\d]*\-?[\d]*$");

            //Cheks the format and that there are only numbers and max one dash in the input
            if (!rxFormat.IsMatch(input))
            {
                errors.Add("Format error, use format: XXXXXXX-X ");
                isCorrect = false;
            }

            if (!rxOnlyNumbersOrDash.IsMatch(input))
            {
                errors.Add("Integer expected error, use only integers and one dash to separate the check digit");
                isCorrect = false;
            }

            //Returns false if it fails one of the earlier tests so that the parsing wont get messed up later
            if (isCorrect == false)
            {
                return isCorrect;
            }
            
            checkDigit = int.Parse(input[8].ToString());

            //we multiply the input (except the check digit) with the coefficients array 
            foreach (int i in coefficients)
            {
                multiplicationOfArrays[counter] = i * int.Parse(input[counter].ToString());
                counter++;
            }

            sumOfValues = multiplicationOfArrays.Sum();
            sumMod = sumOfValues % 11;

            //The rest after you take modulo of the sum should not be 1, if the rest is 0 the check digit should be 0, 
            //if the rest is between 2-10 then the check digit should be 11 minus the rest
            if (!(sumMod != 1 && 11 - (sumMod) == checkDigit || sumMod == 0 && checkDigit == 0))
            {
                isCorrect = false;
                errors.Add("Check digit error, make sure the Y-number is correct");
            }

            return isCorrect;
        }
    }
}
