using System.Text;
using DataManipulatorModels;

namespace DataManipulatorGenerator
{
    public static class BaseDataGenerator
    {
        private static readonly DateTime MinDate = DateTime.Today.AddYears(-5);

        private const uint MaxLatinStringLength = 10;
        private const uint MaxCyrillicStringLength = 10;

        private const int MinInteger = 1;
        private const int MaxInteger = 100_000_000;

        private const int MinDoubleValue = 1;
        private const int MaxDoubleValue = 20;

        public static Data GenerateData()
        {
            Random random = new Random();
            return new Data()
            {
                Date = GenerateDate(),
                LatinString = GenerateString(MaxLatinStringLength, 'A', 'z'),
                CyrillicString = GenerateString(MaxCyrillicStringLength, 'А', 'я'),
                PositiveEvenInteger = 2 * (uint)random.Next(MinInteger, MaxInteger / 2),
                PositiveDouble = Math.Round(random.NextDouble() * (MaxDoubleValue - MinDoubleValue) + MinDoubleValue, 8),
            };
        }

        private static DateTime GenerateDate()
        {
            Random gen = new Random();
            int range = (DateTime.Today - MinDate).Days;
            return MinDate.AddDays(gen.Next(range));
        }

        private static string GenerateString(uint length, char minChar, char maxChar)
        {
            StringBuilder s = new();
            Random random = new Random();
            
            for (int i = 0; i < length; i++)
            {
                char next;
                do
                {
                    next = (char)random.Next(minChar, maxChar);
                }
                while (!Char.IsLetter(next));
                s.Append(next);
            }

            return s.ToString();
        }
    }
}