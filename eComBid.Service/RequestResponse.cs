using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace eComBid.Service
{
    [MessageContract]
    public class Request
    {
        [MessageHeader]
        public int UserId { get; set; }

        [MessageBodyMember]
        public string MyProperty { get; set; }
    }

    [MessageContract]
    public class Response
    {
        [MessageHeader]
        public int UserId { get; set; }

        [MessageBodyMember]
        public string Username { get; set; }
    }

    //{
    //Accept = "application/json; charset=utf-8";
    //"Content-Encoding" = gzip;
    //"Content-Type" = "application/json; charset=utf-8";
    //"X-Client-Unique-Id" = "2D6F79B4-7662-4A14-B9B1-B4A93CC74C7F";
    //"X-Session-Token" = "";
    //"X-User-Id" = "";
    //}
}