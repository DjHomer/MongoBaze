using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppLibrary;
using AppLibrary.Models;
using Microsoft.AspNetCore.Http;


namespace MongoDB_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BusPreduzeceController : Controller
    {
        [HttpPost]
        [Route("KreirajBusPreduzece")]
        public ActionResult KreirajBusPreduzece([FromBody] BusPreduzece busPreduzece)
        {
            try
            {
                DataProvider.KreirajBusPreduzece(busPreduzece);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }

        }

        [HttpGet]
        [Route("VratiBusPreduzeca")]
        public ActionResult VratiBusPreduzeca()
        {
            try
            {
                return new JsonResult(DataProvider.VratiBusPreduzeca());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("VratiBusPreduzece/{id}")]
        public ActionResult VratiBusPreduzece([FromRoute] string id)
        {
            try
            {
                return new JsonResult(DataProvider.VratiBusPreduzece(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpPut]
        [Route("AzurirajBusPreduzece/{id}")]
        public ActionResult AzurirajBusPreduzece([FromRoute] string id, [FromBody] BusPreduzeceDTOUpdate busPreduzece)
        {
            try
            {
                DataProvider.AzurirajBusPreduzece(id, busPreduzece);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }

        [HttpDelete]
        [Route("ObrisiBuzPreduzece/{id}")]
        public IActionResult ObrisiBusPreduzece([FromRoute] string id)
        {
            try
            {
                DataProvider.ObrisiBusPreduzece(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.ToString());
            }
        }


    }
}
