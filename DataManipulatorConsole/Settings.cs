using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManipulatorConsole
{
    internal class Settings
    {
        public string DataPath { get; set; }
        public string DeletePattern { get; set; }
        public string MergedFilePath { get; set; }
        public int NumberOfFiles { get; set; }
        public int NumberOfLines { get; set; }
    }
}
