namespace B2BWebService.ResponseRequestModels;

/// <summary>
/// Класс, представляющий информацию о приложении.
/// </summary>
public class AppInfo
{
    /// <summary>
    /// Идентификатор приложения, текстовое поле.
    /// </summary>
    public string AppID { get; set; }

    /// <summary>
    /// MD5 хеш токена с солью.
    /// </summary>
    public string TokenHashWS { get; set; }
}
