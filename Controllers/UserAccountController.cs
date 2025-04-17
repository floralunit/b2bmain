using B2BWebService.ResponseRequestModels;
using B2BWebService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace B2BWebService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ControllerBase
    {
        private readonly IB2BService _b2bService;

        public UserAccountController(IB2BService b2bService)
        {
            _b2bService = b2bService;
        }

        [HttpPost("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo([FromBody] SessionRequest userInfoRequest)
        {
            try
            {
                var userInfoResponse = await _b2bService.GetUserInfo(userInfoRequest);
                return Ok(userInfoResponse);

            }
            catch (Exception ex)
            {
                // Обработка ошибок
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("GetUserPrivateData")]
        public async Task<IActionResult> GetUserPrivateData([FromBody] SessionRequest request)
        {
            try
            {
                var userInfoResponse = await _b2bService.GetUserPrivateData(request);

                return Ok(userInfoResponse);
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] ResponseRequestModels.LoginRequest loginRequest)
        {

            try
            {
                //Service.InitializeSessionHelper(_context, _cache);
                var loginResponse = await _b2bService.Login(loginRequest);

                return Ok(loginResponse);

            }
            catch (Exception ex)
            {
                // Обработка ошибок
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] SessionRequest sessionRequest)
        {

            try
            {
                //Service.InitializeSessionHelper(_context, _cache);
                var response = await _b2bService.Logout(sessionRequest);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                return StatusCode(500, ex.Message);
            }
        }
        //[HttpPost("GetCurrentBalance")]
        //public async Task<IActionResult> GetCurrentBalance([FromBody] SessionRequest sessionRequest)
        //{

        //    try
        //    {
        //        var response = await Service.GetCurrentBalance(sessionRequest);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Обработка ошибок
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        //[HttpPost("GetUserPhone")]
        //public async Task<IActionResult> GetUserPhone([FromBody] SessionRequest sessionRequest)
        //{

        //    try
        //    {
        //        var response = await Service.GetUserPhone(sessionRequest);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Обработка ошибок
        //        return StatusCode(500, ex.Message);
        //    }
        //}
        [HttpPost("GetClientAltPayers")]
        public async Task<IActionResult> GetClientAltPayers([FromBody] ResponseRequestModels.GetClientAltPayersRequest request)
        {
            try
            {
                var response = await _b2bService.GetClientAltPayers(request);

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
