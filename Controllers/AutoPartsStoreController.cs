using B2BWebService.ResponseRequestModels;
using B2BWebService.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace B2BWebService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AutoPartsStoreController : ControllerBase
    {
        private readonly IB2BService _b2bService;
        public AutoPartsStoreController(IB2BService b2bService)
        {
            _b2bService = b2bService;
        }

        [HttpPost("GetContractDets")]
        public async Task<IActionResult> GetContractDets(GetContractDetsRequest request)
        {
            try
            {
                var response = await _b2bService.GetContractDets(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                return StatusCode(500, ex.Message);
            }
        }
        //[HttpPost("GetAutoParts")]
        //public async Task<IActionResult> GetAutoParts(GetAutoPartsRequest request)
        //{
        //    try
        //    {
        //        var response = await Service.GetAutoParts(request);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Обработка ошибок
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        [HttpPost("GetAutoPartsWholeSale")]
        public async Task<IActionResult> GetAutoPartsWholeSale(GetAutopartsWholesaleRequest request)
        {
            try
            {
                var response = await _b2bService.GetAutoPartsWholeSale(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("CreateAutopartsWholesaleOrder")]
        public async Task<IActionResult> CreateAutopartsWholesaleOrder(CreateAutopartsWholesaleOrderRequest request)
        {
            try
            {
                var response = await _b2bService.CreateAutopartsWholesaleOrder(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                return StatusCode(500, ex.Message);
            }
        }

    }

}
