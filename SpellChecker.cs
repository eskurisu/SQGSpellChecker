using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Provides spell checking functionalities.
/// </summary>
public class SpellChecker
{
    /// <summary>
    /// Gets the dictionary set.
    /// </summary>
    private HashSet<string> DictionarySet { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SpellChecker"/> class.
    /// </summary>
    /// <param name="dictionary">The dictionary to use for spell checking.</param>
    public SpellChecker(List<string> dictionary)
    {
        DictionarySet = new HashSet<string>(dictionary, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Processes the text lines and returns the corrected text.
    /// </summary>
    /// <param name="textLines">The text lines to process.</param>
    /// <param name="useParallelProcessing">if set to <c>true</c> uses parallel processing.</param>
    /// <returns>The list of processed text lines.</returns>
    public List<string> ProcessTextLines(List<string> textLines, bool useParallelProcessing)
    {
        var processedLines = new List<string>();

        if (useParallelProcessing)
        {
            var processedLinesConcurrent = new ConcurrentBag<(int Index, string Line)>();

            Parallel.ForEach(textLines.Select((line, index) => (line, index)), item =>
            {
                string processedLine = ProcessLine(item.line);
                processedLinesConcurrent.Add((item.index, processedLine));
            });

            processedLines = processedLinesConcurrent.OrderBy(x => x.Index).Select(x => x.Line).ToList();
        }
        else
        {
            foreach (var line in textLines)
            {
                processedLines.Add(ProcessLine(line));
            }
        }

        return processedLines;
    }

    /// <summary>
    /// Processes a single line of text and returns the corrected line.
    /// </summary>
    /// <param name="line">The line to process.</param>
    /// <returns>The corrected line.</returns>
    private string ProcessLine(string line)
    {
        var words = line.Split(' ');
        List<string> processedWords = new List<string>();

        foreach (var word in words)
        {
            if (DictionarySet.Contains(word))
            {
                processedWords.Add(word);
            }
            else
            {
                var corrections = TextProcessor.FindCorrections(word, DictionarySet);
                if (corrections.Count == 0)
                {
                    processedWords.Add($"{{{word}?}}");
                }
                else if (corrections.Count == 1)
                {
                    processedWords.Add(corrections.First());
                }
                else
                {
                    processedWords.Add($"{{{string.Join(" ", corrections)}}}");
                }
            }
        }

        return string.Join(" ", processedWords);
    }
}
