internal partial class Program
{
    private static void Main(string[] args)
    {
        FileManager.GenFiles(100000, 100);
        Console.WriteLine("Нажмите любую кнопку, чтобы выйти");
        Console.Read();
    }
}