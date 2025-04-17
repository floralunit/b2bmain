using B2BWebService.ResponseRequestModels;
using Microsoft.Extensions.Caching.Memory;
namespace B2BWebService.Services
{
    public interface IService
    {
        void InitializeSessionHelper(AppDbContext context, IMemoryCache cache);
        Task<ApiResponse<GetUserInfoResponseObj>> GetUserInfo(SessionRequest session);
        Task<ApiResponse<GetUserInfoPrivateDataResponseObj>> GetUserPrivateData(SessionRequest session);
        Task<ApiResponse<SessionInfo>> Login(LoginRequest loginInfo);
        Task<ApiResponse<string>> Logout(SessionRequest session);
        Task<ApiResponse<double>> GetCurrentBalance(SessionRequest session);
        Task<ApiResponse<GetUserPhoneResponseObj>> GetUserPhone(SessionRequest session);
        Task<ApiResponse<List<GetClientAltPayersResponseObj>>> GetClientAltPayers(GetClientAltPayersRequest request);

        Task<ApiResponse<GetSparePartsClientWebAccessInfoResponseObj>> GetSparePartsClientWebAccessInfo(GetSparePartsClientWebAccessInfoRequest request);

        Task<ApiResponse<List<SimpleResponseObj>>> GetCBSites();
        Task<ApiResponse<List<SimpleResponseObj>>> GetCarServicePlaces();
        Task<ApiResponse<List<SimpleResponseObj>>> GetCBSitesOnPoint(PointRequest point);
        Task<ApiResponse<GetCBFirmsResponseObj>> GetCBFirms();
        Task<ApiResponse<GetCBSiteInfoResponseObj>> GetCBSiteInfo(SimpleRequestId id);

        Task<ApiResponse<List<GetCarListResponseObj>>> GetCarList(GetCarListRequest request);
        Task<ApiResponse<List<GetCarMakesResponseObj>>> GetMakes();
        Task<ApiResponse<List<GetWarrantyActionsResponseObj>>> GetWarrantyActions(GetWarrantyActionsRequest request);

        Task<ApiResponse<List<GetContractDetsResponseObj>>> GetContractDets(GetContractDetsRequest request);
        Task<ApiResponse<List<GetAutoPartsResponseObj>>> GetAutoParts(GetAutoPartsRequest request);
        Task<List<GetAutopartsWholesaleResponseObj>> GetAutoPartsWholeSale(GetAutopartsWholesaleRequest request);
        Task<ApiResponse<CreateAutopartsWholesaleOrderResponseObj>> CreateAutopartsWholesaleOrder(CreateAutopartsWholesaleOrderRequest request);

        Task<ApiResponse<GetSalesResponseObj>> GetSales(SessionRequest request);
        Task<ApiResponse<GetInvoicesResponseObj>> GetInvoices(GetInvoicesRequest request);
        Task<ApiResponse<List<GetInvoicesDetailsResponseObj>>> GetInvoiceDetails(GetInvoiceDetailsRequest request);
        Task<ApiResponse<List<GetActualOrdersResponseObj>>> GetActualOrders(SessionRequest request);
        Task<ApiResponse<List<GetActualOrdersBySiteResponseObj>>> GetActualOrdersBySite(GetActualOrdersBySiteRequest request);



    }


}
