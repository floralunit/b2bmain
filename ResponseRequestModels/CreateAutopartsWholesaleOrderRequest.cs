namespace B2BWebService.ResponseRequestModels;

/// <summary>
/// Модель, представляющая запрос на создание заказ-нарядов.
/// </summary>
public class CreateAutopartsWholesaleOrderRequest
{
    /// <summary>
    /// Объект-идентификатор сессии.
    /// </summary>
    public SessionInfo Session { get; set; }

    /// <summary>
    /// Объект-идентификатор площадки для создания заказ-нарядов.
    /// </summary>
    public PointSiteDto Point { get; set; }

    /// <summary>
    /// Опциональный параметр, указывающий на необходимость резервирования запчастей.
    /// Если true, создается открытый заказ-наряд.
    /// </summary>
    public bool? ReservePart { get; set; }

    /// <summary>
    /// Массив объектов для заказа.
    /// </summary>
    public List<Order> Orders { get; set; }
}

/// <summary>
/// Модель, представляющая заказ.
/// </summary>
public class Order
{
    /// <summary>
    /// Идентификатор условия договора.
    /// </summary>
    public object ContractDetID { get; set; }

    /// <summary>
    /// Опционально. Комментарий к заказу.
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// Опционально. Код альтернативного плательщика.
    /// </summary>
    public string? ClientAltPayerDescr { get; set; }

    /// <summary>
    /// Массив запчастей для добавления в заказ.
    /// </summary>
    public List<PartCreateAutoParts> Parts { get; set; }
}


/// <summary>
/// Модель, представляющая запчасть.
/// </summary>
public class PartCreateAutoParts
{
    /// <summary>
    /// Код запчасти. Требуется точное совпадение с возвращенным при поиске.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Количество запчастей.
    /// </summary>
    public int Qty { get; set; }
}

/// <summary>
/// Модель, представляющая ответ на создание заказ-нарядов.
/// </summary>
public class CreateAutopartsWholesaleOrderResponseObj
{
    /// <summary>
    /// Номер созданного сервисного прохода.
    /// </summary>
    public string JointOutOrderNumber { get; set; }

    /// <summary>
    /// Массив созданных заказ-нарядов.
    /// </summary>
    public List<OutOrder> OutOrders { get; set; }
}

/// <summary>
/// Модель, представляющая созданный заказ-наряд.
/// </summary>
public class OutOrder
{
    /// <summary>
    /// Идентификатор условия договора.
    /// </summary>
    public string ContractDetID { get; set; }

    /// <summary>
    /// Номер созданного заказ-наряда.
    /// </summary>
    public string OutOrderNumber { get; set; }
}
