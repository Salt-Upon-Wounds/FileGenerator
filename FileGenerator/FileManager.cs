using System.Text.RegularExpressions;

internal partial class Program
{
    public static class FileManager
    {
        public static void GenFiles(int strings, int files)
        {
            Console.WriteLine("Creating files");
            Console.Write(new string('.', files) + "\r");
            if (!Directory.Exists("Files"))
                Directory.CreateDirectory("Files");

            Parallel.For(0, files, i =>
            {
                using (StreamWriter outputFile = new StreamWriter("Files" + Path.DirectorySeparatorChar + i.ToString() + ".txt", false))
                {
                    for (int j = 0; j < strings; j++)
                    {
                        outputFile.WriteLine(Generators.GenFileString());
                    }
                    Console.Write('|');
                }
            });
            
            Console.WriteLine();
        }
        
        public static void FIlesUnion()
        {
            var files = Directory.GetFiles("Files");
            var resultfile = "Files" + Path.DirectorySeparatorChar + "result.txt";
            using (Stream outputStream = File.OpenWrite(resultfile))
            {
                foreach (string inputFile in files)
                {
                    using (Stream inputStream = File.OpenRead(inputFile))
                    {
                        inputStream.CopyTo(outputStream);
                    }
                }
            }
        }

        public static void RemoveStringsFromFile(string fileName, params string[] strings)
        {
            bool Match(string str)
            {
                bool result = true;
                Parallel.For(0, strings.Length, i =>
                {
                    if (Regex.IsMatch(str, Regex.Escape(strings[i]))) result = false;
                });
                return result;
            }
            File.WriteAllLines(fileName, File.ReadLines(fileName).Where(l => Match(l)).ToList());
        }
    }
}