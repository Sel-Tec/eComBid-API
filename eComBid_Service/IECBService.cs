using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

using eComBid_API.Domain;

namespace eComBid_Service
{
    [ServiceContract]
    public interface IECBService
    {

        [OperationContract]
        [WebInvoke(UriTemplate = "GetDummyUser", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        User GetDummyUser(string username);

        //[OperationContract]
        //[WebInvoke(UriTemplate = "LoginUser", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //LoginResponse LoginUser(LoginRequest loginDetails);
    }
}
