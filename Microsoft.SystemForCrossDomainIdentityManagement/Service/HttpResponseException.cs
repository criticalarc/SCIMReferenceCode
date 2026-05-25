// Copyright (c) Microsoft Corporation.// Licensed under the MIT license.

namespace Microsoft.SCIM
{
    using System;
    using System.Net;
    using System.Net.Http;

    public class HttpResponseException : Exception
    {
        public HttpResponseException(HttpStatusCode statusCode)
            : base(statusCode.ToString())
        {
            Response = new HttpResponseMessage(statusCode);
        }

        public HttpResponseException(HttpResponseMessage response)
            : base(response?.StatusCode.ToString())
        {
            Response = response;
        }

        public HttpResponseMessage Response { get; }
    }
}
