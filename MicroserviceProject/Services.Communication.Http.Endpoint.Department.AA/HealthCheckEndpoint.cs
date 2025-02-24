﻿using Infrastructure.Communication.Http.Constants;
using Infrastructure.Communication.Http.Endpoint.Abstract;
using Infrastructure.Communication.Http.Endpoint.Constants;
using Infrastructure.Communication.Http.Models;

using System.Net;

namespace Services.Communication.Http.Endpoint.Department.AA
{
    public class HealthCheckEndpoint : IEndpoint
    {
        public static string Path => "aa.health";
        public string Url { get; set; } = "/health";
        public string Name { get; set; } = Path;
        public object? Payload { get; set; }
        public HttpAction HttpAction { get; set; } = HttpAction.GET;
        public List<HttpHeaderModel> Headers { get; set; } = new List<HttpHeaderModel>();
        public List<HttpQueryModel> Queries { get; set; } = new List<HttpQueryModel>();
        public AuthenticationType AuthenticationType { get; set; } = AuthenticationType.Anonymouse;
        public EndpointPurpose EndpointPurpose { get; set; } = EndpointPurpose.HealthCheck;
        public List<HttpStatusCode> StatusCodes { get; set; } = new List<HttpStatusCode>()
        {
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest
        };
    }
}
