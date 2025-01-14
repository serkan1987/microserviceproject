﻿using Infrastructure.Communication.Http.Broker;
using Infrastructure.Communication.Http.Constants;
using Infrastructure.Communication.Http.Endpoint.Abstract;
using Infrastructure.Communication.Http.Exceptions;
using Infrastructure.Communication.Http.Models;

using Newtonsoft.Json;

using Services.Communication.Http.Broker.Abstract;

using System.Net;

namespace Services.Communication.Http.Broker
{
    public class AuthenticatedCommunicator : ICommunicator
    {
        private readonly HttpGetCaller _httpGetCaller;
        private readonly HttpPostCaller _httpPostCaller;
        private readonly HttpPutCaller _httpPutCaller;
        private readonly HttpDeleteCaller _httpDeleteCaller;

        public AuthenticatedCommunicator(
            HttpGetCaller httpGetCaller,
            HttpPostCaller httpPostCaller,
            HttpPutCaller httpPutCaller,
            HttpDeleteCaller httpDeleteCaller)
        {
            _httpGetCaller = httpGetCaller;
            _httpPostCaller = httpPostCaller;
            _httpPutCaller = httpPutCaller;
            _httpDeleteCaller = httpDeleteCaller;
        }

        public async Task<ServiceResultModel<TResult>> CallAsync<TResult>(IAuthenticatedEndpoint endpoint, CancellationTokenSource cancellationTokenSource)
        {
            ErrorModel errorModel = new ErrorModel();

            try
            {
                switch (endpoint.HttpAction)
                {
                    case HttpAction.GET:
                        return await _httpGetCaller.CallAsync<ServiceResultModel<TResult>>(endpoint, cancellationTokenSource);
                    case HttpAction.DELETE:
                        return await _httpDeleteCaller.CallAsync<ServiceResultModel<TResult>>(endpoint, cancellationTokenSource);
                    default:
                        throw new UndefinedCallTypeException();
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    if (wex.Response is HttpWebResponse && (wex.Response as HttpWebResponse).StatusCode == HttpStatusCode.Unauthorized)
                    {
                        errorModel.InnerErrors.Add(new ErrorModel()
                        {
                            Code = ((int)HttpStatusCode.Unauthorized).ToString(),
                            Description = wex.Message
                        });

                        return new ServiceResultModel<TResult>()
                        {
                            IsSuccess = false,
                            SourceApiService = endpoint.Name,
                            ErrorModel = errorModel
                        };
                    }
                    else if (wex.Response is HttpWebResponse && (wex.Response as HttpWebResponse).StatusCode == HttpStatusCode.BadRequest)
                    {
                        using (StreamReader streamReader = new StreamReader(wex.Response.GetResponseStream()))
                        {
                            string response = await streamReader.ReadToEndAsync();

                            return JsonConvert.DeserializeObject<ServiceResultModel<TResult>>(response);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorModel.Description = ex.ToString();
            }

            return new ServiceResultModel<TResult>() { IsSuccess = false, SourceApiService = endpoint.Name, ErrorModel = errorModel };
        }

        public async Task<ServiceResultModel<TResult>> CallAsync<TRequest, TResult>(IAuthenticatedEndpoint endpoint, TRequest requestObject, CancellationTokenSource cancellationTokenSource)
        {
            ErrorModel errorModel = new ErrorModel();

            try
            {
                switch (endpoint.HttpAction)
                {
                    case HttpAction.GET:
                        return await _httpGetCaller.CallAsync<ServiceResultModel<TResult>>(endpoint, cancellationTokenSource);
                    case HttpAction.POST:
                        return await _httpPostCaller.CallAsync<TRequest, ServiceResultModel<TResult>>(endpoint, requestObject, cancellationTokenSource);
                    case HttpAction.PUT:
                        return await _httpPutCaller.CallAsync<TRequest, ServiceResultModel<TResult>>(endpoint, requestObject, cancellationTokenSource);
                    case HttpAction.DELETE:
                        return await _httpDeleteCaller.CallAsync<ServiceResultModel<TResult>>(endpoint, cancellationTokenSource);
                    default:
                        throw new UndefinedCallTypeException();
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    if (wex.Response is HttpWebResponse && (wex.Response as HttpWebResponse).StatusCode == HttpStatusCode.Unauthorized)
                    {
                        errorModel.InnerErrors.Add(new ErrorModel()
                        {
                            Code = ((int)HttpStatusCode.Unauthorized).ToString(),
                            Description = wex.Message
                        });

                        return new ServiceResultModel<TResult>()
                        {
                            IsSuccess = false,
                            SourceApiService = endpoint.Name,
                            ErrorModel = errorModel
                        };
                    }
                    else if (wex.Response is HttpWebResponse && (wex.Response as HttpWebResponse).StatusCode == HttpStatusCode.BadRequest)
                    {
                        using (StreamReader streamReader = new StreamReader(wex.Response.GetResponseStream()))
                        {
                            string response = await streamReader.ReadToEndAsync();

                            return JsonConvert.DeserializeObject<ServiceResultModel<TResult>>(response);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorModel.Description = ex.ToString();
            }

            return new ServiceResultModel<TResult>() { IsSuccess = false, SourceApiService = endpoint.Name, ErrorModel = errorModel };
        }
    }
}
