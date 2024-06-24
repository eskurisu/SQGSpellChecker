using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please enter the input. End with '===' on a new line to indicate the end of input.");

        string input = InputReader.ReadInput();

        string[] sections = input.Split(new string[] { "===" }, StringSplitOptions.None);

        if (sections.Length < 2)
        {
            Console.WriteLine("Invalid input format.");
            return;
        }

        string dictionarySection = sections[0];
        List<string> dictionary = dictionarySection.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();

        List<string> textLines = new List<string>();
        for (int i = 1; i < sections.Length; i++)
        {
            textLines.AddRange(sections[i].Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
        }

        SpellChecker spellChecker = new SpellChecker(dictionary);

        Console.WriteLine("Do you want to process text lines in parallel? (yes/no)");
        string userChoice = Console.ReadLine();
        bool useParallelProcessing = userChoice.Equals("yes", StringComparison.OrdinalIgnoreCase);

        List<string> processedLines = spellChecker.ProcessTextLines(textLines, useParallelProcessing);

        Console.WriteLine("Do you want to output the result in JSON format? (yes/no)");
        userChoice = Console.ReadLine();
        bool outputAsJson = userChoice.Equals("yes", StringComparison.OrdinalIgnoreCase);

        ConsoleOutput consoleOutput = new ConsoleOutput();
        consoleOutput.Output(processedLines, outputAsJson);
    }
}
