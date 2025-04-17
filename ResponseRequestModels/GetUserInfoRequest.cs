namespace B2BWebService.ResponseRequestModels;

public class GetUserInfoResponseObj
{
    /// <summary>
    /// Полное имя пользователя (Ф.И.О.).
    /// </summary>
    public string UserFullName { get; set; }

    /// <summary>
    /// Контактный телефон клиента.
    /// </summary>
    public ContactPhoneInfo ContactPhone { get; set; }

    /// <summary>
    /// Группка скидок.
    /// </summary>
    public string DiscountGroup { get; set; }

    /// <summary>
    /// ID группы скидок.
    /// </summary>
    public int? DiscountGroupID { get; set; }

    /// <summary>
    /// Статус в программе "Династия".
    /// </summary>
    public string DynastyStatusName { get; set; }

    /// <summary>
    /// Итоговая сумма, потраченная на сервис.
    /// </summary>
    public decimal? InvoicesTotalAmount { get; set; }

    /// <summary>
    /// Сумма покупки авто.
    /// </summary>
    public decimal? SalesTotalAmount { get; set; }

    /// <summary>
    /// Код династии.
    /// </summary>
    public string DynastyCode { get; set; }

    /// <summary>
    /// Бонусные баллы по программе дилера.
    /// </summary>
    public int? BonusPoints { get; set; }

    /// <summary>
    /// Адрес электронной почты.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных SMS сообщений.
    /// </summary>
    public bool? DontSendSms { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных email сообщений.
    /// </summary>
    public bool? DontSendEmail { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных SMS сообщений по сервису.
    /// </summary>
    public bool? SvcNotSendSms { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных email сообщений по сервису.
    /// </summary>
    public bool? SvcNotSendMail { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных push сообщений по сервису.
    /// </summary>
    public bool? SvcNotSendPush { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных SMS сообщений по акциям сервиса.
    /// </summary>
    public bool? AdvSvcNotSendSms { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных email сообщений по акциям сервиса.
    /// </summary>
    public bool? AdvSvcNotSendMail { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных push сообщений по акциям сервиса.
    /// </summary>
    public bool? AdvSvcNotSendPush { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных SMS сообщений по акциям автосалона.
    /// </summary>
    public bool? AdvCarsNotSendSms { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных email сообщений по акциям автосалона.
    /// </summary>
    public bool? AdvCarsNotSendMail { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных push сообщений по акциям автосалона.
    /// </summary>
    public bool? AdvCarsNotSendPush { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных SMS сообщений от импортера.
    /// </summary>
    public bool? ImporterNotSendSms { get; set; }

    /// <summary>
    /// 
    /// Признак того, что пользователь отказался от информационных email сообщений от импортера.
    /// </summary>
    public bool? ImporterNotSendMail { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных SMS сообщений по всем типам.
    /// </summary>
    public bool? AnyNotSendSms { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных email сообщений по всем типам.
    /// </summary>
    public bool? AnyNotSendMail { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных SMS сообщений по одному из типов.
    /// </summary>
    public bool? AllNotSendSms { get; set; }

    /// <summary>
    /// Признак того, что пользователь отказался от информационных email сообщений по одному из типов.
    /// </summary>
    public bool? AllNotSendMail { get; set; }

    /// <summary>
    /// Уровень бонусов клиента для участников программы бонусов.
    /// </summary>
    public string ClientBonusLevel { get; set; }

    /// <summary>
    /// Дополнительный логин клиента.
    /// </summary>
    public string CustomLogin { get; set; }

    /// <summary>
    /// Признак наличия у клиента дополнительного логина.
    /// </summary>
    public bool? HasCustomLogin { get; set; }
}

/// <summary>
/// Класс, представляющий контактный телефон клиента.
/// </summary>
public class ContactPhoneInfo
{
    /// <summary>
    /// Номер телефона клиента.
    /// </summary>
    public string PhoneNumber { get; set; }
}



