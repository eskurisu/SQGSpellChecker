using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// Provides functionalities to output results to the console.
/// </summary>
public class ConsoleOutput
{
    /// <summary>
    /// Outputs the list of processed text lines to the console.
    /// </summary>
    /// <param name="lines">The lines to output.</param>
    /// <param name="asJson">if set to <c>true</c> outputs the lines in JSON format.</param>
    public void Output(List<string> lines, bool asJson)
    {
        if (asJson)
        {
            string jsonOutput = JsonConvert.SerializeObject(lines, Formatting.Indented);
            Console.WriteLine(jsonOutput);
        }
        else
        {
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
