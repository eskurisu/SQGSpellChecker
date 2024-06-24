using System;

/// <summary>
/// Provides functionalities for calculating edit distances between words.
/// </summary>
public class EditDistanceCalculator
{
    /// <summary>
    /// Determines whether the given dictionary word is a valid two-edit correction for the input word.
    /// </summary>
    /// <param name="inputWord">The input word.</param>
    /// <param name="dictionaryWord">The dictionary word.</param>
    /// <returns><c>true</c> if the dictionary word is a valid two-edit correction for the input word; otherwise, <c>false</c>.</returns>
    public static bool IsValidTwoEditCorrection(string inputWord, string dictionaryWord)
    {
        int inputLength = inputWord.Length;
        int dictionaryLength = dictionaryWord.Length;
        int editCount = 0;

        for (int i = 0, j = 0; i < inputLength && j < dictionaryLength;)
        {
            if (inputWord[i] != dictionaryWord[j])
            {
                editCount++;
                if (editCount > 2) return false;

                if (inputLength > dictionaryLength) i++;
                else if (dictionaryLength > inputLength) j++;
                else { i++; j++; }
            }
            else
            {
                i++;
                j++;
            }
        }

        editCount += Math.Abs(inputLength - dictionaryLength);

        if (editCount == 2 && ((inputLength > 1 && inputWord[0] == inputWord[1]) || (dictionaryLength > 1 && dictionaryWord[0] == dictionaryWord[1])))
        {
            return false;
        }

        return editCount <= 2;
    }

    /// <summary>
    /// Calculates the edit distance between the input word and the dictionary word.
    /// </summary>
    /// <param name="inputWord">The input word.</param>
    /// <param name="dictionaryWord">The dictionary word.</param>
    /// <returns>The edit distance between the input word and the dictionary word.</returns>
    public static int CalculateEditDistance(string inputWord, string dictionaryWord)
    {
        int inputLength = inputWord.Length;
        int dictionaryLength = dictionaryWord.Length;
        int[,] distanceMatrix = new int[inputLength + 1, dictionaryLength + 1];

        for (int i = 0; i <= inputLength; i++)
        {
            for (int j = 0; j <= dictionaryLength; j++)
            {
                if (i == 0)
                {
                    distanceMatrix[i, j] = j;
                }
                else if (j == 0)
                {
                    distanceMatrix[i, j] = i;
                }
                else if (inputWord[i - 1] == dictionaryWord[j - 1])
                {
                    distanceMatrix[i, j] = distanceMatrix[i - 1, j - 1];
                }
                else
                {
                    distanceMatrix[i, j] = 1 + Math.Min(distanceMatrix[i - 1, j], distanceMatrix[i, j - 1]);
                }
            }
        }

        return distanceMatrix[inputLength, dictionaryLength];
    }
}