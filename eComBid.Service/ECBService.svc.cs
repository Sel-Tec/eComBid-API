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
                SetResponseAuthentication(ctx);
                return new User(username, "testPassword", "testEmail@email.com", null, null, "testAlternate@email.com", null, null, null, null, null, null, null, null, true);
            }
            else //TODO: Should return an invalid response packet
                return null;
        }

        #region HelperMethods - HTTPHeader

        public void SetResponseAuthentication(WebOperationContext context)
        {
            Authentication auth = new Authentication(0);

            context.OutgoingResponse.Headers.Add("X-Client-Unique-Id", "");
            context.OutgoingResponse.Headers.Add("X-Auth-Status", "");
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
