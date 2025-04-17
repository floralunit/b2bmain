using B2BWebService.ResponseRequestModels;
using System.Drawing;

namespace B2BWebService.Services
{
    public interface IB2BService
    {
        Task<ApiResponse<GetUserInfoResponseObj>> GetUserInfo(SessionRequest session);
        Task<ApiResponse<GetUserInfoPrivateDataResponseObj>> GetUserPrivateData(SessionRequest session);
        Task<ApiResponse<SessionInfo>> Login(LoginRequest loginInfo);
        Task<ApiResponse<string>> Logout(SessionRequest session);
        Task<ApiResponse<GetSparePartsClientWebAccessInfoResponseObj>> GetSparePartsClientWebAccessInfo(GetSparePartsClientWebAccessInfoRequest request);

        Task<ApiResponse<List<SimpleResponseObj>>> GetCarServicePlaces();
        Task<ApiResponse<List<SimpleResponseObj>>> GetCBSitesOnPoint(PointRequestWithSession request);
        Task<ApiResponse<List<GetAutopartsWholesaleResponseObj>>> GetAutoPartsWholeSale(GetAutopartsWholesaleRequest request);
        Task<ApiResponse<CreateAutopartsWholesaleOrderResponseObj>> CreateAutopartsWholesaleOrder(CreateAutopartsWholesaleOrderRequest request);
        Task<ApiResponse<List<GetContractDetsResponseObj>>> GetContractDets(GetContractDetsRequest request);
        Task<ApiResponse<List<GetClientAltPayersResponseObj>>> GetClientAltPayers(GetClientAltPayersRequest request);
        Task<ApiResponse<List<GetActualOrdersBySiteResponseObj>>> GetActualOrdersBySite(GetActualOrdersBySiteRequest request);
        Task<ApiResponse<List<GetActualOrdersBySiteResponseObj>>> GetOrdersHistory(GetActualOrdersBySiteRequest request);
    }

}
