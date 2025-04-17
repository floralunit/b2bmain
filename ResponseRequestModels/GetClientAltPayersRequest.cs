namespace B2BWebService.ResponseRequestModels;

public class GetClientAltPayersRequest
{
    public SessionInfo Session { get; set; }
    public PointsDictionary? Points { get; set; }
}
public class PointsDictionary: Dictionary<string, string>
{
}

public class GetClientAltPayersResponseObj
{
    public PointSiteStringDto? Point { get; set; }
    public string Error { get; set; }
    public bool? HasError { get; set; }
    public List<ClientAltPayerModel>? ClientAltPayers { get; set; }

}
public class ClientAltPayerModel
{
    public string UID { get; set; }
    public string Presentation { get; set; }
}

