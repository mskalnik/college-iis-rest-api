using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Klijent
{
    class Program
    {
        static void Main(string[] args)
        {
            int nastavi = 0;
            string poruka = "Odaberite opciju\n1) Ispis svih vozaca\n2) Ispis odredenog vozaca\n3) Unos novog vozaca\n4) Prekid rada programa\nOdabir: ";

            do
            {
                Console.Clear();
                Console.Write(poruka);
                string temp = Console.ReadLine();

                while (!int.TryParse(temp, out nastavi))
                {
                    Console.Clear();
                    Console.Write(poruka);
                    temp = Console.ReadLine();
                }

                switch (nastavi)
                {
                    case 1: IspisSve();         break;
                    case 2: IspisOdredenog();   break;
                    case 3: OnosNovog();        break;
                }
            } while (nastavi != 4);
        }

        private static void OnosNovog()
        {
            Console.Clear();

            Console.Write("Upisite ime: ");
            string ime = Console.ReadLine();

            Console.Write("Upisite prezime: ");
            string prezime = Console.ReadLine();

            Console.Write("Upisite OIB: ");
            string oib = Console.ReadLine();

            Console.Write("Upisite datum izdavanja vozacke(dd.MM.yyyy): ");
            string izdavanjeVozacke = Console.ReadLine();
            string[] izdavanjeVozackeLista  = izdavanjeVozacke.Split('.');
            int izdavanjeVozackeDan         = int.Parse(izdavanjeVozackeLista[0]);
            int izdavanjeVozackeMjesec      = int.Parse(izdavanjeVozackeLista[1]);
            int izdavanjeVozackeGodina      = int.Parse(izdavanjeVozackeLista[2]);

            Console.Write("Upisite datum isteka vozacke(dd.MM.yyyy): ");
            string istekVozacke = Console.ReadLine();
            string[] istekVozackeLista  = istekVozacke.Split('.');
            int istekVozackeDan         = int.Parse(istekVozackeLista[0]);
            int istekVozackeMjesec      = int.Parse(istekVozackeLista[1]);
            int istekVozackeGodina      = int.Parse(istekVozackeLista[2]);

            Console.Write("Upisite kategoriju vozacke: ");
            string kategorija = Console.ReadLine();
            Console.Clear();

            Vozac vozac = new Vozac
            {
                Ime                 = ime,
                Prezime             = prezime,
                OIB                 = oib,
                IzdavanjeVozacke    = new DateTime(izdavanjeVozackeGodina, izdavanjeVozackeMjesec, izdavanjeVozackeDan),
                IstekVozacke        = new DateTime(istekVozackeGodina, istekVozackeMjesec, istekVozackeDan),
                Kategorija          = kategorija
            };

            XmlSerializer pretvoriUXml  = new XmlSerializer(typeof(Vozac));
            StringWriter pisac          = new StringWriter();
            pretvoriUXml.Serialize(pisac, vozac);

            string xml = pisac.ToString();
            xml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            xml = xml.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"",
                              "xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.datacontract.org/2004/07/Server.Models\"");

            byte[] podaci           = Encoding.UTF8.GetBytes(xml);
            HttpWebRequest zahtjev  = (HttpWebRequest)WebRequest.Create("http://localhost:50000/api/vozaci/novi");
            zahtjev.Method          = "POST";
            zahtjev.Accept          = "application/xml";
            zahtjev.ContentType     = "application/xml";

            Stream podaciZahtjev = zahtjev.GetRequestStream();
            podaciZahtjev.Write(podaci, 0, podaci.Length);
            podaciZahtjev.Close();

            HttpWebResponse odgovor = (HttpWebResponse)zahtjev.GetResponse();
            Stream podaciOdgovor    = odgovor.GetResponseStream();
            XmlDocument doc         = new XmlDocument();
            doc.Load(podaciOdgovor);
            doc.Save(Console.Out);

            Console.Write("\nPress any key to go to the main menu...");
            Console.Read();
        }

        private static void IspisOdredenog()
        {
            Console.Clear();
            Console.Write("Upisite OIB: ");
            string oib = Console.ReadLine();

            string path = $"http://localhost:50000/api/vozaci/{oib}";
            HttpWebRequest zahtjev = (HttpWebRequest)WebRequest.Create(path);
            zahtjev.Accept = "application/xml";

            HttpWebResponse odgovor = (HttpWebResponse)zahtjev.GetResponse();
            Stream podaci = odgovor.GetResponseStream();
            XmlDocument doc = new XmlDocument();

            doc.Load(podaci);
            doc.Save(Console.Out);

            Console.Write("\nPress any key to go to the main menu...");
            Console.Read();
        }

        private static void IspisSve()
        {
            Console.Clear();

            string path = $"http://localhost:50000/api/vozaci";
            HttpWebRequest zahtjev = (HttpWebRequest)WebRequest.Create(path);
            zahtjev.Accept = "application/xml";

            HttpWebResponse odgovor = (HttpWebResponse)zahtjev.GetResponse();
            Stream podaci = odgovor.GetResponseStream();
            XmlDocument doc = new XmlDocument();

            doc.Load(podaci);
            doc.Save(Console.Out);

            Console.Write("\nPress any key to go to the main menu...");
            Console.Read();
        }
    }
}
