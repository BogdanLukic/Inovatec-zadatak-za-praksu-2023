using Bogdan_Lukic_resenje_zadatka_za_praksu_2023.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogdan_Lukic_resenje_zadatka_za_praksu_2023.Models
{
    public class Automobil: Vozila 
    {
        public int GodinaProizvodnje { get; set; }
        public double Kilometraza { get; set; }

        public Automobil(int id, string tip, string marka, string model, double potrosnja, int godinaProizvodnje, double kilometraza,double cena) :base(id, tip, marka, model, potrosnja,cena)
        {
            GodinaProizvodnje = godinaProizvodnje;
            Kilometraza = kilometraza;
            Nova_cena = cena;
        }

        public override void calculateCena()
        {
            switch (Marka)
            {
                case "Mercedes":
                    if (GodinaProizvodnje > 2016)
                        Nova_cena = Nova_cena + Calculation.percent(5, Nova_cena);
                    else
                        Nova_cena = Nova_cena - Calculation.percent(10, Nova_cena);
                    if (Kilometraza < 50000)
                        Nova_cena = Nova_cena + Calculation.percent(6, Nova_cena);
                    break;
                case "BMW":
                    if (GodinaProizvodnje > 2018 && Potrosnja < 7)
                        Nova_cena = Nova_cena + Calculation.percent(15,Nova_cena);
                    else
                        Nova_cena = Nova_cena - Calculation.percent(10, Nova_cena);
                    break;
                case "Peugeot":
                    if (GodinaProizvodnje < 2012)
                        Nova_cena = Nova_cena - Calculation.percent(15, Nova_cena);
                    else
                        Nova_cena = Nova_cena + Calculation.percent(5, Nova_cena);

                    if (Kilometraza < 30000 && Potrosnja < 6)
                        Nova_cena = Nova_cena + Calculation.percent(10, Nova_cena);
                    break;
            }
        }
    }
}
