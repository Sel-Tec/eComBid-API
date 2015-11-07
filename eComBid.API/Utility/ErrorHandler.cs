using System;
using System.Runtime.Serialization;

namespace eComBid.API.Utility
{

    //Define error codes types and their descripion
    //  Error Code      Description
    //  10XX            Error Codes specific to the DB
    //  20XX            Error Codes specific to the API
    //  30XX            Error Codes specific to the WebService
    //  40XX 
    
    [DataContract]
    public class ErrorHandler
    {
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string ErrorDescription { get; private set; }
        [DataMember]
        public string FriendlyMessage { get; private set; }
        public Exception IntenralException { get; set; }

        private ErrorHandler()
        {

        }

        public ErrorHandler(int errorCode)
        {

        }

        public ErrorHandler(int errorCode, Exception internalException)
        {

        }

        public static ErrorHandler GetById(int errorCode)
        {
            ErrorHandler errHandler = new ErrorHandler();

            return;
        }
    }
}
