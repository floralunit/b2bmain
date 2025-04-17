using B2BWebService.ResponseRequestModels;
using B2BWebService.Services;
using Microsoft.AspNetCore.Mvc;

namespace B2BWebService.Controllers;


[ApiController]
[Route("api/[controller]")]
public class DevController : ControllerBase
{
    private readonly IB2BService _b2bService;
    public DevController(IB2BService b2bService)
    {
        _b2bService = b2bService;
    }

    [HttpPost("GetSparePartsClientWebAccessInfo")]
    public async Task<IActionResult> GetSparePartsClientWebAccessInfo(GetSparePartsClientWebAccessInfoRequest request)
    {
        try
        {
            var response = await _b2bService.GetSparePartsClientWebAccessInfo(request);

            return Ok(response);
        }
        catch (Exception ex)
        {
            // Обработка ошибок
            return StatusCode(500, ex.Message);
        }
    }
    [HttpGet("GetDate")]
    public async Task<IActionResult> GetDate()
    {
        return Ok(DateTime.Now);

    }


}


