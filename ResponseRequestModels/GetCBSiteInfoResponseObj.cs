namespace B2BWebService.ResponseRequestModels
{
    /// <summary>
    /// Представляет информацию о площадке для обслуживания автомобилей.
    /// </summary>
    public class GetCBSiteInfoResponseObj
    {
        /// <summary>
        /// ID запрашиваемой площадки.
        /// </summary>
        public int? ID { get; set; }

        /// <summary>
        /// название площадки.
        /// </summary>
        public string Descr { get; set; }

        /// <summary>
        /// GPS координата долготы.
        /// </summary>
        public double? GPSLongitude { get; set; }

        /// <summary>
        /// GPS координата широты.
        /// </summary>
        public double? GPSLatitude { get; set; }

        /// <summary>
        /// адрес площадки.
        /// </summary>
        public string Address { get; set; }
    }

}
