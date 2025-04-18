namespace B2BWebService.Services
{
    public class ConfigSitePoint
    {
        public int PointID { get; set; }
        public string Text { get; set; }
        public List<ConfigSite> Sites { get; set; }
    }

    public class ConfigSite
    {
        public int SiteID { get; set; }
        public string Text { get; set; }
        public Guid Platform_ID { get; set; }
    }
}
