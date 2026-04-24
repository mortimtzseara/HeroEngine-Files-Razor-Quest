namespace HeroEngine.Tools
{
    public static class Utils
    {
        /// <summary>
        /// Reads a non-negative integer from the console, displaying a prompt and repeating until valid input is
        /// entered.
        /// </summary>
        /// <param name="message">The message displayed to prompt the user for input.</param>
        /// <param name="errorMessage">The message displayed when the user enters invalid input.</param>
        /// <returns>A non-negative integer entered by the user.</returns>
        public static int ReadInt(string message, string errorMessage)
        {
            int result;
            Console.WriteLine(message);
            while (!int.TryParse(Console.ReadLine(), out result) || result < 0)
            {
                Console.WriteLine(errorMessage);
            }
            return result;
        }

        /// <summary>
        /// Prompts the user to enter a positive double-precision floating-point number, displaying a message and
        /// repeating until valid input is provided.
        /// </summary>
        /// <param name="message">The message displayed to prompt the user for input.</param>
        /// <param name="errorMessage">The message displayed when the input is invalid.</param>
        /// <returns>A positive double-precision floating-point number entered by the user.</returns>
        public static double ReadDouble(string message, string errorMessage)
        {
            double result;
            Console.WriteLine(message);
            while (!double.TryParse(Console.ReadLine(), out result) || result <= 0)
            {
                Console.WriteLine(errorMessage);
            }
            return result;
        }

        /// <summary>
        /// Prompts the user to enter a string with a minimum length, displaying an error message until valid input is
        /// provided.
        /// </summary>
        /// <param name="message">The message displayed to prompt the user for input.</param>
        /// <param name="errorMessage">The message displayed when the input is invalid.</param>
        /// <param name="minLength">The minimum required length of the input string.</param>
        /// <returns>The validated user input string.</returns>
        public static string ReadString(string message, string errorMessage, int minLength)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine() ?? string.Empty;
            while (string.IsNullOrWhiteSpace(input) || input.Length < minLength)
            {
                Console.WriteLine(errorMessage);
                input = Console.ReadLine() ?? string.Empty;
            }
            return input;
        }

        /// <summary>
        /// Prompts the user to enter a Boolean value and returns the parsed result.
        /// </summary>
        /// <param name="message">The message displayed to prompt the user for input.</param>
        /// <param name="errorMessage">The message displayed when the input is not a valid Boolean value.</param>
        /// <returns>The Boolean value entered by the user.</returns>
        public static bool ReadBool(string message, string errorMessage)
        {
            bool result;
            Console.WriteLine(message);
            while (!bool.TryParse(Console.ReadLine()?.ToLower().Trim(), out result))
            {
                Console.WriteLine(errorMessage);
            }
            return result;
        }
    }
}