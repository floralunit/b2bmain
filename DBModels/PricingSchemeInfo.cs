namespace B2BWebService.DBModels;

public class PricingSchemeInfo
{
    public Guid PricingScheme_ID { get; set; }
    public Guid Contractor_ID { get; set; }
    public string DogovorNum { get; set; }
    public string PricingSchemeName { get; set; }
}
