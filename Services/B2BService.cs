
using B2BWebService.ResponseRequestModels;
using B2BWebService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

public class B2BService : IB2BService
{
    private readonly string SESSION_ERROR_MESSAGE = "Ваша сессия истекла. Пожалуйста, переавторизуйтесь.";
    private readonly IConfiguration _configuration;
    private readonly MTService _mtService;
    private readonly AstraService _astraService;
    private SessionHelper _sessionHelper;

    public B2BService(AppDbContext context, IConfiguration configuration, IMemoryCache cache, MTService mtService, AstraService astraService)
    {
        _configuration = configuration;
        _mtService = mtService;
        _astraService = astraService;
        _sessionHelper = new SessionHelper(context, cache);
    }

    public async Task<ApiResponse<List<SimpleResponseObj>>> GetCarServicePlaces()
    {
        //var astraPoints = await _astraService.GetCarServicePlaces();
        //var mtPoints = await _mtService.GetCarServicePlaces();
        //var responseObj = astraPoints.Union(mtPoints).ToList();

        var tasks = new List<Task<List<SimpleResponseObj>>>();

        tasks.Add(_astraService.GetCarServicePlaces());

        try
        {
            tasks.Add(_mtService.GetCarServicePlaces());
        }
        catch { }

        var results = await Task.WhenAll(tasks);

        var responsesObj = new List<SimpleResponseObj>();

        foreach (var result in results)
        {
            responsesObj.AddRange(result);
        }

        if (responsesObj.Count != 0)
        {
            return new ApiResponse<List<SimpleResponseObj>>() { ResponseStatus = 0, Obj = responsesObj };
        }
        else
            return new ApiResponse<List<SimpleResponseObj>>() { ResponseStatus = 1, Msg = "Ничего не найдено" };
    }


    public async Task<ApiResponse<List<SimpleResponseObj>>> GetCBSitesOnPoint(PointRequestWithSession request)
    {
        var obj = new List<SimpleResponseObj>();
        var astraSites = await _astraService.GetCBSitesOnPoint(request);
        if (astraSites != null)
        {
            return new ApiResponse<List<SimpleResponseObj>>() { ResponseStatus = 0, Obj = astraSites };
        }

        try
        {
            var pointRequest = new PointRequest()
            {
                PointID = request.PointID,
            };
            var mtSites = await _mtService.GetCBSitesOnPoint(pointRequest);
            if (mtSites != null)
            {
                return new ApiResponse<List<SimpleResponseObj>>() { ResponseStatus = 0, Obj = mtSites };
            }
        }
        catch { }

        return new ApiResponse<List<SimpleResponseObj>>() { ResponseStatus = 1, Msg = "Ничего не найдено" };
    }

    public async Task<ApiResponse<GetUserInfoResponseObj>> GetUserInfo(SessionRequest session)
    {
        if (await _sessionHelper.CheckClientSession(session.Session) != true)
            return new ApiResponse<GetUserInfoResponseObj>() { ResponseStatus = 1, Msg = SESSION_ERROR_MESSAGE };

        return await _astraService.GetUserInfo(session);
    }
    public async Task<ApiResponse<GetUserInfoPrivateDataResponseObj>> GetUserPrivateData(SessionRequest session)
    {
        if (await _sessionHelper.CheckClientSession(session.Session) != true)
            return new ApiResponse<GetUserInfoPrivateDataResponseObj>() { ResponseStatus = 1, Msg = SESSION_ERROR_MESSAGE };

        return await _astraService.GetUserPrivateData(session);
    }
    public async Task<ApiResponse<SessionInfo>> Login(LoginRequest loginRequestInfo)
    {
        return await _astraService.Login(loginRequestInfo);
    }
    public async Task<ApiResponse<string>> Logout(SessionRequest session)
    {
        return await _astraService.Logout(session);
    }

    public async Task<ApiResponse<GetSparePartsClientWebAccessInfoResponseObj>> GetSparePartsClientWebAccessInfo(GetSparePartsClientWebAccessInfoRequest request)
    {
        return await _astraService.GetSparePartsClientWebAccessInfo(request);
    }

    public async Task<ApiResponse<List<GetAutopartsWholesaleResponseObj>>> GetAutoPartsWholeSale(GetAutopartsWholesaleRequest request)
    {
        if (await _sessionHelper.CheckClientSession(request.Session) != true)
            return new ApiResponse<List<GetAutopartsWholesaleResponseObj>>() { ResponseStatus = 1, Msg = SESSION_ERROR_MESSAGE };

        var pointsConfig = _configuration.GetSection("Points").Get<List<ConfigSitePoint>>();

        var astrasRequestsPoints = pointsConfig.Where(r =>
            request.Points.Any(p => p.PointID == r.PointID))
            .ToList();

        var mtRequestsPoints = request.Points.Where(r =>
            !pointsConfig.Any(p => p.PointID == r.PointID))
            .ToList();

        var tasks = new List<Task<List<GetAutopartsWholesaleResponseObj>>>();

        if (astrasRequestsPoints.Any())
        {
            var astraPoint = astrasRequestsPoints.FirstOrDefault();
            var siteIDS = request.Points.Select(x => x.SiteID);
            tasks.Add(_astraService.GetAutoPartsWholeSale(astraPoint.Sites.Where(x => siteIDS.Contains(x.SiteID)).ToList(), request.PartCodes, request.Session, request.BOnlyAvailable, request.BAnalog));
        }

        if (mtRequestsPoints.Any())
        {
            try
            {
                var logReqRes = await _mtService.GetMTSessionFromAstraSession(request.Session);
                if (logReqRes?.Obj != null)
                {
                    var requestForMt = new GetAutopartsWholesaleRequest()
                    {
                        Session = logReqRes.Obj,
                        PartCodes = request.PartCodes,
                        Points = mtRequestsPoints
                    };
                    tasks.Add(_mtService.GetAutoPartsWholeSale(requestForMt));
                }
            }
            catch { }
        }

        var results = await Task.WhenAll(tasks);

        var responsesObj = new List<GetAutopartsWholesaleResponseObj>();

        foreach (var result in results)
        {
            responsesObj.AddRange(result);
        }

        return new ApiResponse<List<GetAutopartsWholesaleResponseObj>>() { ResponseStatus = 0, Obj = responsesObj };
    }

    public async Task<ApiResponse<CreateAutopartsWholesaleOrderResponseObj>> CreateAutopartsWholesaleOrder(CreateAutopartsWholesaleOrderRequest request)
    {
        if (await _sessionHelper.CheckClientSession(request.Session) != true)
            return new ApiResponse<CreateAutopartsWholesaleOrderResponseObj>() { ResponseStatus = 1, Msg = SESSION_ERROR_MESSAGE };

        var pointsConfig = _configuration.GetSection("Points").Get<List<ConfigSitePoint>>();

        var astraRequestsPoint = pointsConfig.Where(r =>
            request.Point.PointID == r.PointID)
            .FirstOrDefault();

        var response = new ApiResponse<CreateAutopartsWholesaleOrderResponseObj>();

        if (astraRequestsPoint != null)
        {
            var astraSite = astraRequestsPoint.Sites.Where(x => x.SiteID == request.Point.SiteID).FirstOrDefault();
            var responseObj = await _astraService.CreateAutopartsWholesaleOrder(astraSite, request.Session, request.Orders);
            response = new ApiResponse<CreateAutopartsWholesaleOrderResponseObj>() { ResponseStatus = 0, Obj = responseObj };
        }
        else
        {
            try
            {
                var logReqRes = await _mtService.GetMTSessionFromAstraSession(request.Session);
                if (logReqRes?.Obj != null)
                {
                    var requestForMt = new CreateAutopartsWholesaleOrderRequest()
                    {
                        Session = logReqRes.Obj,
                        Point = request.Point,
                        ReservePart = request.ReservePart,
                        Orders = request.Orders
                    };
                    response = await _mtService.CreateAutopartsWholesaleOrder(requestForMt);
                }
            }
            catch { }
        }

        return response;
    }

    public async Task<ApiResponse<List<GetContractDetsResponseObj>>> GetContractDets(GetContractDetsRequest request)
    {
        if (await _sessionHelper.CheckClientSession(request.Session) != true)
            return new ApiResponse<List<GetContractDetsResponseObj>>() { ResponseStatus = 1, Msg = SESSION_ERROR_MESSAGE };

        var pointsConfig = _configuration.GetSection("Points").Get<List<ConfigSitePoint>>();

        var tasks = new List<Task<List<GetContractDetsResponseObj>>>();

        var astraPoint = pointsConfig.FirstOrDefault();
        tasks.Add(_astraService.GetContractDets(astraPoint.Sites.Select(x => x.SiteID).ToList(), request.Session));

        try
        {
            var logReqRes = await _mtService.GetMTSessionFromAstraSession(request.Session);
            if (logReqRes?.Obj != null)
            {
                var requestForMt = new GetContractDetsRequest()
                {
                    Session = logReqRes.Obj
                };
                tasks.Add(_mtService.GetContractDets(requestForMt));
            }
        }
        catch { }

        var results = await Task.WhenAll(tasks);

        var responsesObj = new List<GetContractDetsResponseObj>();

        foreach (var result in results)
        {
            responsesObj.AddRange(result);
        }

        return new ApiResponse<List<GetContractDetsResponseObj>>() { ResponseStatus = 0, Obj = responsesObj };
    }

    public async Task<ApiResponse<List<GetClientAltPayersResponseObj>>> GetClientAltPayers(GetClientAltPayersRequest request)
    {
        var pointsConfig = _configuration.GetSection("Points").Get<List<ConfigSitePoint>>();

        var astraPoint = pointsConfig.FirstOrDefault();
        var responsesObj = astraPoint.Sites.Select(x => new GetClientAltPayersResponseObj()
        {
            Point = new PointSiteStringDto() { PointID = "16", SiteID = x.SiteID.ToString() },
            HasError = false,
            ClientAltPayers = new List<ClientAltPayerModel>()
        }).ToList();

        try
        {
            var logReqRes = await _mtService.GetMTSessionFromAstraSession(request.Session);
            if (logReqRes?.Obj != null)
            {
                var requestForMt = new GetClientAltPayersRequest()
                {
                    Session = logReqRes.Obj
                };
                var mtres = await _mtService.GetClientAltPayers(requestForMt);
                if (mtres != null)
                {
                    responsesObj.Concat(mtres.Obj);
                }
            }
        }
        catch { }

        return new ApiResponse<List<GetClientAltPayersResponseObj>>() { ResponseStatus = 0, Obj = responsesObj };
    }

    public async Task<ApiResponse<List<GetActualOrdersBySiteResponseObj>>> GetActualOrdersBySite(GetActualOrdersBySiteRequest request)
    {
        if (await _sessionHelper.CheckClientSession(request.Session) != true)
            return new ApiResponse<List<GetActualOrdersBySiteResponseObj>>() { ResponseStatus = 1, Msg = SESSION_ERROR_MESSAGE };

        var pointsConfig = _configuration.GetSection("Points").Get<List<ConfigSitePoint>>();

        var astraPoint = pointsConfig.FirstOrDefault();

        var tasks = new List<Task<List<GetActualOrdersBySiteResponseObj>>>();

        tasks.Add(_astraService.GetActualOrdersBySite(astraPoint.Sites.ToList(), request.Session));

        try
        {
            var logReqRes = await _mtService.GetMTSessionFromAstraSession(request.Session);
            if (logReqRes?.Obj != null)
            {
                var requestForMt = new GetActualOrdersBySiteRequest()
                {
                    Session = logReqRes.Obj
                };
                tasks.Add(_mtService.GetActualOrdersBySite(requestForMt));
            }
        }
        catch { }

        var results = await Task.WhenAll(tasks);

        var responsesObj = new List<GetActualOrdersBySiteResponseObj>();

        foreach (var result in results)
        {
            responsesObj.AddRange(result);
        }

        return new ApiResponse<List<GetActualOrdersBySiteResponseObj>>() { ResponseStatus = 0, Obj = responsesObj };
    }

    public async Task<ApiResponse<List<GetActualOrdersBySiteResponseObj>>> GetOrdersHistory(GetActualOrdersBySiteRequest request)
    {
        if (await _sessionHelper.CheckClientSession(request.Session) != true)
            return new ApiResponse<List<GetActualOrdersBySiteResponseObj>>() { ResponseStatus = 1, Msg = SESSION_ERROR_MESSAGE };

        var pointsConfig = _configuration.GetSection("Points").Get<List<ConfigSitePoint>>();

        var astraPoint = pointsConfig.FirstOrDefault();


        var result = await _astraService.GetOrdersHistory(astraPoint.Sites.ToList(), request.Session);



        return new ApiResponse<List<GetActualOrdersBySiteResponseObj>>() { ResponseStatus = 0, Obj = result };
    }
}
