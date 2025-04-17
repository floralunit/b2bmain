using B2BWebService.ResponseRequestModels;
using B2BWebService.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace B2BWebService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserCarController : ControllerBase
    {

        //[HttpPost("GetCarList")]
        //public async Task<IActionResult> GetCarList(GetCarListRequest request)
        //{
        //    try
        //    {
        //        var response = await Service.GetCarList(request);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Обработка ошибок
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        //[HttpGet("GetMakes")]
        //public async Task<IActionResult> GetMakes()
        //{
        //    try
        //    {
        //        var response = await Service.GetMakes();

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Обработка ошибок
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        //[HttpPost("GetWarrantyActions")]
        //public async Task<IActionResult> GetWarrantyActions(GetWarrantyActionsRequest request)
        //{
        //    try
        //    {
        //        var response = await Service.GetWarrantyActions(request);

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
