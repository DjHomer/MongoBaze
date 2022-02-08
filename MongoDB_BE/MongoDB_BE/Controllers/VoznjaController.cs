using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using AppLibrary;
using AppLibrary.Models;

namespace MongoDB_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VoznjaController : ControllerBase
    {


        [HttpPost]
        [Route("KreirajVoznju")]
        public ActionResult KreirajVoznju([FromBody] VoznjaDTO voznja)
        {
            try
            {
                Voznja novaVoznja = new Voznja()
                {
                    PolazniGrad = voznja.PolazniGrad,
                    DolazniGrad = voznja.DolazniGrad,
                    CenaVoznje = voznja.CenaVoznje,
                    DatumVoznje = voznja.DatumVoznje,
                    BrojSedista = voznja.BrojSedista,
                    BrojPreostalihSedista = voznja.BrojPreostalihSedista,
                    TipVoznje = voznja.TipVoznje,
                    BusPreduzece = new ObjectId(voznja.BusPreduzece)
                };
                DataProvider.KreirajVoznju(novaVoznja);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

        }

        [HttpGet]
        [Route("VratiSveVoznje")]
        public ActionResult VratiSveVoznje()
        {
            try
            {
                return new JsonResult(DataProvider.VratiSveVoznje());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiSveVoznjeObjectId")]
        public ActionResult VratiSveVoznjeObjectId()
        {
            try
            {
                return new JsonResult(DataProvider.VratiSveVoznjeObjectId());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiSveGotoveVoznje")]
        public ActionResult VratiSveGotoveVoznje()
        {
            try
            {
                return new JsonResult(DataProvider.VratiSveGotoveVoznje());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiSveAktivneVoznje")]
        public ActionResult VratiSveAktivneVoznje()
        {
            try
            {
                return new JsonResult(DataProvider.VratiSveAktivneVoznje());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiVoznju/{id}")]
        public ActionResult VratiVoznju([FromRoute] string id)
        {
            try
            {
                return new JsonResult(DataProvider.VratiVoznju(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajVoznju/{id}")]
        public ActionResult AzurirajVoznju([FromRoute] string id, [FromBody] VoznjaDTO voznja)
        {
            try
            {
                DataProvider.AzurirajVoznju(id, voznja);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiVoznju/{id}")]
        public ActionResult ObrisiVoznju([FromRoute] string id)
        {
            try
            {
                DataProvider.ObrisiVoznju(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }
    }
}
