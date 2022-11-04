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

        public static void FilesUnionWithoutStrings(params string[] strings)
        {

        }
    }
}