namespace B2BWebService.DBModels
{
    public class ContractorCommunication
    {
        public Guid ContractorCommunication_ID { get; set; }
        public Guid Contractor_ID { get; set; }
        public Guid CommunicationSubType_ID { get; set; }
        public string Code { get; set; }
        public string Number { get; set; }
        public string StateCode { get; set; }
    }
}
