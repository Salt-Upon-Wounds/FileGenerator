using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using MySqlConnector;

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
        
        public static void FilesUnion()
        {
            if (File.Exists("result.txt"))
            {
                File.Delete("result.txt");
            }
            var files = Directory.GetFiles("Files");
            var resultfile = "result.txt";
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

        public static void InsertFromFileToDB(string fileName, MySqlConnection conn)
        {

            MySqlBulkLoader bl = new MySqlBulkLoader(conn)
                {
                    Columns = { "@a, estring, rstring, int_num, @d" },
                    Expressions = { "float_num=replace(@d,',','.')", "date=STR_TO_DATE(@a, '%d.%m.%Y')" }
                };
            bl.Local = true;
            bl.TableName = "tbl";
            bl.FieldTerminator = "||";
            bl.LineTerminator = "\n";
            bl.FileName = "fileName";//fileName
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                // Upload data from file
                int count = bl.Load();
                Console.WriteLine(count + " lines uploaded.");

                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public static void InsertAll(MySqlConnection conn)
        {
            var files = Directory.GetFiles("Files");
            int f = 0;
            foreach (string inputFile in files)
            {
                Console.WriteLine("File: " + ++f);
                InsertFromFileToDB(inputFile, conn);
            }
        }

        public static string IntSumAndFloatMedian(MySqlConnection conn)
        {
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string res = "";
                string sql = "SELECT SUM(int_num)  FROM tbl";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();
                res += rdr[0];
                rdr.Close();

                sql =
                    "SET @row_index := -1;" +
                    "SELECT AVG(subq.float_num) as median_value" +
                    " FROM ( SELECT @row_index:=@row_index + 1 AS row_index, float_num" +
                    " FROM tbl ORDER BY float_num )" +
                    " AS subq WHERE subq.row_index  IN (FLOOR(@row_index / 2) , CEIL(@row_index / 2));";

                cmd = new MySqlCommand(sql, conn);
                rdr = cmd.ExecuteReader();

                rdr.Read();
                res += " "+ rdr[0];
                rdr.Close();

                conn.Close();

                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "";
            }
        }
    }
}