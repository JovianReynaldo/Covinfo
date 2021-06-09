namespace Covinfo.ApiClient
{
    class Response
    {
        public ResponseData Confirmed { get; set; }
        public ResponseData Recovered { get; set; }
        public ResponseData Deaths { get; set; }
    }

    class ResponseData
    {
        public string Value { get; set; }
    }
}
