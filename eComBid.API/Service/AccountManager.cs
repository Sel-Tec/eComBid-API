using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eComBid.API;
using eComBid.API.Domain;

namespace eComBid.API.Service
{
    public class AccountManager
    {
        public static User Login(string username, string password)
        {
            bool isAuthenticated = false;
            int? userId = null;
            int id;

            using (DAL.CentralDBEntities context = new DAL.CentralDBEntities())
            {
                System.Data.Entity.Core.Objects.ObjectResult<int?> uid = context.UserLogin(username, password, null);
                userId = uid.FirstOrDefault();
            }

            id = userId == null || userId == 0 ? -1 : userId.Value;
            isAuthenticated = userId == null || userId == 0 ? false : true;

            User user = new User(id);
            user.IsAuthenticated = isAuthenticated;

            return user;
        }

        public static User RegisterNewUser(string username, string password, string email, string firstName, string lastName, string alternateEmail, string phone, string secondaryPhone
                                                , int? userType, DateTime? dob, int? socialMediaSourceId, string socialMediaId, string avatarURL, bool isBuyer
                                                , string addressLine1, string addressLine2, string city, string state, string country, string zipCode)
        {
            //Validations
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                throw new Exception("username or password can not be blank");
            }

            //TODO: Check all params for rogue data

            UserType uType = new UserType(userType != null ? userType.Value : 0);

            Address addr = new Address(addressLine1, addressLine2, city, state, country, zipCode);

            //TODO: Create User Object

            User u = new User(username, password, email, firstName, lastName, alternateEmail, phone, secondaryPhone, uType, addr, dob, socialMediaSourceId != null ? socialMediaSourceId.Value : 0, socialMediaId, avatarURL, isBuyer);

            //TODO: Also add a virtual pet simultaneously


            return RegisterNewUser(u);
        }

        public static User RegisterNewUser(string username, string password, string firstName, string lastName, string email, DateTime? dob)
        {
            //Call to the most basic method
            return RegisterNewUser(username, password, email, firstName, lastName, null, null, null, null, dob, null, null, null, true, null, null, null, null, null, null) ;
        }

        public static User RegisterNewUser(User u)
        {
            int result = u.Save();
            if (result > 0)
            {
                u.Id = result;
                u.IsAuthenticated = true;
            }
            else
                return null;
            return u;
        }

        public static bool IsExistingUser(string email)
        {
            int count = 0;
            using (DAL.CentralDBEntities context = new DAL.CentralDBEntities())
            {
                count = context.Users.Where(u => u.Email == email).Count();
            }

            if (count > 0)
                return true;
            else
                return false;
        }
    }
}
