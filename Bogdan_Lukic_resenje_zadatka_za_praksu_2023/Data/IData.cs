using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogdan_Lukic_resenje_zadatka_za_praksu_2023.Data
{
    public interface IData
    {
        public void calculateCenaByMarka();
        public void calculateCenaByOprema();
        public void calculateDiscountForPerson();
        public void buying();
        public void printInConsole();
        public void writeInCsv();
    }
}
