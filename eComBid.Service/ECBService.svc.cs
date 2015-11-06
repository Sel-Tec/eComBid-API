using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using eComBid.API.Domain;
using System.ServiceModel.Channels;

namespace eComBid.Service
{
    public class ECBService : IECBService
    {
        public User GetDummyUser(string username)
        {
            WebOperationContext ctx = WebOperationContext.Current;
            if (IsRequestAuthentic(ctx))
            {
                SetResponseAuthentication(ctx);
                return new User(username, "testPassword", "testEmail@email.com", "testAlternate@email.com", 0, "FacebookId", new UserType(10));
            }
            else //TODO: Should return an invalid response packet
                return null;
        }

        #region HelperMethods

        public void SetResponseAuthentication(WebOperationContext context)
        {
            context.OutgoingResponse.Headers.Add("X-Client-Unique-Id", "");
            context.OutgoingResponse.Headers.Add("X-Session-Token", "");
            context.OutgoingResponse.Headers.Add("X-User-Id", "");
        }

        public bool IsRequestAuthentic(WebOperationContext context)
        {
            if (context.IncomingRequest.Headers.AllKeys.Contains("X-Client-Unique-Id"))
                return true;
            return false;
        }

        #endregion
    }
}
