namespace B2BWebService.ResponseRequestModels
{
    /// <summary>
    /// Модель для представления информации о площадке и связанных с ней данных.
    /// </summary>
    public class GetActualOrdersResponseObj
    {
        /// <summary>
        /// Название площадки.
        /// </summary>
        public string PointName { get; set; }

        /// <summary>
        /// Список ошибок, если они есть.
        /// </summary>
        public List<string>? ErrorCollection { get; set; }

        /// <summary>
        /// Список заказов на автомобили.
        /// </summary>
        public List<CarSaleDto_GetActualOrders>? CarSales { get; set; }

        /// <summary>
        /// Список заказов на автозапчасти.
        /// </summary>
        public List<AutoPartRequestDto_GetActualOrders>? AutoPartRequests { get; set; }

        /// <summary>
        /// Список подтверждённых заявок на сервис.
        /// </summary>
        public List<FutureAppointmentDto_GetActualOrders>? FutureAppointments { get; set; }

        /// <summary>
        /// Список автомобилей на сервисе (заявки).
        /// </summary>
        public List<CarUnderRepairDto_GetActualOrders>? CarsUnderRepair { get; set; }
    }

    /// <summary>
    /// Модель для представления информации о заказе на автомобиль.
    /// </summary>
    public class CarSaleDto_GetActualOrders
    {
        /// <summary>
        /// Срок поставки.
        /// </summary>
        public string CarOrderStatus { get; set; }

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
        /// Продавец.
        /// </summary>
        public string Salesman { get; set; }

        /// <summary>
        /// Статус сделки.
        /// </summary>
        public string StatusText { get; set; }

        /// <summary>
        /// Цвет кузова.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Базовая комплектация.
        /// </summary>
        public string CarTrim { get; set; }

        /// <summary>
        /// Планируемая дата выдачи.
        /// </summary>
        public string GivingOutDT { get; set; }
    }

    /// <summary>
    /// Модель для представления информации о заказе на автозапчасти.
    /// </summary>
    public class AutoPartRequestDto_GetActualOrders
    {
        /// <summary>
        /// Площадка (устарело).
        /// </summary>
        public string DBName { get; set; }

        /// <summary>
        /// № заказ-наряда.
        /// </summary>
        public string OutOrderDocNum { get; set; }

        /// <summary>
        /// Наименование запчасти.
        /// </summary>
        public string DetName { get; set; }

        /// <summary>
        /// Стоимость со скидкой.
        /// </summary>
        public double? DetCostWithDiscount { get; set; }

        /// <summary>
        /// Стоимость без скидки.
        /// </summary>
        public double? DetCostWithoutDiscount { get; set; }

        /// <summary>
        /// Сумма заказа.
        /// </summary>
        public double? DetSum { get; set; }

        /// <summary>
        /// Код запчасти.
        /// </summary>
        public string DetSparePartCode { get; set; }

        /// <summary>
        /// Единицы измерения.
        /// </summary>
        public string DetMeasUnit { get; set; }

        /// <summary>
        /// Идентификатор статуса ЗН.
        /// </summary>
        public int? DetStatus { get; set; }

        /// <summary>
        /// Название статуса ЗН.
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
        public double? DetQty { get; set; }
    }

    /// <summary>
    /// Модель для представления информации о записи на сервис (подтверждённые заявки).
    /// </summary>
    public class FutureAppointmentDto_GetActualOrders
    {
        /// <summary>
        /// Площадка (устарело).
        /// </summary>
        public string DBName { get; set; }

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
    /// Модель для представления информации об автомобиле на сервисе (заявки).
    /// </summary>
    public class CarUnderRepairDto_GetActualOrders
    {
        /// <summary>
        /// Площадка (устарело).
        /// </summary>
        public string DBName { get; set; }

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
    }

}
