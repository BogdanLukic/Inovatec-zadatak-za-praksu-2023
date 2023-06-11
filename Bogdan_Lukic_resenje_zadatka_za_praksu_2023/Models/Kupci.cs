using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogdan_Lukic_resenje_zadatka_za_praksu_2023.Models
{
    public class Kupci
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public double Budzet { get; set; }
        public bool Premium { get; set; } 

        private int popust = 0;
        public int Popust { get { return popust; } private set { } }

        public void calculatePopust()
        {
            if(Premium == true && popust == 0)
            {
                Random random = new Random();
                popust = (int)random.NextInt64(5, 15);
            }
        }

        public override string ToString()
        {
            return "Id korisnika:" + Id + " Ime:" + Ime + " Prezime:" + Prezime + " Budzet:" + Budzet + " Premium:" + Premium + " Ostvareni popust:" + popust;
        }

    }
}
