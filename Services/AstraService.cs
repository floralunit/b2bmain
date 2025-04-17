using Azure.Core;
using B2BWebService.ResponseRequestModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace B2BWebService.Services
{
    public class AstraService
    {
        private AppDbContext _context;
        private SessionHelper _sessionHelper;
        private readonly IConfiguration _configuration;

        public AstraService(AppDbContext context, IConfiguration configuration, IMemoryCache cache)
        {
            _context = context;
            _configuration = configuration;
            _sessionHelper = new SessionHelper(context, cache);
        }
        public async Task<ApiResponse<GetUserInfoResponseObj>> GetUserInfo(SessionRequest session)
        {
            GetUserInfoResponseObj responseObj = new GetUserInfoResponseObj();
            var contractor = await _context.ContractorInfo.FirstOrDefaultAsync(x => x.OuterCode == session.Session.RequesterUID);
            if (contractor != null)
            {
                var contractorId = contractor.Contractor_ID.ToString();
                var result = await _context.UserInfo
                    .FromSqlRaw($"select db.DictBaseName as UserFullName" +
                                $", ccem.Number as Email" +
                                $", ccph.StateCode + ccph.Code + ccph.Number as Phone" +
                                $" from dbo.DictBase db (nolock) " +
                                $"left join dbo.ContractorCommunication ccem (nolock) on ccem.Contractor_ID= db.DictBase_ID " +
                                    $"and ccem.CommunicationSubType_ID in " +
                                    $"('5A6D4D7A-54FD-47AE-A8CD-2F3F8C1BBF80'" +
                                    $", 'EFE304EE-B911-4C3F-BAEE-C03D66F78BAD' )" +
                                $" left join dbo.ContractorCommunication ccph (nolock) on ccph.Contractor_ID= db.DictBase_ID" +
                                    $" and ccph.CommunicationSubType_ID in ('186BC873-CEF4-4D5E-903A-178BC91BBA2D'" +
                                    $", '20E71FC2-4C77-4CB7-BC7D-1D596C749092'" +
                                    $", '8A7CD12A-37E0-481B-8A1C-318609522821'" +
                                    $", '3C405F07-859C-429F-B712-44B8C1D8360D'" +
                                    $", 'DFB39968-6A97-481E-AADB-738393E09D7C')" +
                                $" where db.DictBase_ID  = '{contractorId}'")
                    .FirstOrDefaultAsync();
                responseObj.UserFullName = result?.UserFullName;
                responseObj.ContactPhone = new ContactPhoneInfo() { PhoneNumber = result?.Phone };

                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (regex.IsMatch(session.Session.DeviceUniqID))
                    responseObj.Email = session.Session.DeviceUniqID;
                else
                    responseObj.Email = result?.Email;

                return new ApiResponse<GetUserInfoResponseObj> { ResponseStatus = 0, Obj = responseObj };
            }
            else
                return new ApiResponse<GetUserInfoResponseObj> { ResponseStatus = 1, Msg = "Contractor was not found." }; ;


        }
        public async Task<ApiResponse<GetUserInfoPrivateDataResponseObj>> GetUserPrivateData(SessionRequest session)
        {
            ApiResponse<GetUserInfoPrivateDataResponseObj> response = new ApiResponse<GetUserInfoPrivateDataResponseObj> { Msg = "test" };
            return response;
        }
        public async Task<ApiResponse<SessionInfo>> Login(LoginRequest loginRequestInfo)
        {
            return await _sessionHelper.CreateSession(loginRequestInfo);

        }
        public async Task<ApiResponse<string>> Logout(SessionRequest session)
        {
            return await _sessionHelper.DeleteSession(session.Session);
        }

        public async Task<List<SimpleResponseObj>> GetCBSites()
        {
            var configSitePoints = _configuration.GetSection("Points").Get<List<ConfigSitePoint>>();
            List<SimpleResponseObj> simplePointsList = configSitePoints.Select(point => new SimpleResponseObj
            {
                ID = point.PointID,
                Text = point.Text
            }).ToList();

            return simplePointsList;
        }
        public async Task<List<SimpleResponseObj>> GetCarServicePlaces()
        {
            var configSitePoints = _configuration.GetSection("Points").Get<List<ConfigSitePoint>>();
            List<SimpleResponseObj> simplePointsList = configSitePoints.Select(point => new SimpleResponseObj
            {
                ID = point.PointID,
                Text = point.Text
            }).ToList();

            return simplePointsList;
        }
        public async Task<List<SimpleResponseObj>> GetCBSitesOnPoint(PointRequestWithSession request)
        {
            var configSitePoints = _configuration.GetSection("Points").Get<List<ConfigSitePoint>>();
            var point = configSitePoints.FirstOrDefault(p => p.PointID == request.PointID);

            if (point != null)
            {
                if (request.Session == null)
                {
                    var responseSites = point.Sites.Select(site => new SimpleResponseObj
                    {
                        ID = site.SiteID,
                        Text = site.Text
                    }).ToList();
                    return responseSites;
                }

                var contractor = await _sessionHelper.FindContractorBySession(request.Session);
                if (contractor != null)
                {
                    var contractorId = contractor.Contractor_ID;
                    var sql = new StringBuilder();
                    sql.AppendLine("exec [b2b].[PR_ContractorCenterList_Get] @Contractor_ID=@contractorId");

                    var result = await _context.ContractorSiteList
                                        .FromSqlRaw(sql.ToString(), new SqlParameter("@contractorId", contractorId))
                                        .ToListAsync();

                    var siteList = point.Sites.Where(x => result.Any(y => y.Center_ID.ToString().ToLower() == x.CenterGuid.ToString().ToLower())).ToList();

                    var responseSites = siteList.Select(site => new SimpleResponseObj
                    {
                        ID = site.SiteID,
                        Text = site.Text
                    }).ToList();

                    return responseSites;
                }
                else
                {
                    return null;
                }
               
            }
            else
                return null;
        }
        public async Task<List<GetContractDetsResponseObj>> GetContractDets(List<int> siteIDS, SessionInfo session)
        {
            var contractor = await _sessionHelper.FindContractorBySession(session);
            if (contractor != null)
            {
                var contractorId = contractor.Contractor_ID;
                var sql = new StringBuilder();
                sql.AppendLine("exec [b2b].[PR_GetContractorPricingSchemes] @Contractor_ID=@contractorId");

                var result = await _context.PricingSchemeInfo
                                    .FromSqlRaw(sql.ToString(), new SqlParameter("@contractorId", contractorId))
                                    .ToListAsync();
                List<ContractDetail> contracts = result.Select(x => new ContractDetail()
                {
                    ContractDetID = x.PricingScheme_ID.ToString(),
                    ContractDetName = x.PricingSchemeName,
                    ContractName = x.DogovorNum,
                }).ToList();

                var responseObj = siteIDS.Select(x => new GetContractDetsResponseObj()
                {
                    Point = new PointSiteDto() { PointID = 16, SiteID = x },
                    ContractDets = contracts
                }).ToList();

                return responseObj;
            }
            else
                return new List<GetContractDetsResponseObj>();
        }
        public async Task<List<GetAutopartsWholesaleResponseObj>> GetAutoPartsWholeSale(List<ConfigSite> sites, List<string> partCodes, SessionInfo session, bool? BOnlyAvailable, bool? BAnalog)
        {
            var contractor = await _context.ContractorInfo.FirstOrDefaultAsync(x => x.OuterCode == session.RequesterUID);
            if (contractor != null)
            {
                var contractorId = contractor.Contractor_ID.ToString();
                var sqlScheme = new StringBuilder();
                sqlScheme.AppendLine("exec [b2b].[PR_GetContractorPricingSchemes] @Contractor_ID=@contractorId");

                var contractorSchemes = await _context.PricingSchemeInfo
                                                .FromSqlRaw(sqlScheme.ToString(), new SqlParameter("@contractorId", contractorId))
                                                .ToListAsync();

                XElement xmlCenters = new XElement(
                 "CenterNames", sites.Select(site => new XElement("CenterName", site.Text))
                 );
                XElement xmlPartCodes = new XElement(
                    "PartCodes", partCodes.Select(code => new XElement("PartCode", code))
                    );

                var sqlResult = new StringBuilder();
                sqlResult.AppendLine("exec [b2b].[PR_GetAutoPartsWholeSale] @CenterNames=@p0, @ListPartCodes=@p1, @Contractor_ID=@p2, @BOnlyAvailable=@p3, @BAnalog=@p4");

                var result = await _context.PartsInfo
                    .FromSqlRaw(sqlResult.ToString(),
                            new SqlParameter("@p0", xmlCenters.ToString()),
                            new SqlParameter("@p1", xmlPartCodes.ToString()),
                            new SqlParameter("@p2", contractorId),
                            new SqlParameter("@p3", true),//BOnlyAvailable ?? true),
                            new SqlParameter("@p4", true)//BAnalog ?? true)
                            )
                    .ToListAsync();

                List<GetAutopartsWholesaleResponseObj> autopartsWholesaleResponseObjList = new List<GetAutopartsWholesaleResponseObj>();

                var pointsConfig = _configuration.GetSection("Points").Get<List<ConfigSitePoint>>();
                var allSites = pointsConfig.FirstOrDefault().Sites.ToList();

                foreach (var partCode in partCodes)
                {
                    var resultByPartCode = result.Where(x => x.RequestPartCode == partCode).ToList();
                    var sitesByPartCode = allSites
                        .Where(x => resultByPartCode
                                    .Any(r => r.Center_ID.ToString().ToLower() == x.CenterGuid.ToLower())
                                    || sites.Any(s => s.CenterGuid.ToLower() == x.CenterGuid.ToLower())).ToList();

                    List<SiteResult> siteResults = new List<SiteResult>();
                    foreach (var center in sitesByPartCode)
                    {
                        var resultByCenter = resultByPartCode.Where(x => x.Center_ID.ToString().ToLower() == center.CenterGuid.ToLower()).ToList();
                        List<ContractDetsDetail> contractDets = new List<ContractDetsDetail>();
                        foreach (var scheme in contractorSchemes)
                        {
                            var resultByScheme = resultByCenter.Where(x => x.PricingSchemeName == scheme.PricingSchemeName).ToList();
                            List<Part> parts = new List<Part>();
                            if (resultByScheme.Count > 0)
                            {
                                parts = resultByScheme.GroupBy(x => new { x.PartCode, x.PartName, x.PriceListPrice, x.PriceListPriceWithDiscount, x.Analog, x.Replacement }).Select(part => new Part
                                {
                                    Code = part.Key.PartCode,
                                    Name = part.Key.PartName,
                                    PricePerUnit = part.Key.PriceListPrice,
                                    AvailableQuantity = part.Sum(p => p.Quant), // группировка и суммирование для случая, когда запчасть в одном центре, но в нескольких подразделениях
                                    StdSalePrice = part.Key.PriceListPriceWithDiscount,
                                    IsAnalog = part.Key.Analog,
                                    IsReplacement = part.Key.Replacement,
                                    FoundByCode = partCode

                                }).ToList();
                            }
                            else
                            {
                                if (BOnlyAvailable != true)
                                {
                                    var findedPart = resultByCenter.FirstOrDefault();
                                    var name = findedPart?.PartName;
                                    parts.Add(new Part()
                                    {
                                        Code = partCode,
                                        Name = name,
                                        //PricePerUnit = part.PriceListPrice,
                                        AvailableQuantity = 0,
                                        //StdSalePrice = part.PriceListPriceWithDiscount,
                                        IsAnalog = false,
                                        IsReplacement = false
                                    });
                                }
                            }
                            contractDets.Add(new ContractDetsDetail()
                            {
                                Name = scheme.PricingSchemeName,
                                ID = scheme.PricingScheme_ID.ToString(),
                                Parts = parts,
                                ContractDetIsForOrder = false,
                                ContractNumber = scheme.DogovorNum
                            });
                        }

                        siteResults.Add(new SiteResult()
                        {
                            Point = new PointSiteDto { SiteID = center.SiteID, PointID = 16 },
                            ContractDets = contractDets
                        });
                    }

                    autopartsWholesaleResponseObjList.Add(new GetAutopartsWholesaleResponseObj()
                    {
                        RequestPartCode = partCode,
                        ResultsBySite = siteResults
                    });
                }

                return autopartsWholesaleResponseObjList;
            }
            return new List<GetAutopartsWholesaleResponseObj>();
        }

        public async Task<CreateAutopartsWholesaleOrderResponseObj> CreateAutopartsWholesaleOrder(ConfigSite center, SessionInfo session, List<Order> orders)
        {
            var contractor = await _context.ContractorInfo.FirstOrDefaultAsync(x => x.OuterCode == session.RequesterUID);
            if (contractor != null)
            {
                var contractorId = contractor.Contractor_ID.ToString();

                XElement xmlCenters = new XElement(
              "CenterNames", new XElement("CenterName", center.Text)
              );
                List<OutOrder> createdOrdersList = new List<OutOrder>();
                foreach (var order in orders)
                {
                    XElement xmlPartCodes = new XElement(
                        "PartCodes", order.Parts.Select(code => new XElement("PartCode", code.Code))
                        );
                    var pricingSchemeID = order.ContractDetID.ToString();

                    var sqlParts = new StringBuilder();
                    sqlParts.AppendLine("exec [b2b].[PR_GetAutoPartsWholeSale] @CenterNames=@p0, @PartCodes=@p1, @Contractor_ID=@p2, @PricingScheme_ID=@p3");

                    var searchParts = await _context.PartsInfo
                        .FromSqlRaw(sqlParts.ToString(),
                                new SqlParameter("@p0", xmlCenters.ToString()),
                                new SqlParameter("@p1", xmlPartCodes.ToString()),
                                new SqlParameter("@p2", contractorId),
                                new SqlParameter("@p3", pricingSchemeID))
                        .ToListAsync();

                    var partsResultAll = searchParts.Where(x => x.Center_ID.ToString().ToLower() == center.CenterGuid.ToLower());
                    var department = partsResultAll.Select(x => x.Department_ID).FirstOrDefault();
                    var partsResult = partsResultAll.Where(x => x.Department_ID == department).ToList();

                    XElement xmlOrderParts = new XElement("PartsForOrder",
                    from part in partsResult
                    select new XElement("PartForOrder",
                                new XElement("GoodsAll_ID", part.Parts_ID),
                                new XElement("Quant", order.Parts.Where(x => x.Code == part.PartCode).Select(x => x.Qty).FirstOrDefault()),
                                new XElement("Price", part.PriceListPrice),
                                new XElement("BasePrice", part.PriceListPriceWithDiscount)
                        ));

                    var requestString = new StringBuilder();
                    requestString.AppendLine("exec [b2b].[PR_CreateAutopartsWholesaleOrder]")
                                .Append(" @PricingScheme_ID=@p0,")
                                .Append(" @Center_ID=@p1,")
                                .Append(" @Department_ID=@p2,")
                                .Append(" @Project_ID=@p3,")
                                .Append(" @Contractor_ID=@p4,")
                                .Append("@PartsForOrderXML=@p5");

                    var createdOrder = await _context.CreatedOrderInfo
                        .FromSqlRaw(requestString.ToString(),
                                new SqlParameter("@p0", pricingSchemeID),
                                new SqlParameter("@p1", center.CenterGuid),
                                new SqlParameter("@p2", department),
                                new SqlParameter("@p3", center.ProjectGuid),
                                new SqlParameter("@p4", contractorId),
                                new SqlParameter("@p5", xmlOrderParts.ToString()))
                        .ToListAsync();

                    createdOrdersList.Add(new OutOrder()
                    {
                        ContractDetID = order.ContractDetID.ToString(),
                        OutOrderNumber = createdOrder.FirstOrDefault().DocumentBaseNumber
                    });


                }

                var responseObj = new CreateAutopartsWholesaleOrderResponseObj() { JointOutOrderNumber = createdOrdersList.FirstOrDefault().OutOrderNumber, OutOrders = createdOrdersList };

                return responseObj;
            }
            return new CreateAutopartsWholesaleOrderResponseObj();
        }
        public async Task<List<GetActualOrdersBySiteResponseObj>> GetActualOrdersBySite(List<ConfigSite> centers, SessionInfo session)
        {
            var contractor = await _context.ContractorInfo.FirstOrDefaultAsync(x => x.OuterCode == session.RequesterUID);
            if (contractor != null)
            {
                var contractorId = contractor.Contractor_ID.ToString();

                XElement xmlCenters = new XElement(
                      "Centers", centers.Select(site => new XElement("Center_ID", site.CenterGuid))
                      );

                var result = await _context.OrderInfo
                .FromSqlRaw($"exec [b2b].[PR_GetPartOrders] @CentersXML='{xmlCenters.ToString()}',  @Contractor_ID='{contractorId}', @IsForHistory=0")
                .ToListAsync();

                List<GetActualOrdersBySiteResponseObj> actualOrdersBySiteResponseObjList = new List<GetActualOrdersBySiteResponseObj>();
                var resultCenters = result.Select(x => x.Center_ID.ToString()).Distinct().ToList();
                foreach (var center in resultCenters)
                {
                    var resultByCenter = result.Where(x => x.Center_ID.ToString() == center).ToList();
                    var resultDocs = resultByCenter.Select(x => x.DocumentSpecification_ID).Distinct().ToList();
                    List<AutoPartRequest_GetActualOrdersBySite> ordersByCenter = new List<AutoPartRequest_GetActualOrdersBySite>();
                    foreach (var specDoc in resultDocs)
                    {
                        var resultBySpec = resultByCenter.Where(x => x.DocumentSpecification_ID == specDoc);
                        var resultSaleRows = resultBySpec.Select(x => x.DocumentRowBase_ID).ToList();
                        foreach (var row in resultSaleRows)
                        {
                            var resultByRow = resultBySpec.Where(x => x.DocumentRowBase_ID == row).FirstOrDefault();
                            var orderRow = new AutoPartRequest_GetActualOrdersBySite()
                            {
                                OutOrderDocNum = resultByRow?.OutOrderDocNum,
                                DetCostWithDiscount = resultByRow?.Summa,
                                DetMeasUnit = resultByRow?.DetMeasUnit,
                                DetName = resultByRow?.DetName,
                                DetQty = resultByRow?.Quant,
                                DetSparePartCode = resultByRow?.DetSparePartCode,
                                DetStatus = resultByRow?.DetStatus,
                                DetStatusName = resultByRow?.DetStatusName,
                                DetSum = resultByRow?.Summa,
                                DetCostWithoutDiscount = resultByRow?.SummaWithoutDiscount,
                                DetSparePartStatus = 1,
                                DetSparePartStatusName = "Запчасти в наличии"

                            };
                            ordersByCenter.Add(orderRow);
                        }

                    }
                    var site = centers.FirstOrDefault(x => x.CenterGuid.ToLower() == center.ToLower());
                    actualOrdersBySiteResponseObjList.Add(new GetActualOrdersBySiteResponseObj()
                    {
                        PointID = 16,
                        CBSiteID = site?.SiteID,
                        SiteName = site?.Text,
                        AutoPartRequests = ordersByCenter
                    });
                }

                return actualOrdersBySiteResponseObjList;
            }
            return new List<GetActualOrdersBySiteResponseObj>();
        }
        public async Task<ApiResponse<GetSparePartsClientWebAccessInfoResponseObj>> GetSparePartsClientWebAccessInfo(GetSparePartsClientWebAccessInfoRequest request)
        {
            var mtCode = request.Descr;
            var contractor = await _context.ContractorInfo.FirstOrDefaultAsync(c => c.OuterCode == mtCode);

            ApiResponse<GetSparePartsClientWebAccessInfoResponseObj> response = new ApiResponse<GetSparePartsClientWebAccessInfoResponseObj>();
            if (contractor == null)
            {
                response.Msg = "Client was not found";
                response.ResponseStatus = 1;
                return response;
            }

            if (contractor.Activity == false)
            {
                response.Msg = "Client is disabled";
                response.ResponseStatus = 1;
                return response;
            }

            contractor.ExtIdWeb = request.ExtIdWeb;
            contractor.UpdDate = DateTime.Now;
            var updUser = Guid.TryParse("B5C8A3E3-CF6F-44B3-83BF-68ACA010B473", out var guid);
            contractor.UpdApplicationUser_ID = guid;

            await _context.SaveChangesAsync();


            var responseObj = new GetSparePartsClientWebAccessInfoResponseObj()
            {
                Login = request.Descr,
                PasswordHash = contractor.PwdHash,
                WebAccessStatus = 0
            };

            response.ResponseStatus = 0;
            response.Obj = responseObj;

            return response;
        }

        public async Task<List<GetActualOrdersBySiteResponseObj>> GetOrdersHistory(List<ConfigSite> centers, SessionInfo session)
        {

            var contractor = await _context.ContractorInfo.FirstOrDefaultAsync(x => x.OuterCode == session.RequesterUID);
            if (contractor != null)
            {
                var contractorId = contractor.Contractor_ID.ToString();

                XElement xmlCenters = new XElement(
                      "Centers", centers.Select(site => new XElement("Center_ID", site.CenterGuid))
                      );

                var result = await _context.OrderInfo
                .FromSqlRaw($"exec [b2b].[PR_GetPartOrders] @CentersXML='{xmlCenters.ToString()}',  @Contractor_ID='{contractorId}', @IsForHistory=1")
                .ToListAsync();

                List<GetActualOrdersBySiteResponseObj> actualOrdersBySiteResponseObjList = new List<GetActualOrdersBySiteResponseObj>();
                var resultCenters = result.Select(x => x.Center_ID.ToString()).Distinct().ToList();
                foreach (var center in resultCenters)
                {
                    var resultByCenter = result.Where(x => x.Center_ID.ToString() == center).ToList();
                    var resultDocs = resultByCenter.Select(x => x.DocumentSpecification_ID).Distinct().ToList();
                    List<AutoPartRequest_GetActualOrdersBySite> ordersByCenter = new List<AutoPartRequest_GetActualOrdersBySite>();
                    foreach (var specDoc in resultDocs)
                    {
                        var resultBySpec = resultByCenter.Where(x => x.DocumentSpecification_ID == specDoc);
                        var resultSaleRows = resultBySpec.Select(x => x.DocumentRowBase_ID).ToList();
                        foreach (var row in resultSaleRows)
                        {
                            var resultByRow = resultBySpec.Where(x => x.DocumentRowBase_ID == row).FirstOrDefault();
                            var orderRow = new AutoPartRequest_GetActualOrdersBySite()
                            {
                                OutOrderDocNum = resultByRow?.OutOrderDocNum,
                                DetCostWithDiscount = resultByRow?.Summa,
                                DetMeasUnit = resultByRow?.DetMeasUnit,
                                DetName = resultByRow?.DetName,
                                DetQty = resultByRow?.Quant,
                                DetSparePartCode = resultByRow?.DetSparePartCode,
                                DetStatus = resultByRow?.DetStatus,
                                DetStatusName = resultByRow?.DetStatusName,
                                DetSum = resultByRow?.Summa,
                                DetCostWithoutDiscount = resultByRow?.SummaWithoutDiscount,
                                DetSparePartStatus = 3,
                                DetSparePartStatusName = "Запчасти в архиве"

                            };
                            ordersByCenter.Add(orderRow);
                        }

                    }
                    var site = centers.FirstOrDefault(x => x.CenterGuid.ToLower() == center.ToLower());
                    actualOrdersBySiteResponseObjList.Add(new GetActualOrdersBySiteResponseObj()
                    {
                        PointID = 16,
                        CBSiteID = site?.SiteID,
                        SiteName = site?.Text,
                        AutoPartRequests = ordersByCenter
                    });
                }

                return actualOrdersBySiteResponseObjList;
            }
            return new List<GetActualOrdersBySiteResponseObj>();
        }
    }

}
