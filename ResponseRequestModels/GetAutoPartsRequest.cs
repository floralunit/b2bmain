namespace B2BWebService.ResponseRequestModels;

/// <summary>
/// Представляет запрос на проверку запчастей.
/// </summary>
public class GetAutoPartsRequest
{
    public SessionInfo Session { get; set; }

    /// <summary>
    /// Массив идентификаторов площадок для поиска запчастей.
    /// Если пустой, будет произведен опрос всех локальных баз и площадок.
    /// </summary>
    public List<PointSiteDto>? Points { get; set; }

    /// <summary>
    /// Массив запчастей, которые требуется проверить.
    /// </summary>
    public List<AutoPart> AutoParts { get; set; }
}

/// <summary>
/// Представляет информацию о запчасти.
/// </summary>
public class AutoPart
{
    /// <summary>
    /// Код запчасти.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Требуемое количество запчасти.
    /// </summary>
    public double? Deal { get; set; }
}

/// <summary>
/// Представляет ответ на запрос проверки запчастей.
/// </summary>
public class GetAutoPartsResponseObj
{
    /// <summary>
    /// ID площадки.
    /// </summary>
    public PointRequest Point { get; set; }

    /// <summary>
    /// Значение, указывающее, была ли пропущена площадка при поиске.
    /// </summary>
    public bool IsSkipped { get; set; }

    /// <summary>
    /// Сообщение о причине пропуска площадки.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Массив проверенных запчастей.
    /// </summary>
    public List<CheckedAutoPart> AutoParts { get; set; }

    /// <summary>
    /// ID базы, откуда получен текущий результат.
    /// </summary>
    public int? PointID { get; set; }

    /// <summary>
    /// ID площадки, откуда получен текущий результат.
    /// </summary>
    public int? SiteID { get; set; }
}

/// <summary>
/// Представляет информацию о проверенной запчасти.
/// </summary>
public class CheckedAutoPart
{
    /// <summary>
    /// Наименование запчасти.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Значение, указывающее, была ли пропущена информация о детали.
    /// </summary>
    public bool? IsSkipped { get; set; }

    /// <summary>
    /// Значение, указывающее, есть ли запрашиваемое количество в наличии.
    /// </summary>
    public bool? IsAvailable { get; set; }

    /// <summary>
    /// Значение, указывающее, была ли деталь найдена.
    /// </summary>
    public bool? IsFound { get; set; }

    /// <summary>
    /// Значение, указывающее, является ли данная деталь заменой запрашиваемой.
    /// </summary>
    public bool? IsReplacement { get; set; }

    /// <summary>
    /// Причина выставления IsSkipped=true.
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// Доступное количество запчасти.
    /// </summary>
    public decimal? AvailableQuantity { get; set; }

    /// <summary>
    /// Цена за единицу запчасти.
    /// </summary>
    public decimal? PricePerUnit { get; set; }

    /// <summary>
    /// Цена за единицу с учетом скидки.
    /// </summary>
    public decimal? PricePerUnitWithDisc { get; set; }

    /// <summary>
    /// Значение, указывающее, доступна ли запчасть для доставки.
    /// </summary>
    public bool? IsAvaibleForDelivery { get; set; }
}
