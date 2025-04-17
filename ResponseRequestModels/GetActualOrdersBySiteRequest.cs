namespace B2BWebService.ResponseRequestModels
{
    public class GetActualOrdersBySiteRequest
    {
        public SessionInfo Session { get; set; }
        /// <summary>
        /// Опциональный. Массив идентификаторов типов заявок (0 – заказы на автомобили, 1 – заказы на автозапчасти, 2 – подтвержденные заявки на сервис, 3 – автомобиль на сервисе (заявки))
        /// </summary>
        public List<int>? RequestTypes { get; set; }
    }
    /// <summary>
    /// Класс, представляющий данные площадки.
    /// </summary>
    public class GetActualOrdersBySiteResponseObj
    {
        /// <summary>
        /// Название площадки.
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// ID площадки из ЦБ.
        /// </summary>
        public int? CBSiteID { get; set; }

        /// <summary>
        /// ID локальной базы.
        /// </summary>
        public int? PointID { get; set; }

        /// <summary>
        /// Список ошибок, если они есть.
        /// </summary>
        public List<string>? ErrorCollection { get; set; }

        /// <summary>
        /// Список заказов на автомобили.
        /// </summary>
        public List<CarSaleOrder_GetActualOrdersBySite>? CarSales { get; set; }

        /// <summary>
        /// Список заказов на автозапчасти.
        /// </summary>
        public List<AutoPartRequest_GetActualOrdersBySite>? AutoPartRequests { get; set; }

        /// <summary>
        /// Список подтверждённых заявок на сервис.
        /// </summary>
        public List<FutureAppointment_GetActualOrdersBySite>? FutureAppointments { get; set; }

        /// <summary>
        /// Список автомобилей на сервисе (заявки).
        /// </summary>
        public List<CarUnderRepair_GetActualOrdersBySite>? CarsUnderRepair { get; set; }
    }

    /// <summary>
    /// Класс, представляющий заказ на автомобиль.
    /// </summary>
    public class CarSaleOrder_GetActualOrdersBySite
    {
        /// <summary>
        /// Срок поставки.
        /// </summary>
        public string CarOrderStatus { get; set; }

        /// <summary>
        /// Список счетов.
        /// </summary>
        public List<CarSaleBill_GetActualOrdersBySite>? CarSaleBills { get; set; }

        /// <summary>
        /// Площадка (устарело).
        /// </summary>
        public string DBName { get; set; }

        /// <summary>
        /// Дата сделки.
        /// </summary>
        public string DocDate { get; set; }

        /// <summary>
        /// Номер сделки.
        /// </summary>
        public string DocNum { get; set; }

        /// <summary>
        /// Марка автомобиля.
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// Модель автомобиля.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Продавец автомобиля.
        /// </summary>
        public string Salesman { get; set; }

        /// <summary>
        /// Статус сделки, код.
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// Статус сделки, текст.
        /// </summary>
        public string StatusText { get; set; }

        /// <summary>
        /// Цвет кузова автомобиля.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Базовая комплектация автомобиля.
        /// </summary>
        public string CarTrim { get; set; }

        /// <summary>
        /// Планируемая дата выдачи автомобиля.
        /// </summary>
        public string GivingOutDT { get; set; }

        /// <summary>
        /// ID площадки (устарело).
        /// </summary>
        public int? LocalSiteID { get; set; }

        /// <summary>
        /// Текст ошибки по данному объекту.
        /// </summary>
        public string ErrorMsg { get; set; }

        /// <summary>
        /// Признак ошибки по данному объекту.
        /// </summary>
        public bool? HasError { get; set; }
    }

    /// <summary>
    /// Класс, представляющий счет на продажу автомобиля.
    /// </summary>
    public class CarSaleBill_GetActualOrdersBySite
    {
        /// <summary>
        /// ID счета из ЦБ.
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// Номер/дата счета.
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Класс, представляющий заказ на автозапчасть.
    /// </summary>
    public class AutoPartRequest_GetActualOrdersBySite
    {
        /// <summary>
        /// Площадка (устарело).
        /// </summary>
        public string DBName { get; set; }

        /// <summary>
        /// ID площадки (устарело).
        /// </summary>
        public int? LocalSiteID { get; set; }

        /// <summary>
        /// Номер заказ-наряда.
        /// </summary>
        public string OutOrderDocNum { get; set; }
        /// <summary>
        /// Дата заказ-наряда.
        /// </summary>
        public DateTime? OutOrderDate { get; set; }
        /// <summary>
        /// Наименование запчасти.
        /// </summary>
        public string DetName { get; set; }

        /// <summary>
        /// Стоимость запчасти со скидкой.
        /// </summary>
        public decimal? DetCostWithDiscount { get; set; }

        /// <summary>
        /// Стоимость запчасти без скидки.
        /// </summary>
        public decimal? DetCostWithoutDiscount { get; set; }

        /// <summary>
        /// Сумма заказа на запчасть.
        /// </summary>
        public decimal? DetSum { get; set; }

        /// <summary>
        /// Код запчасти.
        /// </summary>
        public string DetSparePartCode { get; set; }

        /// <summary>
        /// Единицы измерения запчасти.
        /// </summary>
        public string DetMeasUnit { get; set; }

        /// <summary>
        /// Идентификатор статуса заказ-наряда.
        /// </summary>
        public int? DetStatus { get; set; }

        /// <summary>
        /// Название статуса заказ-наряда.
        /// </summary>
        public string DetStatusName { get; set; }

        /// <summary>
        /// Идентификатор статуса нахождения запчасти.
        /// </summary>
        public int? DetSparePartStatus { get; set; }

        /// <summary>
        /// Название статуса нахождения запчастей.
        /// </summary>
        public string DetSparePartStatusName { get; set; }

        /// <summary>
        /// Количество запчастей.
        /// </summary>
        public decimal? DetQty { get; set; }
    }
    /// <summary>
    /// Класс, представляющий запись на сервис.
    /// </summary>
    public class FutureAppointment_GetActualOrdersBySite
    {
        /// <summary>
        /// Площадка (устарело).
        /// </summary>
        public string DBName { get; set; }

        /// <summary>
        /// ID площадки (устарело).
        /// </summary>
        public int? LocalSiteID { get; set; }

        /// <summary>
        /// Дата визита (устарело).
        /// </summary>
        public string VisitDate { get; set; }

        /// <summary>
        /// Время визита (устарело).
        /// </summary>
        public string VisitTime { get; set; }

        /// <summary>
        /// Дата и время визита.
        /// </summary>
        public string VisitDateAndTime { get; set; }

        /// <summary>
        /// VIN номер автомобиля.
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// Имя консультанта.
        /// </summary>
        public string Svad { get; set; }

        /// <summary>
        /// Идентификатор записи в рамках локальной базы.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Пробег (-1, если не указан).
        /// </summary>
        public int? TotalTrip { get; set; }

        /// <summary>
        /// Автомобиль (текст).
        /// </summary>
        public string CarText { get; set; }

        /// <summary>
        /// Заявка клиента.
        /// </summary>
        public string ClientClaim { get; set; }
    }

    /// <summary>
    /// Класс, представляющий автомобиль на сервисе.
    /// </summary>
    public class CarUnderRepair_GetActualOrdersBySite
    {
        /// <summary>
        /// Площадка (устарело).
        /// </summary>
        public string DBName { get; set; }

        /// <summary>
        /// Дата заказ-наряда.
        /// </summary>
        public string DocDate { get; set; }

        /// <summary>
        /// Номер заказ-наряда.
        /// </summary>
        public string DocNum { get; set; }

        /// <summary>
        /// ID площадки (устарело).
        /// </summary>
        public int? LocalSiteID { get; set; }

        /// <summary>
        /// VIN номер автомобиля.
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// Имя консультанта.
        /// </summary>
        public string Svad { get; set; }

        /// <summary>
        /// Статус прохождения ремонта.
        /// </summary>
        public string RepairStatus { get; set; }

        /// <summary>
        /// Расширенный статус заказ-наряда.
        /// </summary>
        public string ExtendedOOStatus { get; set; }

        /// <summary>
        /// Госномер автомобиля.
        /// </summary>
        public string RegNum { get; set; }

        /// <summary>
        /// UID Клиента.
        /// </summary>
        public string ClientCode { get; set; }

        /// <summary>
        /// Полное имя клиента.
        /// </summary>
        public string ClientPresentation { get; set; }

        /// <summary>
        /// Роль пользователя (10 - владелец, 20 - клиент, 30 - доверенное лицо).
        /// </summary>
        public int? ClientRole { get; set; }

        /// <summary>
        /// Требуется согласование, если true.
        /// </summary>
        public bool? NeedsAgreement { get; set; }
    }
}
