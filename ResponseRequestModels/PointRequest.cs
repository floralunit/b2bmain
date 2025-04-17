namespace B2BWebService.ResponseRequestModels
{
    public class PointRequestWithSession
    {
        public SessionInfo? Session { get; set; }
        public int PointID { get; set; }
    }
    public class PointRequest
    {
        public int PointID { get; set; }
    }
    public class PointSiteDto
    {
        public int PointID { get; set; }
        public int SiteID { get; set; }
    }
    public class PointSiteStringDto
    {
        public string PointID { get; set; }
        public string SiteID { get; set; }
    }
}
