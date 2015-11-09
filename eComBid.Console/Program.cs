using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using eComBid.API;
using eComBid.API.Domain;
using eComBid.API.Service;

namespace eComBid.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = AccountManager.Login("shishir333", "myPass");
        }
    }
}
