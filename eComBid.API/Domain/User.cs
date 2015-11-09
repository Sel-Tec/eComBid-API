using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using eComBid.API.Utility;

namespace eComBid.API.Domain
{
    [DataContract]
    public class UserType : DomainBase
    {
        [DataMember]
        public string Description { get; set; }

        #region constructors

        public UserType()
        {
            this.Id = 0;
            this.Name = "Default User Type";
        }

        public UserType(int id)
        {
            //TODO: Implement the method
        }

        #endregion
    }

    [DataContract]
    public class User : DomainBase
    {
        #region members
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { private get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public new string Name { get { return FirstName + ' ' + LastName; }  private set { }  }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string AlternateEmail { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public string SecondaryPhone { get; set; }
        [DataMember]
        public UserType UType;
        [DataMember]
        public Address Addr;
        [DataMember]
        public DateTime? DOB { get; set; }
        [DataMember]
        public int SocialMediaSourceId { get; set; }
        [DataMember]
        public string SocialMediaId { get; set; }
        private string _avatarURL;
        [DataMember]
        public string AvatarURL { get { return ConfigMember.ImageURL + _avatarURL; } set { _avatarURL = value; } }
        [DataMember]
        public bool? IsBuyer { get; set; }
        [DataMember]
        public bool IsAuthenticated { get; set; }

        #endregion

        #region constructors

        public User() : base()
        {
            this.Id = 0;
            this.IsAuthenticated = false;
            this.Username = "Guest";
            this.UType = new UserType();
        }

        public User(int id) : this()
        {
            User u = User.GetById(id);
            this.Id = u.Id;
            this.Username = u.Username;
            this.Email = u.Email;
            this.FirstName = u.FirstName;
            this.LastName = u.LastName;
            this.AlternateEmail = u.AlternateEmail;
            this.Phone = u.Phone;
            this.SecondaryPhone = u.SecondaryPhone;
            this.UType = u.UType;
            this.Addr = u.Addr;
            this.DOB = u.DOB;
            this.SocialMediaSourceId = u.SocialMediaSourceId;
            this.SocialMediaId = u.SocialMediaId;
            this._avatarURL = u._avatarURL;
            this.IsBuyer = u.IsBuyer;
            this.DateAdded = u.DateAdded;
            this.DateModified = u.DateModified;
            this.IsActive = u.IsActive;

            this.IsAuthenticated = this.Id > 0 ? true : false;
        }

        public User(User u) : this()
        {
            this.Id = u.Id;
            this.Username = u.Username;
            this.Email = u.Email;
            this.FirstName = u.FirstName;
            this.LastName = u.LastName;
            this.AlternateEmail = u.AlternateEmail;
            this.Phone = u.Phone;
            this.SecondaryPhone = u.SecondaryPhone;
            this.UType = u.UType;
            this.Addr = u.Addr;
            this.DOB = u.DOB;
            this.SocialMediaSourceId = u.SocialMediaSourceId;
            this.SocialMediaId = u.SocialMediaId;
            this._avatarURL = u._avatarURL;
            this.IsBuyer = u.IsBuyer;
            this.DateAdded = u.DateAdded;
            this.DateModified = u.DateModified;
            this.IsActive = u.IsActive;

            this.IsAuthenticated = this.Id > 0 ? true : false;
        }

        public User(DAL.User u) : this()
        {
            this.Id = u.Id;
            this.Username = u.Username;
            this.Email = u.Email;
            this.FirstName = u.FirstName;
            this.LastName = u.LastName;
            this.AlternateEmail = u.AlternateEmail;
            this.Phone = u.Phone;
            this.SecondaryPhone = u.SecondaryPhone;
            this.UType = new UserType(u.UserTypeId.HasValue ? u.UserTypeId.Value: 0);
            this.Addr = new Address(u.AddressId.HasValue ? u.AddressId.Value : 0);
            this.DOB = u.DOB;
            this.SocialMediaSourceId = u.SocialMediaSourceId.HasValue ? u.SocialMediaSourceId.Value : 0;
            this.SocialMediaId = u.SocialMediaId;
            this._avatarURL = u.AvatarURL;
            this.IsBuyer = u.IsBuyer;
            this.DateAdded = u.DateAdded;
            this.DateModified = u.DateModified;
            this.IsActive = u.IsActive;

            this.IsAuthenticated = this.Id > 0 ? true : false;
        }

        public User(string username, string password, string email, string firstName, string lastName, string alternateEmail, string phone, string secondaryPhone
                        , UserType uType, Address addr, DateTime? dob, int? socialMediaSourceId, string socialMediaId, string avatarURL, bool isBuyer) : this()
        {
            this.Id = 0;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.AlternateEmail = alternateEmail;
            this.Phone = phone;
            this.SecondaryPhone = secondaryPhone;
            this.UType = uType;
            this.Addr = addr;
            this.DOB = dob;
            this.SocialMediaSourceId = socialMediaSourceId.HasValue ? socialMediaSourceId.Value : 0;
            this.SocialMediaId = socialMediaId;
            this._avatarURL = avatarURL;
            this.IsBuyer = isBuyer;
        }

        #endregion

        #region methods

        public static User GetById(int id)
        {
            DAL.User userObj;

            using (DAL.CentralDBEntities context = new DAL.CentralDBEntities())
            {
                userObj = context.Users.Where(p => p.Id == id && p.IsDeleted == false).FirstOrDefault();
            }
            if (userObj != null)
            {
                User u = new User(userObj);
                return u;
            }
            return new User();
        }

        internal static bool Validate(User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email))
                return false;
            else
                return true;
        }

        internal bool Validate()
        {
            if (string.IsNullOrEmpty(this.Username) || string.IsNullOrEmpty(this.Email))
                return false;
            else
                return true;
        }

        internal int Save()
        {
            //Check if all the objects in User's object is saved
            int result = -1;

            this.Addr.Save();

            if (Validate())
            {
                using (DAL.CentralDBEntities context = new DAL.CentralDBEntities())
                {
                    result = Convert.ToInt32(context.InsertUpdateUser(this.Id, this.Username, this.Password, this.Email, this.Name, this.FirstName, this.LastName, this.AlternateEmail, this.Phone, this.SecondaryPhone
                                                                        , this.UType.Id == 0 ? null : (int?)this.UType.Id, this.Addr.Id == 0 ? null : (int?)this.Addr.Id, this.DOB, this.SocialMediaSourceId, this.SocialMediaId, this._avatarURL, this.IsBuyer).FirstOrDefault());
                    if (result > 0)
                        this.Id = result;
                }
            }
            return result;
        }

        internal static int Save(User u)
        {
            int result = -1;

            u.Addr.Save();

            if (Validate(u))
            {
                using (DAL.CentralDBEntities context = new DAL.CentralDBEntities())
                {
                    result = Convert.ToInt32(context.InsertUpdateUser(u.Id, u.Username, u.Password, u.Email, u.Name, u.FirstName, u.LastName, u.AlternateEmail, u.Phone, u.SecondaryPhone
                                                                        , u.UType.Id, u.Addr.Id, u.DOB, u.SocialMediaSourceId, u.SocialMediaId, u._avatarURL, u.IsBuyer).FirstOrDefault());
                    if (result > 0)
                        u.Id = result;
                }
            }
            return result;
        }

        internal bool Delete()
        {
            int result = -1;
            if (this.Id <= 0) return false;

            using (DAL.CentralDBEntities context = new DAL.CentralDBEntities())
            {
                result = context.DeleteUserById(this.Id);
                if (result > 0)
                    this.Id = result;
            }
            return result > 0 ? true : false ;
        }

        internal static bool DeleteById(int id)
        {
            int result = -1;
            if (id <= 0) return false;

            using (DAL.CentralDBEntities context = new DAL.CentralDBEntities())
            {
                result = context.DeleteUserById(id);
                if (result > 0)
                    id = result;
            }
            return result > 0 ? true : false;
        }

        #endregion
    }
}
