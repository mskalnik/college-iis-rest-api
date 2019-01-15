using System;
using System.Runtime.Serialization;

namespace Server.Models
{
    [DataContract]
    public class Vozac
    {
        [DataMember(Order = 0)]
        public string Ime { get; set; }
        [DataMember(Order = 1)]
        public string Prezime { get; set; }
        [DataMember(Order = 2)]
        public string OIB { get; set; }
        [DataMember(Order = 3)]
        public DateTime IzdavanjeVozacke { get; set; }
        [DataMember(Order = 4)]
        public DateTime IstekVozacke { get; set; }
        [DataMember(Order = 5)]
        public string Kategorija { get; set; }
    }
}