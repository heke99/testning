using System;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;

public class BadWords
{
    private static string[] badWords;

    static BadWords()
    {
        string badWordsFilePath = Path.Combine("Backend", "json", "bad-words.json");

        
        {
            // parsea json
            string jsonContent = File.ReadAllText(badWordsFilePath);
            badWords = JsonSerializer.Deserialize<string[]>(jsonContent);
        }
        
    }

    public static string RemoveBadWords(string text, string replacement)
    {
        foreach (string badWord in badWords)
        {
            string pattern = @"\b" + Regex.Escape(badWord) + @"\b";
            text = Regex.Replace(text, pattern, replacement, RegexOptions.IgnoreCase);
        }
        return text;
    }

    public static void Main()
    {
       
        {
            string text = "This is a badWord.";
            string replacement = "HejHej";

            string cleanedText = RemoveBadWords(text, replacement);
            Console.WriteLine(cleanedText);
        }

       
    }
}
