using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogdan_Lukic_resenje_zadatka_za_praksu_2023.Models
{
    public class Oprema
    {
        public int Id { get; set; }
        public string Naziv { get; set;}
        public int Cena { get; set; }
        public bool PovecavaCenu { get; set; }
    }
}
