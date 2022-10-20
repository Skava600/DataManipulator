namespace DataManipulatorModels
{
    public class Data
    {
        public DateTime Date { get; set; }

        public string LatinString { get; set; }

        public string CyrillicString { get; set; }

        public uint PositiveEvenInteger { get; set; }

        public double PositiveDouble { get; set; }

        public override string ToString()
        {
            return $"{this.Date.ToString("MM/dd/yyyy")}||" +
                               $"{this.LatinString}||" +
                               $"{this.CyrillicString}||" +
                               $"{this.PositiveEvenInteger}||" +
                               $"{this.PositiveDouble}";
        }
    }
}