using Bogdan_Lukic_resenje_zadatka_za_praksu_2023.Models;
using CsvHelper;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper.Configuration;
using Bogdan_Lukic_resenje_zadatka_za_praksu_2023.Services;

namespace Bogdan_Lukic_resenje_zadatka_za_praksu_2023.Data
{
    public class DataCsv : IData
    {

        private List<Kupci> kupci_list;
        private List<Kupovina> kupovina_list;
        private List<Oprema> oprema_list;
        private List<Vozila> vozila_list;
        private List<VoziloOprema> vozilo_oprema_list;

        private readonly static Object key = new Object();
        private static IData data;
        public static IData Data { 
            get {
                if(data == null) {
                    lock (key)
                    {
                        if (data == null) data = new DataCsv();
                    }
                }
                return data; 
                } 
            private set { } 
        }

        private DataCsv() 
        {
            kupci_list = readCsv<Kupci>("kupci.csv");
            kupovina_list = readCsv<Kupovina>("kupovina.csv");
            oprema_list = readCsv<Oprema>("oprema.csv");
            vozila_list = readVozilaCsv("vozila.csv");
            vozilo_oprema_list = readCsv<VoziloOprema>("vozilo_oprema.csv");
        }

        private List<T> readCsv<T>(string file)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            using (var reader = new StreamReader(CSVPath.Default_Path + file))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<T>();
                var list = new List<T>();
                foreach(var record in records)
                {
                    list.Add(record);
                }
                return list;
            }
        }

        private List<Vozila> readVozilaCsv(string file)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
            };
            using (var reader = new StreamReader(CSVPath.Default_Path + file))
            using (var csv = new CsvReader(reader, config))
            {
                List<Vozila> vozila_list = new List<Vozila>();
                while (csv.Read())
                {
                    var tip = csv.GetField(1);
                    switch (tip)
                    {
                        case "Automobil":
                            var id =  int.Parse(csv[0]);
                            var marka = csv[2];
                            var model = csv[3];
                            var potrosnja = double.Parse(csv[4]);
                            var godina_proizvodnje = int.Parse(csv[6]);
                            var kilometraza = double.Parse(csv[7]);
                            Automobil automobil = new Automobil(id,tip,marka,model,potrosnja, godina_proizvodnje, kilometraza,4000);
                            vozila_list.Add(automobil);
                            break;
                        case "Motor":
                            var idMotor = int.Parse(csv[0]);
                            var markaMotor = csv[2];
                            var modelMotor = csv[3];
                            var potrosnjaMotor = double.Parse(csv[4]);
                            var kubikaza = int.Parse(csv[5]);
                            var snaga = double.Parse(csv[8]);
                            Motor motor = new Motor(idMotor, tip, markaMotor, modelMotor, potrosnjaMotor, kubikaza, snaga,2500);
                            vozila_list.Add(motor);
                            break;
                    }
                }
                return vozila_list;
            }
        }

        public void calculateCenaByMarka()
        {
            foreach (Vozila vozila in vozila_list)
            {
                vozila.calculateCena();
            }
        }

        public void calculateCenaByOprema()
        {
            var info = from vo in vozilo_oprema_list
                       join o in oprema_list on vo.OpremaId equals o.Id
                       select new { vo.VoziloId, vo.OpremaId, o.Cena, o.PovecavaCenu };

            foreach(var i in info)
            {
                var vozilo = vozila_list.Where(x => x.Id == i.VoziloId).FirstOrDefault();
                if (vozilo != null && i.PovecavaCenu == true)
                    vozilo.Nova_cena += i.Cena;
                else if(vozilo != null)
                    vozilo.Nova_cena -= i.Cena;
            }
        }

        public void calculateDiscountForPerson()
        {
            foreach (Kupci k in kupci_list)
            {
                k.calculatePopust();
            }
        }

        private List<Kupci_Vozila> kupci_vozila_list;

        public void buying()
        {
            kupci_vozila_list = new List<Kupci_Vozila>();
            var kupci_kupovina_vozila = from kupovina in kupovina_list
                                        join kupci in kupci_list on kupovina.KupacId equals kupci.Id
                                        join vozilo in vozila_list on kupovina.VoziloId equals vozilo.Id
                                        select new { kupci.Id, kupci.Budzet, kupci.Premium, kupci.Popust, kupovina.VoziloId, kupovina.Datum, vozilo.Nova_cena };

            var premium_kupci = kupci_kupovina_vozila.OrderByDescending(x => x.Premium);

            foreach (var pk in premium_kupci)
            {
                var potraznja = premium_kupci.Where(x => x.VoziloId == pk.VoziloId).OrderBy(x=>x.Datum).ToArray();
                var vozilo = vozila_list.FirstOrDefault(x => x.Id == pk.VoziloId);

                var cena_vozila = pk.Nova_cena;
                var kupac = kupci_list.FirstOrDefault(x => x.Id == pk.Id);

                if (pk.Premium == true)
                    cena_vozila = cena_vozila - Calculation.percent(pk.Popust, cena_vozila);

                if(vozilo != null && vozilo.Prodat == false && pk.Budzet >= cena_vozila)
                {
                    vozilo.Nova_cena = cena_vozila;
                    vozilo.Prodat = true;
                    
                    if(kupac != null)
                    {
                        kupac.Budzet = kupac.Budzet - pk.Nova_cena;
                        Kupci_Vozila kp = new Kupci_Vozila(kupac, vozilo);
                        kupci_vozila_list.Add(kp);
                    }
                }
                else
                {
                    if(kupac != null)
                    {
                        Kupci_Vozila kp = new Kupci_Vozila(kupac, null);
                        kupci_vozila_list.Add(kp);
                    }
                }
            }
        }

        public void printInConsole()
        {
            Console.WriteLine("======== Podaci o vozilima ========");
            foreach(Vozila v in vozila_list)
            {
                Console.WriteLine(v.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("======== Podaci o korisnicima ========");
            foreach(Kupci k in kupci_list)
            {
                Console.WriteLine(k.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("======== Podaci o kupovini ========");
            foreach (var k in kupci_vozila_list)
            {
                Console.WriteLine(k.kupac + " " + k.vozila);
            }
        }

        public void writeInCsv()
        {
            using (var writer = new StreamWriter(CSVPath.Default_Path + "Podaci_o_kupovini.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteField("Id_kupca");
                csv.WriteField("Id_kupljenog_vozila");
                csv.NextRecord();
                foreach (var k in kupci_vozila_list)
                {
                    csv.WriteRecord(k.kupac.Id);
                    if(k.vozila != null)
                        csv.WriteRecord(k.vozila.Id);
                    csv.NextRecord();
                }
            }
        }
    }
}
