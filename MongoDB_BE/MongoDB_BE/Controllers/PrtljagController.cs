using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLibrary;
using AppLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace MongoDB_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrtljagController:Controller
    {

        [HttpPost]
        [Route("KreirajPrtljag")]
        public ActionResult KreirajPrtljag([FromBody] PrtljagDTO prtljag)
        {
            try
            {
                Prtljag p = new Prtljag()
                {
                    Kolicina= prtljag.Kolicina,
                    PostojiPrtljag=prtljag.PostojiPrtljag
                    
                };
                DataProvider.KreirajPrtljag(p);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        [Route("VratiSavPrtljag")]
        public ActionResult VratiSavPrtljag()
        {
            try
            {
                IList<PrtljagDTO> returnList = new List<PrtljagDTO>();
                foreach (Prtljag p in DataProvider.VratiSavPrtljag())
                {
                    returnList.Add(new PrtljagDTO()
                    {
                        Id = p.Id.ToString(),
                        Kolicina=p.Kolicina,
                        PostojiPrtljag=p.PostojiPrtljag
                      
                    });
                }
                return Ok(returnList);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        

        [HttpDelete]
        [Route("ObrisiPrtljag/{prtljagId}")]
        public ActionResult ObrisiPrtljag([FromRoute(Name = "prtljagId")] string prtljagId)
        {
            try
            {
                DataProvider.ObrisiPrtljag(new ObjectId(prtljagId));
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajKolicinuPrtljaga/{idPrtljaga}/{novaKolicina}")]
        public ActionResult AzurirajKolicinuPrtljaga([FromRoute] string idPrtljaga,
                                                            [FromRoute(Name = "novaKolicina")] int novaKolicina)
        {
            try
            {
                DataProvider.AzurirajKolicinuPrtljaga(new ObjectId(idPrtljaga), novaKolicina);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }
    }
}
