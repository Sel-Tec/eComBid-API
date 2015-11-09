using System;
using eComBid.API.Utility;

namespace eComBid.API.Security
{
    public class Authentication
    {
        #region members
        public string _authToken;

        const char _seperator = '}';
        const double _tokenValidity = 60;                               //Token validity in hours
        const string _encryptionPassword = "ksg$645yu^&%$gf";           //Random password to safeguard the key

        public string AuthToken
        {
            get { return this._authToken; }
            private set { _authToken = value; }
        }

        public string DeviceId { get; set; }
        public int UserId { get; set; }
        public DateTime TokenGenerationDT { get; set; }
        public DateTime TokenExpirationDT { get { return TokenGenerationDT.AddHours(_tokenValidity); } }
        public bool IsValid { get; set; }

        #endregion

        #region constructors

        public Authentication(string authToken)
        {
            _authToken = authToken;
            string key = AESThenHMAC.SimpleDecryptWithPassword(_authToken, _encryptionPassword);
            string[] keys = key.Split(_seperator);
            DeviceId = keys[1];
            UserId = Convert.ToInt32(keys[2]);
            TokenGenerationDT = Convert.ToDateTime(keys[3]);
            if (TokenExpirationDT > DateTime.Now)
                IsValid = false;
            else
                IsValid = true;
        }

        public Authentication(int userId, string deviceId)
        {
            _authToken = GenerateNewToken(userId, deviceId);
            UserId = userId;
            DeviceId = deviceId;
            TokenGenerationDT = DateTime.Now;
        }

        #endregion

        #region methods

        public static string GenerateNewToken(int userId, string deviceId)
        {
            string key = Guid.NewGuid().ToString() + _seperator 
                        + deviceId + _seperator 
                        + userId + _seperator 
                        + DateTime.Now.ToString();

            string AuthToken = AESThenHMAC.SimpleEncryptWithPassword(key, _encryptionPassword);
            
            return AuthToken;
        }



        public string RefreshToken(int userId, string authToken)
        {
            return "";
        }

        public bool IsValidToken()
        {
            //Implement the body
            return true;
        }

        #endregion
    }
}
