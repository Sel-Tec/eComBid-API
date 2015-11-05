using System;

namespace eComBid.API.Security
{
    public class Authentication
    {
        #region members
        public string _authToken;

        const string _seperator = "ZxA(";
        const double _tokenValidity = 60; //token validity in hours

        public string AuthToken
        {
            get { return this._authToken; }
        }

        public string DeviceId { get; set; }
        public int UserId { get; set; }
        public DateTime TokenGenerationDT { get; set; }
        public DateTime TokenExpirationDT { get { return TokenGenerationDT.AddHours(_tokenValidity); } }

        #endregion

        #region constructors

        public Authentication(string authToken)
        {

        }

        public Authentication(int userId)
        {

        }

        #endregion

        #region methods

        public string GenerateNewToken(int userId)
        {
            string key = Guid.NewGuid().ToString() + _seperator 
                        + DeviceId + _seperator 
                        + UserId + _seperator 
                        + TokenGenerationDT;
            return "";
        }



        public string RefreshToken(int userId, string authToken)
        {
            return "";
        }

        #endregion
    }
}
