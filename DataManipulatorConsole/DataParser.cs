using System.Globalization;
using DataManipulatorModels;

namespace DataManipulatorConsole
{
    internal class DataParser
    {
        private readonly string separator;

        public DataParser(string separator)
        {
            this.separator = separator;
        }

        public Data ParseString(string line)
        {
            string[] fields = line.Split(separator);
            DateTime date = Convert.ToDateTime(fields[0], CultureInfo.InvariantCulture);
            string latinString = fields[1];
            string cyrillicString = fields[2];
            uint positiveInt = Convert.ToUInt32(fields[3]);
            double positiveDouble = Convert.ToDouble(fields[4], CultureInfo.InvariantCulture);

            return new Data()
            {
                Date = date,
                LatinString = latinString,
                CyrillicString = cyrillicString,
                PositiveEvenInteger = positiveInt,
                PositiveDouble = positiveDouble
            };
        }
    }
}
