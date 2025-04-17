namespace B2BWebService.ResponseRequestModels
{
    /// <summary>
    /// Представляет информацию о фирме.
    /// </summary>
    public class GetCBFirmsResponseObj
    {
        public List<FirmDto> FirmList { get; set; }
    }

    public class FirmDto
    {
        /// <summary>
        /// ID фирмы.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// название фирмы.
        /// </summary>
        public string Descr { get; set; }

        /// <summary>
        /// контактный телефон фирмы.
        /// </summary>
        public string FirmPhone { get; set; }

        /// <summary>
        /// фактический адрес фирмы.
        /// </summary>
        public string FirmAddress { get; set; }
    }

}
