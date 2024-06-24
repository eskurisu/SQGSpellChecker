using System.Collections.Generic;

/// <summary>
/// Provides text processing for finding corrections for misspelled words.
/// </summary>
public class TextProcessor
{
    /// <summary>
    /// Finds corrections for a given word from the dictionary.
    /// </summary>
    /// <param name="inputWord">The word to find corrections for.</param>
    /// <param name="dictionarySet">The dictionary set to use for finding corrections.</param>
    /// <returns>A list of possible corrections for the input word.</returns>
    public static List<string> FindCorrections(string inputWord, HashSet<string> dictionarySet)
    {
        List<string> oneEditCorrections = new List<string>();
        List<string> twoEditCorrections = new List<string>();

        foreach (var dictWord in dictionarySet)
        {
            int distance = EditDistanceCalculator.CalculateEditDistance(inputWord, dictWord);
            if (distance == 1)
            {
                oneEditCorrections.Add(dictWord);
            }
            else if (distance == 2 && EditDistanceCalculator.IsValidTwoEditCorrection(inputWord, dictWord))
            {
                twoEditCorrections.Add(dictWord);
            }
        }

        if (oneEditCorrections.Count > 0)
        {
            return oneEditCorrections;
        }
        return twoEditCorrections;
    }
}