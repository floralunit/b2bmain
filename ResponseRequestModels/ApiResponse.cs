using Newtonsoft.Json;

namespace B2BWebService.ResponseRequestModels
{
    public class ApiResponse<T>
    {
        public int? ResponseStatus { get; set; }
        public string Msg { get; set; }
        public T Obj { get; set; }
        public string Url { get; set; }
        public string Stack { get; set; }
    }

}
