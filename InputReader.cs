using System;

/// <summary>
/// Provides functionalities to read input from the console.
/// </summary>
public class InputReader
{
    /// <summary>
    /// Reads input from the console until the delimiter '===' is encountered twice.
    /// </summary>
    /// <returns>The concatenated input string.</returns>
    public static string ReadInput()
    {
        string inputLine;
        string result = string.Empty;
        int delimiterCount = 0;

        while ((inputLine = Console.ReadLine()) != null)
        {
            result += inputLine + "\n";
            if (inputLine.Trim() == "===")
            {
                delimiterCount++;
                if (delimiterCount == 2)
                {
                    break;
                }
            }
        }

        return result;
    }
}
