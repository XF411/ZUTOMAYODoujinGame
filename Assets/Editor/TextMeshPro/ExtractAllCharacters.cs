using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ExtractAllCharacters : EditorWindow
{
    private static readonly HashSet<char> additionalCharacters = new HashSet<char>
    {
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        ' ', '.', ',', '!', '?', ';', ':', '-', '_', '(', ')', '[', ']', '{', '}', '"', '\'', '“', '”', '‘', '’',
        '@', '#', '$', '%', '^', '&', '*', '+', '=', '<', '>', '/', '\\', '|', '~', '`'
    };

    [MenuItem("Tools/Extract Characters")]
    public static void ShowWindow()
    {
        ExtractCharactersFromCSV();
    }

    private static void ExtractCharactersFromCSV()
    {
        string csvPath = "Assets/AssetRaw/Configs/LocalLanguage.csv";
        string chineseOutputPath = "Assets/Editor/TextMeshPro/ChineseForGame.txt";
        string japaneseOutputPath = "Assets/Editor/TextMeshPro/JapaneseForGame.txt";
        string englishOutputPath = "Assets/Editor/TextMeshPro/EnglishForGame.txt";

        if (!File.Exists(csvPath))
        {
            Debug.LogError("CSV file not found at " + csvPath);
            return;
        }

        HashSet<char> chineseCharacters = new HashSet<char>(additionalCharacters);
        HashSet<char> japaneseCharacters = new HashSet<char>(additionalCharacters);
        HashSet<char> englishCharacters = new HashSet<char>(additionalCharacters);

        for (char c = 'a'; c <= 'z'; c++)
        {
            chineseCharacters.Add(c);
            japaneseCharacters.Add(c);
            englishCharacters.Add(c);
        }

        for (char c = 'A'; c <= 'Z'; c++)
        {
            chineseCharacters.Add(c);
            japaneseCharacters.Add(c);
            englishCharacters.Add(c);
        }

        string[] lines = File.ReadAllLines(csvPath);
        foreach (string line in lines)
        {
            foreach (char character in line)
            {
                if (IsChinese(character))
                {
                    chineseCharacters.Add(character);
                }
                else if (IsJapanese(character))
                {
                    japaneseCharacters.Add(character);
                }
                else if (IsEnglish(character))
                {
                    englishCharacters.Add(character);
                }
            }
        }

        WriteCharactersToFile(chineseOutputPath, chineseCharacters);
        WriteCharactersToFile(japaneseOutputPath, japaneseCharacters);
        WriteCharactersToFile(englishOutputPath, englishCharacters);

        AssetDatabase.Refresh();
        Debug.Log("Characters extracted and saved to respective files.");
    }

    private static bool IsChinese(char c)
    {
        return c >= 0x4E00 && c <= 0x9FFF;
    }

    private static bool IsJapanese(char c)
    {
        return (c >= 0x3040 && c <= 0x30FF) || (c >= 0x31F0 && c <= 0x31FF) || (c >= 0xFF00 && c <= 0xFFEF);
    }

    private static bool IsEnglish(char c)
    {
        return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
    }

    private static void WriteCharactersToFile(string path, HashSet<char> characters)
    {
        using (StreamWriter writer = new StreamWriter(path, false))
        {
            foreach (char character in characters)
            {
                writer.Write(character);
            }
        }
    }
}
