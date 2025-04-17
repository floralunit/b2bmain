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
        public string CenterGuid { get; set; }
        public string ProjectGuid { get; set; }
    }
}
