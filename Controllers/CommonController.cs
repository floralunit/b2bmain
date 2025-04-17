using B2BWebService.ResponseRequestModels;
using B2BWebService.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace B2BWebService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CommonController : ControllerBase
    {
        private readonly IB2BService _b2bService;
        public CommonController(IB2BService b2bService)
        {
            _b2bService = b2bService;
        }

        //[HttpGet("GetCBSites")]
        //public async Task<IActionResult> GetCBSites()
        //{
        //    try
        //    {
        //        var response = await _b2bService.GetCarServicePlaces();

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Обработка ошибок
        //        return StatusCode(500, ex.Message);
        //    }
        //}


        [HttpPost("GetCarServicePlaces")]
        public async Task<IActionResult> GetCarServicePlaces()
        {

            try
            {
                var response = await _b2bService.GetCarServicePlaces();

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("GetCBSitesOnPoint")]
        public async Task<IActionResult> GetCBSitesOnPoint(PointRequestWithSession request)
        {
            try
            {
                var response = await _b2bService.GetCBSitesOnPoint(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                return StatusCode(500, ex.Message);
            }
        }
        //[HttpGet("GetCBFirms")]
        //public async Task<IActionResult> GetCBFirms()
        //{
        //    try
        //    {
        //        var response = await Service.GetCBFirms();

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Обработка ошибок
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        //[HttpPost("GetCBSiteInfo")]
        //public async Task<IActionResult> GetCBSiteInfo(SimpleRequestId request)
        //{
        //    try
        //    {
        //        var response = await Service.GetCBSiteInfo(request);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Обработка ошибок
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }

}
