﻿using Infrastructure.Logging.Model;

namespace Services.Logging.RequestResponse.Configuration
{
    /// <summary>
    /// Request-response log modeli
    /// </summary>
    public class RequestResponseLogModel : BaseLogModel
    {
        public string Content { get; set; }
        public long? RequestContentLength { get; set; }
        public string Host { get; set; }
        public string IpAddress { get; set; }
        public string Method { get; set; }
        public string Protocol { get; set; }
        public long? ResponseContentLength { get; set; }
        public string ResponseContentType { get; set; }
        public long ResponseTime { get; set; }
        public int StatusCode { get; set; }
        public string Url { get; set; }
    }
}
