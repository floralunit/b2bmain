namespace B2BWebService.ResponseRequestModels;
public class GetContractDetsRequest
{
    public SessionInfo Session { get; set; }
    public List<PointSiteStringDto>? Points { get; set; }
}
/// <summary>
/// Представляет запрос на получение условий договора для определенной площадки.
/// </summary>
public class GetContractDetsResponseObj
{
    /// <summary>
    /// идентификатор площадки.
    /// </summary>
    public PointSiteDto Point { get; set; }

    /// <summary>
    /// массив объектов условий договора.
    /// </summary>
    public List<ContractDetail> ContractDets { get; set; }
}

/// <summary>
/// Представляет детали условия договора.
/// </summary>
public class ContractDetail
{
    /// <summary>
    /// идентификатор условия договора (уникальный только в рамках Point).
    /// </summary>
    public string ContractDetID { get; set; }

    /// <summary>
    /// название условия договора.
    /// </summary>
    public string ContractDetName { get; set; }

    /// <summary>
    /// номер договора.
    /// </summary>
    public string ContractName { get; set; }

    /// <summary>
    /// значение, указывающее, является ли условие договора применимым для заказа из центрального склада.
    /// </summary>
    public bool ContractDetIsForOrder { get; set; }

    /// <summary>
    /// тип заказа с центрального склада (Rush, Stock, VOR).
    /// </summary>
    public string ContractDetOrderyType { get; set; }
}

