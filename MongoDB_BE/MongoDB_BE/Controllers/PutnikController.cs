using AppLibrary;
using AppLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PutnikController : Controller
    {
        [HttpGet]
        [Route("VratiSvePutnike")]
        public ActionResult VratiSvePutnike()
        {
            try
            {
                return new JsonResult(DataProvider.VratiSvePutnike());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiPutnika/{id}")]
        public ActionResult VratiPutnika([FromRoute(Name = "id")] string id)
        {
            try
            {
                return new JsonResult(DataProvider.VratiPutnika(id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }
        [HttpGet]
        [Route("VratiPutnikaJmbg/{jmbg}")]
        public ActionResult VratiPutnikaJmbg([FromRoute(Name = "jmbg")] string jmbg)
        {
            try
            {
                Putnik p = DataProvider.VratiPutnikaJmbg(jmbg);
                if (p != null)
                    return Ok(p);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiPutnikeZaVoznju/{sifra}")]
        public ActionResult VratiPutnikeZaVoznju([FromRoute(Name = "sifra")] String sifra)
        {
            try
            {
                return new JsonResult(DataProvider.VratiPutnikeZaVoznju(sifra));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }

        }

        [HttpPost]
        [Route("KreirajKolekcijuPutnika")]
        public ActionResult KreirajKolekcijuPutnika()
        {
            try
            {
                DataProvider.KreirajKolekcijuPutnika();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPost]
        [Route("KreirajPutnika")]
        public ActionResult KreirajPutnika([FromBody] Putnik putnik)
        {
            try
            {
                ObjectId retVal = DataProvider.KreirajPutnika(putnik);
                return new JsonResult(retVal.ToString());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiPutnika/{jmbg}")]
        public ActionResult ObrisiPutnika([FromRoute(Name = "jmbg")] String jmbg)
        {
            try
            {
                DataProvider.ObrisiPutnika(jmbg);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("DodajRezervacijuPutniku/{sifra}/{jmbg}")]
        public ActionResult DodajRezervacijuPutniku([FromRoute(Name = "sifra")] String sifra,
                                                          [FromRoute(Name = "jmbg")] String jmbg)
        {
            try
            {
                DataProvider.DodajRezervacijuPutniku(sifra, jmbg);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }



        [HttpPut]
        [Route("AzurirajPutnika/{id}")]
        public ActionResult AzurirajPutnika([FromRoute] string id, [FromBody] PutnikDTOUpdate putnikDTOUpdate)
        {
            try
            {
                DataProvider.AzurirajPutnika(id, putnikDTOUpdate);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }
    }
}
