using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogdan_Lukic_resenje_zadatka_za_praksu_2023.Models
{
    public abstract class Vozila
    {
        public int Id { get; set; }
        public string Tip { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public double Potrosnja { get; set; }
        public double Cena { get; set; }
        public double Nova_cena { get; set; }

        public bool Prodat = false;

        public Vozila(int id, string tip, string marka, string model, double potrosnja, double cena)
        {
            Id = id;
            Tip = tip;
            Marka = marka;
            Model = model;
            Potrosnja = potrosnja;
            Cena = cena;
        }

        public abstract void calculateCena();

        public override string ToString()
        {
            return "Id vozila:" + Id + " Tip:" + Tip + " Marka:" + Marka + " Model:" + Model + " Potrosnja: " + Potrosnja + " Cena:" + Nova_cena;
        }
    }
}
