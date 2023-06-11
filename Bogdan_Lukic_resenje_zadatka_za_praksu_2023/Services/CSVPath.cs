using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogdan_Lukic_resenje_zadatka_za_praksu_2023.Services
{
    public class CSVPath
    {
        private static string default_path = "..\\..\\..\\..\\Csv-s\\";  // + fajl.csv
        public static string Default_Path { get { return default_path; } }
    }
}
