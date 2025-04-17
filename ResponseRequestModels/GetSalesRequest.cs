namespace B2BWebService.ResponseRequestModels
{

    /// <summary>
    /// Модель, представляющая итоговую сумму по всем сделкам.
    /// </summary>
    public class GetSalesResponseObj
    {
        /// <summary>
        /// Итоговая сумма по всем сделкам.
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// Список закрытых сделок.
        /// </summary>
        public List<SaleDto> Sales { get; set; }
    }

    /// <summary>
    /// Модель, представляющая закрытую сделку.
    /// </summary>
    public class SaleDto
    {
        /// <summary>
        /// ID сделки.
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// Производитель.
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// Модель.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Дата продажи.
        /// </summary>
        public string SaleDate { get; set; }

        /// <summary>
        /// Сумма.
        /// </summary>
        public double? Sum { get; set; }
    }
}
