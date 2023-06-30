﻿namespace Infrastructure.Communication.Http.Broker.Mock
{
    public class HttpPostCallerFactory
    {
        public static HttpPostCaller Instance
        {
            get
            {
                return new HttpPostCaller();
            }
        }
    }
}
