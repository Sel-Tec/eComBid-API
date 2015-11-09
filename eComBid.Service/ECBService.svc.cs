using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using eComBid.API.Domain;
using System.ServiceModel.Channels;
using eComBid.API.Security;

namespace eComBid.Service
{
    public class ECBService : IECBService
    {
        public User GetDummyUser(string username)
        {
            WebOperationContext ctx = WebOperationContext.Current;
            if (IsRequestAuthentic(ctx))
            {
                Authentication auth = new Authentication(10, "IMEI-12345");
                SetResponseAuthentication(ctx, auth);
                return new User(username, "testPassword", "testEmail@email.com", null, null, "testAlternate@email.com", null, null, null, null, null, null, null, null, true);
            }
            else //TODO: Should return an invalid response packet
                return null;
        }

        public LoginResponse Login(LoginRequest request)
        {
            WebOperationContext ctx = WebOperationContext.Current;
            Authentication auth = new Authentication(10, "IMEI-12345");
            SetResponseAuthentication(ctx, auth);

            return null;
        }


        #region HelperMethods - HTTPHeader

        public void SetResponseAuthentication(WebOperationContext context, Authentication auth)
        {
            //Authentication auth = new Authentication();

            context.OutgoingResponse.Headers.Add("X-Client-Unique-Id", auth.DeviceId);
            context.OutgoingResponse.Headers.Add("X-Auth-Status", auth.IsValid.ToString());
            context.OutgoingResponse.Headers.Add("X-User-Id", auth.UserId.ToString());
        }

        public bool IsRequestAuthentic(WebOperationContext context)
        {
            if (context.IncomingRequest.Headers.AllKeys.Contains("X-Client-Unique-Id") && context.IncomingRequest.Headers.AllKeys.Contains("X-Session-Token") && context.IncomingRequest.Headers.AllKeys.Contains("X-User-Id"))
            {
                string authToken;
                string deviceId;
                string userId;

                ReadRequestHeader(context, out authToken, out deviceId, out userId);

                if (!string.IsNullOrEmpty(authToken) && !string.IsNullOrEmpty(deviceId) && !string.IsNullOrEmpty(userId))
                {
                    Authentication auth = new Authentication(authToken);

                    if (auth.DeviceId == deviceId && auth.UserId.ToString() == userId)
                        return true;
                }
            }
            return false;
        }

        public void ReadRequestHeader(WebOperationContext context, out string authToken, out string deviceId, out string userId)
        {
            authToken = "";
            deviceId = "";
            userId = "";

            deviceId = context.IncomingRequest.Headers.Get("X-Client-Unique-Id");
            authToken = context.IncomingRequest.Headers.Get("X-Session-Token");
            userId = context.IncomingRequest.Headers.Get("X-User-Id");
        }

        #endregion
    }
}
