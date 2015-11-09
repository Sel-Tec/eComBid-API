using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

using eComBid.API.Domain;

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

        public Response()
        {
            Id = -1;
            IsSuccess = false;
            Code = 999;
            Message = null;
        }
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
        [DataMember]
        public new User Message { get; set; }

    }

    #endregion

    #region Registration

    [DataContract]
    public class RegisterRequest : Request
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }
    }

    [DataContract]
    public class RegisterResponse : Response
    {
        
    }

    #endregion
}