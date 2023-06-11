using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bogdan_Lukic_resenje_zadatka_za_praksu_2023.Models
{
    public class Kupci_Vozila
    {
        public Kupci kupac;
        public Vozila? vozila;

        public Kupci_Vozila(Kupci kupci, Vozila vozila) {
            this.kupac = kupci;
            this.vozila = vozila;
        }
    }
}
