using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

using eComBid.API.Domain;

namespace eComBid.Service
{
    [ServiceContract]
    public interface IECBService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "GetDummyUser", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        User GetDummyUser(string username);

        [OperationContract]
        [WebInvoke(UriTemplate = "Login", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LoginResponse Login(LoginRequest request);
    }
}
