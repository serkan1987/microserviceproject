﻿using Infrastructure.Communication.Http.Constants;
using Infrastructure.Communication.Http.Endpoint.Abstract;
using Infrastructure.Communication.Http.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Communication.Http.Endpoints.Api.Business.Departments.HR
{
    public class CreateWorkerEndpoint : IEndpoint
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public object Payload { get; set; }
        public HttpAction HttpAction { get; set; }
        public List<HttpHeader> Headers { get; set; }
        public List<HttpQuery> Queries { get; set; }
        public IEndpointAuthentication EndpointAuthentication { get; set; }
    }
}
