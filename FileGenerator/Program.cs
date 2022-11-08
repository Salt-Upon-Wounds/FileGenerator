using MySqlConnector;

internal partial class Program
{
    private static void Main(string[] args)
    {
        Console.Write("Enter server: ");
        var datasource = Console.ReadLine(); //your server
        Console.Write("Enter db name: ");
        var database = Console.ReadLine(); //your database name
        Console.Write("Enter username: ");
        var username = Console.ReadLine(); //username of server to connect
        Console.Write("Enter password: ");
        var password = Console.ReadLine(); //password
        Console.Write("Enter port: ");
        var port = Console.ReadLine();//port
        Console.Clear();
        //your connection string 
        string connString = @"Data Source=" + datasource + ";Initial Catalog="
                    + database + ";Port=" + port + ";User ID=" + username + ";Password=" + password + ";AllowLoadLocalInfile=true;Allow User Variables=true";
        
        MySqlConnection conn = new MySqlConnection(connString);
        int i = 0;
        while (true)
        {
            Console.WriteLine("1. сгенерировать файлы в отдельной папке");
            Console.WriteLine("2. объединить файлы в один result.txt рядом с экзешником");
            Console.WriteLine("3. занести файлы из папки в бд");
            Console.WriteLine("4. получить сумму целых и медиану дробных");
            Console.WriteLine("5. Выйти");
            Console.Write("Введите номер пункта: ");
            int.TryParse(Console.ReadLine(), out i);
            switch (i)
            {
                case 1: FileManager.GenFiles(100000, 100); break;
                case 2:
                    {
                        Console.WriteLine("Введите через пробел сочетания символов, строки с которыми хотите удалить:");
                        string s = Console.ReadLine();
                        FileManager.FilesUnion();
                        FileManager.RemoveStringsFromFile(@"result.txt", s.Split(' '));
                        break;
                    }
                case 3: FileManager.InsertAll(conn); break;
                case 4: FileManager.IntSumAndFloatMedian(conn); break;
                case 5: return;
                default: break;
            }
            Console.Clear();
        }
    }
}