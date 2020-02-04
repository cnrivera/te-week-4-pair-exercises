using System;
using System.IO;
using System.Collections.Generic;

namespace FindAndReplace
{
    class Program
    {
        static void Main(string[] args)
        {
            string searchPhrase;
            string replacePhrase;
            string sourceFilePath;
            string destinationFilePath;
            string originalText;
            string newText;

            searchPhrase = PromptUser("Please enter the phrase to replace");
            replacePhrase = PromptUser("Please enter the new phrase");
            sourceFilePath = GetFilePath();
            destinationFilePath = GetDestinationFilePath();
            if(destinationFilePath.Length > 0)
            {
                originalText = ReadFile(sourceFilePath);
                newText = originalText.Replace(searchPhrase, replacePhrase);
                WriteToNewFile(destinationFilePath, newText);
            }
            
        }
        public static string PromptUser(string message)
        {
            string response = "";

            Console.WriteLine(message);

            response = Console.ReadLine();

            return response;
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
        public static string GetDestinationFilePath()
        {
            string filePath = "";
            bool isValidFilePath;

            Console.WriteLine("Please enter the filepath");
            filePath = Console.ReadLine();

            isValidFilePath = ValidateFilePath(filePath);

            if (isValidFilePath)
            {
                Console.WriteLine("The specified file path is already in use, would you like to try a different path? Y/N");
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
        public static void WriteToNewFile(string fileName, string contents)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.Write(contents);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
