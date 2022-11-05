using MySqlConnector;

internal partial class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Getting Connection ...");

        var datasource = @"";//your server
        var database = ""; //your database name
        var username = ""; //username of server to connect
        var password = ""; //password
        var port = "";//port

        //your connection string 
        string connString = @"Data Source=" + datasource + ";Initial Catalog="
                    + database + ";Port=" + port + ";User ID=" + username + ";Password=" + password + ";AllowLoadLocalInfile=true;Allow User Variables=true";
        
        MySqlConnection conn = new MySqlConnection(connString);
        //FileManager.GenFiles(100000, 100);
        //FileManager.GenFiles(4, 1);
        //FileManager.FilesUnion();
        //FileManager.RemoveStringsFromFile(@"FIles\result.txt",
        //    ".02.", ".03.", ".04.", ".05.", ".06.", ".07.", ".08.", ".09.", ".10.", ".11.", ".12.");
        //FileManager.InsertFromFileToDB("asd", conn);
        Console.WriteLine(FileManager.IntSumAndFloatMedian(conn));
        Console.WriteLine("Нажмите любую кнопку, чтобы выйти");
        Console.Read();
    }
}