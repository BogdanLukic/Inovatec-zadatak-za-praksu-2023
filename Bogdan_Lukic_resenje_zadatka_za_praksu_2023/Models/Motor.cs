using Bogdan_Lukic_resenje_zadatka_za_praksu_2023.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogdan_Lukic_resenje_zadatka_za_praksu_2023.Models
{
    public class Motor: Vozila
    {
        public int Kubikaza { get; set; }
        public double Snaga { get; set; }

        public Motor(int id, string tip, string marka, string model, double potrosnja, int kubikaza, double snaga, double cena) : base(id, tip, marka, model, potrosnja, cena)
        {
            Kubikaza = kubikaza;
            Snaga = snaga;
            Nova_cena = cena;
        }

        public override void calculateCena()
        {
            switch (Marka)
            {
                case "Yamaha":
                    Nova_cena = Nova_cena + Calculation.percent(15, Nova_cena);
                    if (Kubikaza > 1200)
                        Nova_cena = Nova_cena + Calculation.percent(10, Nova_cena);
                    else
                        Nova_cena = Nova_cena - Calculation.percent(5, Nova_cena);
                    break;
                case "Harley":
                    Nova_cena = Nova_cena + Calculation.percent(10, Nova_cena);
                    if (Snaga > 180)
                        Nova_cena = Nova_cena + Calculation.percent(5, Nova_cena);
                    else
                        Nova_cena = Nova_cena - Calculation.percent(10, Nova_cena);
                    break;
            }
        }
    }
}
