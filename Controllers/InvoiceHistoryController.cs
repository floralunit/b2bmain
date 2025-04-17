using B2BWebService.ResponseRequestModels;
using B2BWebService.Services;
using Microsoft.AspNetCore.Mvc;

namespace B2BWebService.Controllers;


[ApiController]
[Route("api/[controller]")]
public class InvoiceHistoryController : ControllerBase
{
    private readonly IB2BService _b2bService;
    public InvoiceHistoryController(IB2BService b2bService)
    {
        _b2bService = b2bService;
    }

    //[HttpPost("GetSales")]
    //public async Task<IActionResult> GetSales(SessionRequest request)
    //{
    //    try
    //    {
    //        var response = await Service.GetSales(request);

    //        return Ok(response);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Обработка ошибок
    //        return StatusCode(500, ex.Message);
    //    }
    //}
    //[HttpPost("GetInvoices")]
    //public async Task<IActionResult> GetInvoices(GetInvoicesRequest request)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }
    //    try
    //    {
    //        var response = await Service.GetInvoices(request);

    //        return Ok(response);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Обработка ошибок
    //        return StatusCode(500, ex.Message);
    //    }
    //}
    //[HttpPost("GetInvoiceDetails")]
    //public async Task<IActionResult> GetInvoiceDetails(GetInvoiceDetailsRequest request)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }
    //    try
    //    {
    //        var response = await Service.GetInvoiceDetails(request);

    //        return Ok(response);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Обработка ошибок
    //        return StatusCode(500, ex.Message);
    //    }
    //}
    //[HttpPost("GetActualOrders")]
    //public async Task<IActionResult> GetActualOrders([FromBody] SessionRequest request)
    //{
    //    try
    //    {
    //        var userInfoResponse = await Service.GetActualOrders(request);

    //        return Ok(userInfoResponse);
    //    }
    //    catch (Exception ex)
    //    {
    //        // Обработка ошибок
    //        return StatusCode(500, ex.Message);
    //    }
    //}
    [HttpPost("GetActualOrdersBySite")]
    public async Task<IActionResult> GetActualOrdersBySite(GetActualOrdersBySiteRequest request)
    {
        try
        {
            var response = await _b2bService.GetActualOrdersBySite(request);

            return Ok(response);
        }
        catch (Exception ex)
        {
            // Обработка ошибок
            return StatusCode(500, ex.Message);
        }
    }
    [HttpPost("GetOrdersHistory")]
    public async Task<IActionResult> GetOrdersHistory(GetActualOrdersBySiteRequest request)
    {
        try
        {
            var response = await _b2bService.GetOrdersHistory(request);

            return Ok(response);
        }
        catch (Exception ex)
        {
            // Обработка ошибок
            return StatusCode(500, ex.Message);
        }
    }


}

