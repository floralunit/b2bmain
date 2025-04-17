namespace B2BWebService.DBModels;

public class ContractorInfo
{
    public Guid ContractorInfo_ID { get; set; }
    public Guid Contractor_ID { get; set; }
    /// <summary>
    /// Код МТ
    /// </summary>
    public string OuterCode { get; set; }
    public DateTime UpdDate { get; set; }
    public Guid UpdApplicationUser_ID { get; set; }
    /// <summary>
    /// SHA1-hash пароля
    /// </summary>
    public string? PwdHash { get; set; }

    /// <summary>
    /// Уникальный идентификатор устройства (логин)
    /// </summary>
    public string? B2BLogin { get; set; }
    public string? ExtIdWeb { get; set; }
    public bool Activity { get; set; }
    public DateTime? LastLoginDt { get; set; }
}
