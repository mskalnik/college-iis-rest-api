using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Server.Controllers
{
    public class ServerController : ApiController
    {
        #region GET metode

        [Route("api/vozaci")]
        [HttpGet]
        public List<Vozac> Get()
        {
            return PopisVozaca.GetVozaci();
        }

        [Route("api/vozaci/{oib}")]
        public Vozac Get(string oib)
        {
            foreach (var vozac in PopisVozaca.GetVozaci())
            {
                if (vozac.OIB == oib)
                    return vozac;
            }
            return null;
        }

        #endregion

        #region POST metode        

        [Route("api/vozaci/novi")]
        [HttpPost]
        public List<Vozac> PostNoveOsobeVratiPodatke(Vozac vozac)
        {
            List<Vozac> popisOsoba = PopisVozaca.GetVozaci();
            popisOsoba.Add(vozac);
            return popisOsoba;
        }

        #endregion
    }
}
