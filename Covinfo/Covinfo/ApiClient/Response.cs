namespace Covinfo.ApiClient
{
    class Response
    {
        public ResponseData confirmed { get; set; }
        public ResponseData recovered { get; set; }
        public ResponseData deaths { get; set; }
    }

    class ResponseData
    {
        public string value { get; set; }
    }
}
