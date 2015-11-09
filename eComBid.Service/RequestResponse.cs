using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace eComBid.Service
{
    [DataContract]
    public class Request
    {
        
    }

    [DataContract]
    public class Response
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public bool IsSuccess { get; set; }
        [DataMember]
        public int Code { get; set; }
        [DataMember]
        public object Message { get; set; }
    }


    #region Login

    [DataContract]
    public class LoginRequest : Request
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public bool IsFirstLogin { get; set; }
    }

    [DataContract]
    public class LoginResponse : Response
    {
        [DataMember]
        public string AuthToken { get; set; }
    }

    #endregion
}