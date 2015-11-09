using System;
using System.Linq;
using System.Runtime.Serialization;

namespace eComBid.API.Utility
{

    //Define error codes types and their descripion
    //  Error Code      Description
    //  10XX            Authentication Errors
    //  20XX            Error Codes specific to the DB
    //  30XX            Error Codes specific to the API
    //  40XX            Error Codes specific to the WebService
    //  50XX            Business Logic error

    
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
            ErrorCode = 999;
            ErrorDescription = "Undefined Error";
            FriendlyMessage = "Error Occurred: Debuggers on their way!";
        }

        public ErrorHandler(int errorCode) : this()
        {
            using (DAL.CentralDBEntities context = new DAL.CentralDBEntities())
            {
                DAL.ECBError error = context.ECBErrors.Where(e => e.ErrorCode == errorCode).FirstOrDefault();

                this.ErrorCode = error.ErrorCode;
                this.ErrorDescription = error.Description;
                this.FriendlyMessage = error.FriendlyMessage;
            }
        }

        public ErrorHandler(int errorCode, Exception internalException) : this(errorCode)
        {
            this.IntenralException = internalException;
        }

        public static string GetErrorCodeDescription(int errorCode)
        {
            string description;
            using (DAL.CentralDBEntities context = new DAL.CentralDBEntities())
            {
                DAL.ECBError error = context.ECBErrors.Where(e => e.ErrorCode == errorCode).FirstOrDefault();

                description = error.Description;
            }

            return description;
        }
    }
}
