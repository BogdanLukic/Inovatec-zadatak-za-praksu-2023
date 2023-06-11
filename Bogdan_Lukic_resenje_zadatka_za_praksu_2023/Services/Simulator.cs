using Bogdan_Lukic_resenje_zadatka_za_praksu_2023.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogdan_Lukic_resenje_zadatka_za_praksu_2023.Services
{
    public class Simulator
    {
        private static IData data = null;
        public static void startSimulation()
        {
            data = DataCsv.Data;
            
            // Calculating FIRST discount
            data.calculateCenaByMarka();

            // Calculating SECOUND discount
            data.calculateCenaByOprema();

            // Calculating discount for person
            data.calculateDiscountForPerson();

            // Simulate process of buying
            data.buying();

            //  Printing answer in console
            data.printInConsole();

            //  Printing answer in csv-file in "\Bogdan_Lukic_resenje_zadatka_za_praksu_2023\Csv-s\Podaci_o_kupovini.csv"
            data.writeInCsv();
        }

    }
}
