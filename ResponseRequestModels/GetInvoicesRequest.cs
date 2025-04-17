using System.ComponentModel.DataAnnotations;

namespace B2BWebService.ResponseRequestModels
{
    public enum SortColumn_GetInvoices
    {
        DocDate,
        DocNum,
        Amount
    }

    public class GetInvoicesRequest
    {
        /// <summary>
        /// Объект-идентификатор сессии.
        /// </summary>
        public SessionInfo Session { get; set; }

        /// <summary>
        /// Имя поля для выполнения сортировки (возможные значения: DocDate, DocNum, Amount).
        /// </summary>
        public string SortColumn { get; set; }

        /// <summary>
        /// Указывает, нужно ли возвращать детальную информацию для данной записи.
        /// </summary>
        public bool? IncludeDetails { get; set; } = false;
    }

    /// <summary>
    /// Модель, представляющая ответ с массивом счетов.
    /// </summary>
    public class GetInvoicesResponseObj
    {
        /// <summary>
        /// Содержит массив объектов счетов.
        /// </summary>
        public List<InvoiceDto> Invoices { get; set; }

        /// <summary>
        /// Общая сумма.
        /// </summary>
        public double? TotalAmount { get; set; }
    }

    /// <summary>
    /// Модель, представляющая объект счета.
    /// </summary>
    public class InvoiceDto
    {
        /// <summary>
        /// ID записи.
        /// </summary>
        public int? InvoiceID { get; set; }

        /// <summary>
        /// Номер документа.
        /// </summary>
        public string DocNum { get; set; }

        /// <summary>
        /// Дата документа.
        /// </summary>
        public string DocDate { get; set; }

        /// <summary>
        /// Номер заказ-наряда.
        /// </summary>
        public string OrderNum { get; set; }

        /// <summary>
        /// VIN-номер.
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// Госномер.
        /// </summary>
        public string RegNum { get; set; }

        /// <summary>
        /// Сумма оплаты.
        /// </summary>
        public double? Amount { get; set; }

        /// <summary>
        /// ФИО мастера-консультанта.
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Список позиций в счёте. Будет возвращен массив, если в запросе был выставлен флаг IncludeDetails.
        /// </summary>
        public List<GetInvoicesDetailsResponseObj>? Details { get; set; }
    }

}
