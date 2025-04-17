using Azure.Core;
using B2BWebService.DBModels;
using B2BWebService.ResponseRequestModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace B2BWebService.Services
{
    public class CachedSession
    {
        public string MTCode { get; set; }
        public DateTime Expiration { get; set; }
    }
    public class SessionHelper : ISessionHelper
    {
        private readonly IMemoryCache _cache;
        private readonly AppDbContext _context;
        private const int SessionDurationMinutes = 5000;
        public SessionHelper(AppDbContext context, IMemoryCache cache)
        {
            _cache = cache;
            _context = context;
        }
        public async Task<ApiResponse<SessionInfo>> CreateSession(ResponseRequestModels.LoginRequest loginRequestInfo)
        {
            var contractor = await _context.ContractorInfo.FirstOrDefaultAsync(c => c.OuterCode == loginRequestInfo.Login && c.Activity == true && c.PwdHash == loginRequestInfo.PwdHash);
            if (contractor != null)
            {
                var sessionId = Guid.NewGuid().ToString();
                var expiration = DateTime.UtcNow.AddMinutes(SessionDurationMinutes);
                var cachedSession = new CachedSession
                {
                    MTCode = loginRequestInfo.Login,
                    Expiration = expiration
                };

                _cache.Set(sessionId, cachedSession, TimeSpan.FromMinutes(SessionDurationMinutes));

                if (contractor.B2BLogin == null)
                {
                    contractor.B2BLogin = loginRequestInfo.DeviceUniqID;
                    contractor.UpdDate = DateTime.Now;
                    var updUser = Guid.TryParse("B5C8A3E3-CF6F-44B3-83BF-68ACA010B473", out var guid);
                    contractor.UpdApplicationUser_ID = guid;
                }
                contractor.LastLoginDt = DateTime.Now;
                await _context.SaveChangesAsync();

                var sessionInfo = new SessionInfo
                {
                    SessionID = sessionId,
                    DeviceUniqID = loginRequestInfo.DeviceUniqID,//npneva@mail.ru
                    RequesterUID = loginRequestInfo.Login //04104409 = RequesterUID
                };
                return new ApiResponse<SessionInfo> { ResponseStatus = 0, Obj = sessionInfo };
            }
            else
                return new ApiResponse<SessionInfo> {ResponseStatus = 1, Msg = "Message: Доступ запрещен" };
        }
        public async Task<ApiResponse<string>> DeleteSession(SessionInfo sessionInfo)
        {
            if (_cache.TryGetValue(sessionInfo.SessionID, out var userInfoResponse))
            {
                _cache.Remove(sessionInfo.SessionID);
                return new ApiResponse<string> { ResponseStatus = 0 };
            }
            else
                return new ApiResponse<string> { ResponseStatus = 1, Msg = "Message: Доступ запрещен" };
        }
        public async Task<bool> CheckClientSession(SessionInfo sessionInfo)
        {
            var sessionId = sessionInfo.SessionID;
            if (_cache.TryGetValue(sessionId, out var cachedValue))
            {
                var cachedSession = cachedValue as CachedSession;

                if (cachedSession != null && cachedSession.Expiration > DateTime.UtcNow)
                {
                    return await CheckClientExist(cachedSession.MTCode);
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }
        public async Task<bool> CheckClientExist(string mtCode)
        {
            var result = await _context.ContractorInfo.FirstOrDefaultAsync(c => c.OuterCode == mtCode &&  c.Activity == true);
            return result != null;
        }

        public async Task<ContractorInfo> FindContractorBySession(SessionInfo session)
        {
            var result = await _context.ContractorInfo.FirstOrDefaultAsync(c => c.OuterCode == session.RequesterUID);
            return result;
        }
    }
}
