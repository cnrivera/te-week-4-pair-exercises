using System;
using System.IO;
using System.Collections.Generic;

namespace file_io_part1_exercises_pair
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath;
            string fileContents;
            int wordCount;
            int sentenceCount;

            filePath = GetFilePath();
            if (filePath.Length > 0)
            {
                fileContents = ReadFile(filePath);
                fileContents = RemoveDoubleSpaces(fileContents);
                wordCount = CountWords(fileContents);
                sentenceCount = CountSentences(fileContents);
                DisplayCounts(wordCount, sentenceCount);
            }

        }

        public static string GetFilePath()
        {
            string filePath = "";
            bool isValidFilePath;

            Console.WriteLine("Please enter the filepath");
            filePath = Console.ReadLine();

            isValidFilePath = ValidateFilePath(filePath);

            if (!isValidFilePath)
            {
                // to do
                Console.WriteLine("The specified file path was invalid, would you re-enter it? Y/N");
                string response = Console.ReadLine();
                if (response.ToUpper() == "Y")
                {
                    filePath = GetFilePath();
                }
                else
                {
                    filePath = "";
                }
            }

            return filePath;
        }

        public static bool ValidateFilePath(string filePath)
        {
            bool isValid = File.Exists(filePath);             
            return isValid;
        }

        public static string ReadFile(string filePath)
        {
            string fileContents = "";

            try
            {
                //Open a StreamReader with the using statement
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Read the file until the end of the stream is reached
                    // EndOfStream is a "marker" that the stream uses to determine 
                    // if it has reached the end
                    // As we read forward the marker moves forward like a typewriter.

                    while (!sr.EndOfStream)
                    {
                        fileContents += " " + sr.ReadLine();
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error reading the file");
                Console.WriteLine(ex.Message);
            }

            return fileContents;
        }

        public static string RemoveDoubleSpaces(string text)
        {
            const string doubleSpace = "  ";
            const string singleSpace = " ";

            while(text.Contains(doubleSpace))
            {
                text = text.Replace(doubleSpace, singleSpace);
            }

            return text;
        }

        public static int CountWords(string fileContents)
        {            
            string[] words = fileContents.Split(" ");

            return words.Length;
        }

        public static int CountSentences(string fileContents)
        {
            char[] punctuation = { '!', '.', '?' };
            string[] sentences = fileContents.Split(punctuation);
            return sentences.Length;
        }

        private static void DisplayCounts(int wordCount, int sentenceCount)
        {
            Console.Clear();
            Console.WriteLine($"There are {wordCount} words in the file");
            Console.WriteLine($"There are {sentenceCount} sentences in the file");
        }
    }
}
