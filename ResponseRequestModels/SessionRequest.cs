using System.ComponentModel.DataAnnotations;

namespace B2BWebService.ResponseRequestModels
{
    public class SessionRequest
    {
        [Required]
        public SessionInfo Session { get; set; }
    }
    public class SessionInfo
    {
        [Required]
        public string SessionID { get; set; }
        [Required]
        public string RequesterUID { get; set; }
        [Required]
        public string DeviceUniqID { get; set; }
    }
}
