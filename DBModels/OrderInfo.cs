namespace B2BWebService.DBModels;

public class OrderInfo
{
    public Guid? DocumentSpecification_ID { get; set; }
    public Guid DocumentRowBase_ID { get; set; }
    public Guid? GoodsAll_ID { get; set; }
    public Guid? Contractor_ID { get; set; }
    public Guid? Center_ID { get; set; }
    public string? OutOrderDocNum { get; set; }
    public string? DetSparePartCode { get; set; }
    public string? DetName { get; set; }
    public decimal? Quant { get; set; }
    public decimal? Summa { get; set; }
    public decimal? SummaWithoutDiscount { get; set; }
    public string? DetMeasUnit { get; set; }
    public string? CenterName { get; set; }
    public string? DetStatusName { get; set; }
    public int? DetStatus { get; set; }
}
