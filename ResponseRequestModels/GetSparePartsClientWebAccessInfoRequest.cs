namespace B2BWebService.ResponseRequestModels;

public class GetSparePartsClientWebAccessInfoRequest
{
    /// <summary>
    /// Код клиента.
    /// </summary>
    public string Descr { get; set; }

    /// <summary>
    /// Внешний идентификатор для клиента, используется только для ЦБ с модулем MTCliAvtodomCB.
    /// </summary>
    public string ExtIdWeb { get; set; }

    /// <summary>
    /// Информация о приложении.
    /// </summary>
    public AppInfo AppInfo { get; set; }
}

/// <summary>
/// Класс, представляющий информацию о пользователе.
/// </summary>
public class GetSparePartsClientWebAccessInfoResponseObj
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Хеш пароля пользователя.
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    /// Статус выполнения операции. 
    /// 0 - произошла лишь передача текущих данных, 
    /// 1 - был создан новый пароль, 
    /// 2 - был создан новый клиент с паролем.
    /// </summary>
    public int? WebAccessStatus { get; set; }
}