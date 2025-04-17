namespace B2BWebService.ResponseRequestModels
{
    public class GetCarListRequest
    {
        public SessionInfo Session { get; set; }
        public bool ShowTrusted { get; set; } = false;
    }
    /// <summary>
    /// Представляет информацию об автомобиле.
    /// </summary>
    public class GetCarListResponseObj
    {
        /// <summary>
        /// ID автомобиля.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// описание автомобиля (марка, модель, госномер).
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// VIN номер автомобиля.
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// название марки автомобиля.
        /// </summary>
        public string MakeText { get; set; }

        /// <summary>
        /// государственный регистрационный номер автомобиля.
        /// </summary>
        public string RegNum { get; set; }

        /// <summary>
        /// название модели автомобиля.
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// роль пользователя (10 - владелец, 20 - клиент, 30 - доверенное лицо).
        /// </summary>
        public int ClientRole { get; set; }

        /// <summary>
        /// пробег автомобиля.
        /// </summary>
        public double Mileage { get; set; }
    }

    /// <summary>
    /// Представляет информацию о марке автомобиля.
    /// </summary>
    public class GetCarMakesResponseObj
    {
        /// <summary>
        /// ID марки автомобиля.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// название марки (внутреннее).
        /// </summary>
        public string MakeText { get; set; }

        /// <summary>
        /// название марки для отображения на веб-сайтах.
        /// </summary>
        public string MakeTextForWeb { get; set; }

        /// <summary>
        /// название марки для веб-сайта, если имеется. Иначе - внутреннее название.
        /// </summary>
        public string ProcessedMakeText { get; set; }
    }

    /// <summary>
    /// Представляет информацию о гарантии и связанных акциях.
    /// </summary>
    public class GetWarrantyActionsResponseObj
    {
        /// <summary>
        /// идентификатор локальной базы.
        /// </summary>
        public int PointID { get; set; }

        /// <summary>
        /// массив гарантийных акций.
        /// </summary>
        public List<WarrantyActionDetail> WarrantyActions { get; set; }
    }

    /// <summary>
    /// Представляет детали гарантийной акции.
    /// </summary>
    public class WarrantyActionDetail
    {
        /// <summary>
        /// код гарантийной акции в локальной базе (внутренний).
        /// </summary>
        public string Descr { get; set; }

        /// <summary>
        /// код гарантийной акции.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// название гарантийной акции.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// степень гарантийной акции:
        /// 1 - отзыв, 2 - при визите, 3 - по просьбе клиента.
        /// </summary>
        public int Degree { get; set; }

        /// <summary>
        /// текст заголовка гарантийной акции.
        /// </summary>
        public string OrderHeaderText { get; set; }
    }


    public class GetWarrantyActionsRequest
    {
        /// <summary>
        /// VIN номер автомобиля.
        /// </summary>
        public string VIN { get; set; }

        /// <summary>
        /// необязательное поле. Фильтр по локальным базам. 
        /// Если отсутствует - возвращаются гарантийные акции из всех баз.
        /// </summary>
        public List<int> PointFilter { get; set; }
    }


}
