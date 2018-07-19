using System;
using System.IO;
using System.Linq;

namespace ImageToBase64
{
    class Program
    {
        static void Main(string[] args)
        {
            // Hardcoded array of available extensions
            // Can be redefined if needed
            var desiredFileExtensions = new string[]
            {
                ".apng",
                ".bmp",
                ".gif",
                ".ico",
                ".jpeg",
                ".jpg",
                ".png",
                ".svg"
            };

            // Get current directory
            var currentDirectory = Environment.CurrentDirectory;

            // Get information about current directory
            var directoryInfo = new DirectoryInfo(currentDirectory);

            // Get all files with extensions
            var enumerateFiles = directoryInfo
                .EnumerateFiles("*.*");

            // Get all files with desired extensions in directory
            var fileList = from file in enumerateFiles
                           where desiredFileExtensions.Contains(file.Extension)
                           select file;

            // Convert files to base64 representation
            foreach (var file in fileList)
            {
                var fullFileName = file
                    .FullName;

                var fileDirectory = file
                    .DirectoryName;

                var fileName = Path
                    .GetFileNameWithoutExtension(fullFileName);

                // Read all bytes from file and close it
                var fileBytes = File
                    .ReadAllBytes(fullFileName);

                // Build path to new file
                var newFilePath = Path
                    .Combine(fileDirectory, $"{fileName}.base64.txt");

                // Convert all bytes of file to
                // base64 string representation
                var fileStringBase64 = Convert
                    .ToBase64String(fileBytes);

                // Write data to a file
                File
                    .WriteAllText(newFilePath, fileStringBase64);
            }
        }
    }
}
