using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using AppLibrary;
using AppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RezervacijaController : Controller
    {
        [HttpPost]
        [Route("KreirajRezervacije")]
        public ActionResult KreirajRezervacije()
        {
            try
            {
                DataProvider.KreirajRezervacije();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPost]
        [Route("KreirajRezervaciju")]
        public ActionResult KreirajRezervaciju([FromBody] RezervacijaDTO rezervacija)
        {
            try
            {
                //IList<Prtljag> llist = DataProvider.VratiSavPrtljag();
                Prtljag prtljag = null;
               /* foreach (Prtljag p in llist)
                {
                    if (p.PostojiPrtljag.CompareTo(true) == 0)
                    {
                        prtljag = p;
                        break;
                    }
                }*/
                if (prtljag == null)
                {
                    prtljag = new Prtljag();
                    prtljag.Kolicina = rezervacija.KolicinaPrt;
                    prtljag.PostojiPrtljag = rezervacija.PostojiPrt;
                    DataProvider.KreirajPrtljag(prtljag);
                } //NAKON OVOGA JE KREIRAN PRTLJAG KOME JOS NIJE DODELJEN ID REZERVACIJE!!!


                
                Random rnd = new Random();
                string pom = rnd.Next(100000, 999999).ToString();
                Rezervacija r = new Rezervacija
                {
                    //Id = rezervacija.Id,
                    BrSedista = rezervacija.BrSedista,
                    Legitimacija = Convert.FromBase64String(rezervacija.Legitimacija),
                    Covid19Test = Convert.FromBase64String(rezervacija.Covid19Test),
                    Status = rezervacija.Status,
                    Sifra_Rezervacije = pom, 
                    Cena = rezervacija.Cena,
                    Niz_Usluga = rezervacija.Niz_Usluga,
                    Putnik = new ObjectId(rezervacija.Putnik),
                    Voznja = new ObjectId(rezervacija.Voznja),
                    Prtljag = prtljag.Id
                };              

                return new JsonResult(DataProvider.KreirajRezervaciju(r).ToString());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiRezervaciju/{Sifra_Rezervacije}")]
        public ActionResult VratiRezervaciju([FromRoute(Name = "Sifra_Rezervacije")] string Sifra_Rezervacije)
        {
            try
            {
                Rezervacija rez = DataProvider.VratiRezervaciju(Sifra_Rezervacije);
                if (rez != null)
                    return Ok(rez);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiRezervacijuId/{Id}")]
        public ActionResult VratiRezervacijuId([FromRoute(Name = "Id")] string Id)
        {
            try
            {
                return Ok(DataProvider.VratiRezervacijuId(Id));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiRezervacije")]
        public ActionResult VratiRezervacije()
        {
            try
            {
                return Ok(DataProvider.VratiRezervacije());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpDelete]
        [Route("ObirisRezervaciju/{Sifra_Rezervacije}")]
        public ActionResult ObrisiRezervaciju([FromRoute(Name = "Sifra_Rezervacije")] string Sifra_Rezervacije)
        {
            try
            {
                DataProvider.ObrisiRezervaciju(Sifra_Rezervacije);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajStatus/{Sifra_Rezervacije}/{status}")]
        public ActionResult AzurirajStatusRezervaciji([FromRoute(Name = "Sifra_Rezervacije")] string Sifra_Rezervacije,
                                                    [FromRoute(Name = "status")] string status)
        {
            try
            {
                DataProvider.AzurirajRezervaciju(Sifra_Rezervacije, status);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("DodajUsluge/{Id}")]
        public ActionResult DodajUsluge([FromRoute(Name = "Id")] string Id, [FromBody] String[] rezervacije)
        {
            try
            {
                DataProvider.DodajUsluge(Id, rezervacije);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

    }


}
