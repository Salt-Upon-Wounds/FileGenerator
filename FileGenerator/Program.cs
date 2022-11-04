internal partial class Program
{
    private static void Main(string[] args)
    {
        //FileManager.GenFiles(100000, 100);
        //FileManager.GenFiles(10, 3);
        //FileManager.FIlesUnion();
        FileManager.RemoveStringsFromFile(@"FIles\result.txt", "31.");
        Console.WriteLine("Нажмите любую кнопку, чтобы выйти");
        Console.Read();
    }
}