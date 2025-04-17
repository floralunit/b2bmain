using B2BWebService.ResponseRequestModels;

namespace B2BWebService.Services
{
    public interface ISessionHelper
    {
        Task<bool> CheckClientSession(SessionInfo sessionInfo);
        Task<bool> CheckClientExist(string mtCode);
        Task<ApiResponse<string>> DeleteSession(SessionInfo sessionInfo);
    }
}
