using System;

namespace eComBid_API.Security
{
    public class Authentication
    {
        #region members
        public string _authToken;

        public string AuthToken
        {
            get { return this._authToken; }
        }

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

        public static string GenerateNewToken(int userId)
        {
            string key = Guid.NewGuid().ToString() +"ZxA" + "Replace with IMEI" + "ZxA" + "Replace with UserId" + "ZxA" + "replace with datetime";
            return "";
        }

        public static string RefreshToken(int userId, string authToken)
        {
            return "";
        }

        #endregion
    }
}
