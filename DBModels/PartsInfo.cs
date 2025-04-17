namespace B2BWebService.DBModels
{
    public class PartsInfo
    {
        public Guid? RequestPart_ID { get; set; }
        public string? RequestPartCode { get; set; }
        public Guid? Parts_ID { get; set; }
        public string? PartName { get; set; }
        public string? PartCode { get; set; }
        public decimal? Quant { get; set; }
        public bool? Analog { get; set; }
        public bool? Replacement { get; set; }
        public decimal? PriceListPrice { get; set; }
        public decimal? PriceListPriceWithDiscount { get; set; }
        public Guid? Contractor_ID { get; set; }
        public Guid? PricingScheme_ID { get; set; }
        public string? PricingSchemeName { get; set; }
        public string? ContractNumber { get; set; }
        //public string? PriceName { get; set; }
        public Guid? Center_ID { get; set; }
        //public string? CenterName { get; set; }
        public Guid? Department_ID { get; set; }
        //public string? Department{ get; set; }
        //public decimal? MaxPrice { get; set; }
    }
}
