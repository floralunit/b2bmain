using System.ComponentModel.DataAnnotations;

namespace B2BWebService.ResponseRequestModels
{
    public enum SortColumn_GetInvoiceDetails
    {
        TypeOfWork,
        Name,
        Quantity,
        Sum,
        HeaderNum
    }


    public class GetInvoiceDetailsRequest
    {
        public SessionInfo Session { get; set; }
        public string SortColumn { get; set; }

        public int InvoiceID { get; set; }
    }

    /// <summary>
    /// Представляет детали работы.
    /// </summary>
    public class GetInvoicesDetailsResponseObj
    {
        /// <summary>
        /// Получает или задает тип работы.
        /// </summary>
        public string TypeOfWork { get; set; } // Тип работы

        /// <summary>
        /// Получает или задает наименование работы.
        /// </summary>
        public string Name { get; set; } // Наименование

        /// <summary>
        /// Получает или задает количество работ.
        /// </summary>
        public double? Quantity { get; set; } // Количество

        /// <summary>
        /// Получает или задает сумму к оплате.
        /// </summary>
        public double? Sum { get; set; } // Сумма к оплате

        /// <summary>
        /// Получает или задает ID записи деталей.
        /// </summary>
        public int? DetailInfoID { get; set; } // ID записи

        /// <summary>
        /// Получает или задает номер заголовка. Может быть null.
        /// </summary>
        public int? HeaderNum { get; set; } // Номер заголовка (может быть null)

        /// <summary>
        /// Получает или задает ID типа записи.
        /// </summary>
        public int? DetTypeID { get; set; } // ID типа записи
    }
}
