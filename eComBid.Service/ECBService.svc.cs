using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using eComBid.API.Domain;

namespace eComBid.Service
{
    public class ECBService : IECBService
    {
        public User GetDummyUser(string username)
        {
            return new User(username, "testPassword", "testEmail@email.com", "testAlternate@email.com", 0, "FacebookId", new UserType(10));
        }

        public Response GetDummyUser2(int userId)
        {
            return new Response() { UserId = userId, Username = "shishir" };
        }
    }
}
