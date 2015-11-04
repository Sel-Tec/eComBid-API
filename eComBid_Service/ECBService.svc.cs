using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using eComBid_API.Domain;

namespace eComBid_Service
{
    public class ECBService : IECBService
    {
        public User GetDummyUser(string username)
        {
            return new User(username, "testPassword", "testEmail@email.com", "testAlternate@email.com", 0, "FacebookId", new UserType(10));
        }
    }
}
