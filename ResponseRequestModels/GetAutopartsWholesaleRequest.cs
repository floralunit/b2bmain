namespace B2BWebService.ResponseRequestModels;

public class GetAutopartsWholesaleRequest
{
    public SessionInfo Session { get; set; }
    /// <summary>
    /// Массив идентификаторов площадок для поиска запчастей.
    /// Если пустой, будет произведен опрос всех локальных баз и площадок.
    /// </summary>
    public List<PointSiteDto>? Points { get; set; }
    public List<string> PartCodes { get; set; }
    public bool? BOnlyAvailable { get; set; }
    public bool? BAnalog { get; set; }
}

public class GetAutopartsWholesaleResponseObj
{
    /// <summary>
    /// код искомой запчасти.
    /// </summary>
    public string RequestPartCode { get; set; }

    /// <summary>
    /// массив результатов поиска по площадкам.
    /// </summary>
    public List<SiteResult> ResultsBySite { get; set; }
}
/// <summary>
/// Представляет результат поиска запчасти по определенной площадке.
/// </summary>
public class SiteResult
{
    /// <summary>
    /// информация о площадке.
    /// </summary>
    public PointSiteDto Point { get; set; }

    /// <summary>
    /// массив объектов условий договора для данной площадки.
    /// </summary>
    public List<ContractDetsDetail> ContractDets { get; set; }
}
/// <summary>
/// Представляет детали условия договора.
/// </summary>
public class ContractDetsDetail
{
    /// <summary>
    /// Уникальный идентификатор условия договора.
    /// </summary>
    public string? ID { get; set; }

    /// <summary>
    /// Название условия договора.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Номер договора.
    /// </summary>
    public string ContractNumber { get; set; }

    /// <summary>
    /// Значение, указывающее, является ли условие договора применимым для заказа.
    /// </summary>
    public bool? ContractDetIsForOrder { get; set; }

    /// <summary>
    /// Массив найденных запчастей.
    /// </summary>
    public List<Part> Parts { get; set; }
}
/// <summary>
/// Представляет информацию о найденной запчасти.
/// </summary>
public class Part
{
    /// <summary>
    /// Код найденной запчасти.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Название найденной запчасти.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Код, по которому была найдена запчасть.
    /// </summary>
    public string FoundByCode { get; set; }

    /// <summary>
    /// Стоимость запчасти за единицу.
    /// </summary>
    public decimal? PricePerUnit { get; set; }

    /// <summary>
    /// Значение, указывающее, является ли найденная запчасть заменой.
    /// </summary>
    public bool? IsReplacement { get; set; }

    /// <summary>
    /// Значение, указывающее, является ли запчасть аналогом.
    /// </summary>
    public bool? IsAnalog { get; set; }

    /// <summary>
    /// Уровень в цепочке замен.
    /// </summary>
    public int? ChainLvl { get; set; }

    /// <summary>
    /// Значение, указывающее, является ли эта запчасть последней в цепочке замен.
    /// </summary>
    public bool? IsLastInChain { get; set; }

    /// <summary>
    /// Доступное количество запчасти на складе площадки.
    /// </summary>
    public decimal? AvailableQuantity { get; set; }

    /// <summary>
    /// Количество на складе импортера. Может быть null или строка с данными.
    /// </summary>
    public string CenterStockQty { get; set; } // Nullable строка.

    /// <summary>
    /// Дата актуальности данных о количестве на складе импортера.
    /// Значение "0001-01-01T00:00:00" указывает на отсутствие данных.
    /// </summary>
    public DateTime? CenterStockQtyDate { get; set; }

    /// <summary>
    /// Рекомендованная цена продажи из справочника запчастей с НДС.
    /// </summary>
    public decimal? StdSalePrice { get; set; }
}
