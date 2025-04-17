using Azure.Core;
using B2BWebService.ResponseRequestModels;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace B2BWebService.Services
{
    public class MTService
    {
        private readonly HttpClient _httpClient;
        private SessionHelper _sessionHelper;

        public MTService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public void InitializeSessionHelper(AppDbContext context, IMemoryCache cache)
        {
            _sessionHelper = new SessionHelper(context, cache);
        }
        public async Task<ApiResponse<SessionInfo>?> GetMTSessionFromAstraSession(ResponseRequestModels.SessionInfo sessionInfoAstra) {

            var pwdReq = new GetSparePartsClientWebAccessInfoRequest()
            {
                AppInfo = MD5Hasher.GetAppInfo(),
                Descr = sessionInfoAstra.RequesterUID,
                ExtIdWeb = ""
            };
            var pwdRes = await GetSparePartsClientWebAccessInfo(pwdReq);
            if (pwdRes.Obj != null)
            {
                var logReq = new LoginRequest()
                {
                    Login = sessionInfoAstra.RequesterUID,
                    DeviceUniqID = sessionInfoAstra.DeviceUniqID,
                    PwdHash = pwdRes.Obj.PasswordHash
                };
                var logReqRes = await Login(logReq);
                return logReqRes;
            }
            return null;
        }
        public async Task<ApiResponse<GetUserInfoResponseObj>> GetUserInfo(SessionRequest session)
        {
            var response = await _httpClient.PostAsJsonAsync("api/useraccount/getuserinfo", session);
            var jsonString = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<GetUserInfoResponseObj>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetUserInfo. {apiResponse?.Msg}");
            }

            return apiResponse;
        }
        public async Task<ApiResponse<GetUserInfoPrivateDataResponseObj>> GetUserPrivateData(SessionRequest session)
        {
            var response = await _httpClient.PostAsJsonAsync("api/useraccount/getuserprivatedata", session);
            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<GetUserInfoPrivateDataResponseObj>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetUserPrivateData. {apiResponse?.Msg}");
            }

            return apiResponse;
        }
        public async Task<ApiResponse<SessionInfo>> Login(LoginRequest loginRequestInfo)
        {
            var response = await _httpClient.PostAsJsonAsync("api/useraccount/login", loginRequestInfo);
            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<SessionInfo>>(jsonString);

            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to authenticate user. {apiResponse?.Msg}");
            }

            return apiResponse;
        }
        public async Task<ApiResponse<string>> Logout(SessionRequest session)
        {
            var response = await _httpClient.PostAsJsonAsync("api/useraccount/logout", session);

            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<string>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to Logout. {apiResponse?.Msg}");
            }

            return apiResponse;
        }
        public async Task<ApiResponse<double>> GetCurrentBalance(SessionRequest session)
        {
            var response = await _httpClient.PostAsJsonAsync("api/useraccount/getcurrentbalance", session);

            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<double>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetCurrentBalance. {apiResponse?.Msg}");
            }

            return apiResponse;
        }
        public async Task<ApiResponse<GetUserPhoneResponseObj>> GetUserPhone(SessionRequest session)
        {
            var response = await _httpClient.PostAsJsonAsync("api/useraccount/getuserphone", session);
            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<GetUserPhoneResponseObj>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetUserPhone. {apiResponse?.Msg}");
            }

            return apiResponse;
        }
        public async Task<ApiResponse<List<GetClientAltPayersResponseObj>>> GetClientAltPayers(GetClientAltPayersRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/useraccount/getclientaltpayers", request);

            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetClientAltPayersResponseObj>>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetClientAltPayers. {apiResponse?.Msg}");
            }

            return apiResponse;
        }

        public async Task<List<SimpleResponseObj>> GetCBSites()
        {
            var response = await _httpClient.PostAsync("api/common/getcbsites", null);

            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<SimpleResponseObj>>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetCBSites. {apiResponse?.Msg}");
            }

            return apiResponse.Obj;
        }
        public async Task<List<SimpleResponseObj>> GetCarServicePlaces()
        {
            var response = await _httpClient.PostAsync("api/common/getcarserviceplaces", null);
            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<SimpleResponseObj>>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetCarServicePlaces. {apiResponse?.Msg}");
            }

            return apiResponse.Obj;
        }
        public async Task<List<SimpleResponseObj>> GetCBSitesOnPoint(PointRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/common/getcbsitesonpoint", request);
            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<SimpleResponseObj>>>(jsonString);

            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetCBSitesOnPoint. {apiResponse?.Msg}");
            }

            return apiResponse.Obj;
        }
        public async Task<ApiResponse<GetCBFirmsResponseObj>> GetCBFirms()
        {
            var response = await _httpClient.PostAsync("api/common/getcbfirms", null);
            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<GetCBFirmsResponseObj>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetCBFirms. {apiResponse?.Msg}");
            }

            return apiResponse;
        }
        public async Task<ApiResponse<GetCBSiteInfoResponseObj>> GetCBSiteInfo(SimpleRequestId id)
        {
            var response = await _httpClient.PostAsJsonAsync("api/common/getcbsiteinfo", id);
            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<GetCBSiteInfoResponseObj>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetCBSiteInfo. {apiResponse?.Msg}");
            }

            return apiResponse;
        }


        public async Task<ApiResponse<List<GetCarListResponseObj>>> GetCarList(GetCarListRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/usercar/getcarlist", request);

            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetCarListResponseObj>>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetCarList. {apiResponse?.Msg}");
            }

            return apiResponse;
        }
        public async Task<ApiResponse<List<GetCarMakesResponseObj>>> GetMakes()
        {
            var response = await _httpClient.PostAsync("api/usercar/getmakes", null);

            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetCarMakesResponseObj>>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetMakes. {apiResponse?.Msg}");
            }

            return apiResponse;
        }
        public async Task<ApiResponse<List<GetWarrantyActionsResponseObj>>> GetWarrantyActions(GetWarrantyActionsRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/usercar/getwarrantyactions", request);

            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetWarrantyActionsResponseObj>>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetWarrantyActions. {apiResponse?.Msg}");
            }

            return apiResponse;
        }



        public async Task<List<GetContractDetsResponseObj>> GetContractDets(GetContractDetsRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/autopartsstore/getcontractdets", request);

            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetContractDetsResponseObj>>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetContractDets. {apiResponse?.Msg}");
            }

            return apiResponse.Obj;
        }
        public async Task<ApiResponse<List<GetAutoPartsResponseObj>>> GetAutoParts(GetAutoPartsRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/autopartsstore/getautoparts", request);
            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetAutoPartsResponseObj>>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetAutoParts. {apiResponse?.Msg}");
            }

            return apiResponse;
        }
        public async Task<List<GetAutopartsWholesaleResponseObj>> GetAutoPartsWholeSale(GetAutopartsWholesaleRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/autopartsstore/getautopartswholesale", request);
            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetAutopartsWholesaleResponseObj>>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetAutoPartsWholeSale. {apiResponse?.Msg}");
            }

            return apiResponse.Obj;
        }
        public async Task<ApiResponse<CreateAutopartsWholesaleOrderResponseObj>> CreateAutopartsWholesaleOrder(CreateAutopartsWholesaleOrderRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/autopartsstore/createautopartswholesaleorder", request);
            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<CreateAutopartsWholesaleOrderResponseObj>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to CreateAutopartsWholesaleOrder. {apiResponse?.Msg}");
            }

            return apiResponse;
        }

        public async Task<ApiResponse<GetSalesResponseObj>> GetSales(SessionRequest session)
        {
            var response = await _httpClient.PostAsJsonAsync("api/invoicehistory/getsales", session);
            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<GetSalesResponseObj>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetSales. {apiResponse?.Msg}");
            }

            return apiResponse;
        }
        public async Task<ApiResponse<GetInvoicesResponseObj>> GetInvoices(GetInvoicesRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/invoicehistory/getinvoices", request);
            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<GetInvoicesResponseObj>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetInvoices. {apiResponse?.Msg}");
            }

            return apiResponse;
        }
        public async Task<ApiResponse<List<GetInvoicesDetailsResponseObj>>> GetInvoiceDetails(GetInvoiceDetailsRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/invoicehistory/getinvoicedetails", request);

            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetInvoicesDetailsResponseObj>>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetInvoiceDetails. {apiResponse?.Msg}");
            }

            return apiResponse;
        }
        public async Task<ApiResponse<List<GetActualOrdersResponseObj>>> GetActualOrders(SessionRequest session)
        {
            var response = await _httpClient.PostAsJsonAsync("api/invoicehistory/getactualorders", session);

            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetActualOrdersResponseObj>>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetActualOrders. {apiResponse?.Msg}");
            }

            return apiResponse;
        }
        public async Task<List<GetActualOrdersBySiteResponseObj>> GetActualOrdersBySite(GetActualOrdersBySiteRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/invoicehistory/getactualordersbysite", request);
            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetActualOrdersBySiteResponseObj>>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetActualOrdersBySite. {apiResponse?.Msg}");
            }

            return apiResponse.Obj;
        }
        public async Task<ApiResponse<GetSparePartsClientWebAccessInfoResponseObj>> GetSparePartsClientWebAccessInfo(GetSparePartsClientWebAccessInfoRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/dev/getsparepartsclientwebaccessinfo", request);
            var jsonString = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<GetSparePartsClientWebAccessInfoResponseObj>>(jsonString);
            if (apiResponse == null || !string.IsNullOrEmpty(apiResponse.Msg))
            {
                throw new Exception($"Failed to GetSparePartsClientWebAccessInfo. {apiResponse?.Msg}");
            }

            return apiResponse;
        }
    }
}
