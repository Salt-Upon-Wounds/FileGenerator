internal partial class Program
{
    /// <summary>
    /// Класс предоставляет статические методы для генерации псевдослучайных данных с возможностью генерировать 
    /// во множестве потоков
    /// </summary>
    public static class Generators
    {
        static int seed = Environment.TickCount;

        static readonly ThreadLocal<Random> random =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        public static int Rand()
        {
            return random.Value.Next();
        }

        public static DateTime GenDate()
        {
            var now = DateTime.Today;
            var past = DateTime.Today.AddYears(-5);
            int range = (now - past).Days;
            return past.AddDays(random.Value.Next(range));
        }

        public static string GenSrtingFromSourse(string chars)
        {
            var stringChars = new char[10];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Value.Next(chars.Length)];
            }

            return new String(stringChars);
        }
        public static string GenEngStr()
        {
            return GenSrtingFromSourse("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
        }

        public static string GenRuStr()
        {
            return GenSrtingFromSourse("йцукенгшщзхъфывапролджэячсмитьбюёЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮЁ");
        }

        public static int GenInteger()
        {
            return random.Value.Next(1, 10000000);
        }

        public static double GenFloating()
        {
            return random.Value.NextDouble() * 19 + 1;
        }

        public static string GenFileString()
        {
            return GenDate().ToShortDateString() + "||" +
                   GenEngStr() + "||" +
                   GenRuStr() + "||" +
                   GenInteger() + "||" +
                   GenFloating().ToString("0.00000000") + "||";

        }
    }
}