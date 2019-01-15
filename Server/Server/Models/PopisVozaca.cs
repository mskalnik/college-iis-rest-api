using System;
using System.Collections.Generic;

namespace Server.Models
{
    public class PopisVozaca
    {
        public static List<Vozac> GetVozaci()
        {
            Vozac a = new Vozac
            {
                Ime = "Prvi",
                Prezime = "Lik",
                OIB = "1234567890",
                IzdavanjeVozacke = new DateTime(1991, 1, 1),
                IstekVozacke = new DateTime(2001, 1, 1),
                Kategorija = "B"
            };

            Vozac b = new Vozac
            {
                Ime = "Drugi",
                Prezime = "Ciko",
                OIB = "0987654321",
                IzdavanjeVozacke = new DateTime(1992, 2, 2),
                IstekVozacke = new DateTime(2002, 2, 2),
                Kategorija = "C"
            };

            List<Vozac> popis = new List<Vozac>
            {
                a,
                b
            };

            return popis;
        }
    }
}