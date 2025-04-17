namespace B2BWebService.ResponseRequestModels;

public class GetUserInfoPrivateDataResponseObj
{
    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// Домашний телефон.
    /// </summary>
    public string HomePhone { get; set; }

    /// <summary>
    /// Рабочий телефон.
    /// </summary>
    public string BusinessPhone { get; set; }

    /// <summary>
    /// Номер водительского удостоверения.
    /// </summary>
    public string DriverLicenseNumber { get; set; }

    /// <summary>
    /// Номер паспорта.
    /// </summary>
    public string PassportNumber { get; set; }

    /// <summary>
    /// Серия паспорта.
    /// </summary>
    public string PassportSeries { get; set; }

    /// <summary>
    /// Орган, выдавший паспорт.
    /// </summary>
    public string PassportOrgan { get; set; }

    /// <summary>
    /// Дата выдачи паспорта.
    /// </summary>
    public DateTime? PassportDate { get; set; }

    /// <summary>
    /// Фактический адрес проживания.
    /// </summary>
    public string FactAddress { get; set; }

    /// <summary>
    /// Адрес прописки.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string SecondName { get; set; }

    /// <summary>
    /// Имя и отчество.
    /// </summary>
    public string FirstMiddleName { get; set; }

    // Фактический адрес
    /// <summary>
    /// Дом по фактическому адресу.
    /// </summary>
    public string AddressFactHouse { get; set; }

    /// <summary>
    /// Код КЛАДР по фактическому адресу.
    /// </summary>
    public string AddressFactKLADR { get; set; }

    /// <summary>
    /// Почтовый индекс по фактическому адресу.
    /// </summary>
    public string AddressFactZipcode { get; set; }

    /// <summary>
    /// Квартира по фактическому адресу.
    /// </summary>
    public string AddressFactApartment { get; set; }

    /// <summary>
    /// Строение 1 по фактическому адресу.
    /// </summary>
    public string AddressFactBuild1 { get; set; }

    /// <summary>
    /// Город по фактическому адресу.
    /// </summary>
    public string AddressFactTown { get; set; }

    /// <summary>
    /// Район по фактическому адресу.
    /// </summary>
    public string AddressFactArea { get; set; }

    /// <summary>
    /// Офис по фактическому адресу.
    /// </summary>
    public string AddressFactOffice { get; set; }

    /// <summary>
    /// Округ по фактическому адресу.
    /// </summary>
    public string AddressFactDistrict { get; set; }

    /// <summary>
    /// Страна по фактическому адресу.
    /// </summary>
    public string AddressFactCountry { get; set; }

    /// <summary>
    /// Строение 2 по фактическому адресу.
    /// </summary>
    public string AddressFactBuild2 { get; set; }

    /// <summary>
    /// Улица по фактическому адресу.
    /// </summary>
    public string AddressFactStreet { get; set; }

    // Адрес регистрации
    /// <summary>
    /// Дом по юридическому адресу.
    /// </summary>
    public string AddressLegalHouse { get; set; }

    /// <summary>
    /// Код КЛАДР по юридическому адресу.
    /// </summary>
    public string AddressLegalKLADR { get; set; }

    /// <summary>
    /// Почтовый индекс по юридическому адресу.
    /// </summary>
    public string AddressLegalZipcode { get; set; }

    /// <summary>
    /// Квартира по юридическому адресу.
    /// </summary>
    public string AddressLegalApartment { get; set; }

    /// <summary>
    /// Строение 1 по юридическому адресу.
    /// </summary>
    public string AddressLegalBuild1 { get; set; }

    /// <summary>
    /// Город по юридическому адресу.
    /// </summary>
    public string AddressLegalTown { get; set; }

    /// <summary>
    /// Район по юридическому адресу.
    /// </summary>
    public string AddressLegalArea { get; set; }

    /// <summary>
    /// Офис по юридическому адресу.
    /// </summary>
    public string AddressLegalOffice { get; set; }

    /// <summary>
    /// Округ по юридическому адресу.
    /// </summary>
    public string AddressLegalDistrict { get; set; }

    /// <summary>
    /// Страна по юридическому адресу.
    /// </summary>
    public string AddressLegalCountry { get; set; }

    /// <summary>
    /// Строение 2 по юридическому адресу.
    /// </summary>
    public string AddressLegalBuild2 { get; set; }

    /// <summary>
    /// Улица по юридическому адресу.
    /// </summary>
    public string AddressLegalStreet { get; set; }
}
