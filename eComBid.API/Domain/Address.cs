using System;
using System.Linq;
using System.Runtime.Serialization;
namespace eComBid.API.Domain
{
    [DataContract]
    public class Address : DomainBase
    {
        #region members
        [DataMember]
        public string AddressLine1 { get; set; }
        [DataMember]
        public string AddressLine2 { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
        [DataMember]
        public bool IsBillingAddress { get; set; }
        [DataMember]
        public bool IsShippingAddress { get; set; }
        #endregion

        #region constructors
        public Address() : base()
        {
            this.IsBillingAddress = true;
            this.IsShippingAddress = true;
        }

        public Address(int id) : this()
        {
            Address addr = Address.GetById(id);
            this.Id = addr.Id;
            this.AddressLine1 = addr.AddressLine1;
            this.AddressLine2 = addr.AddressLine2;
            this.City = addr.City;
            this.State = addr.State;
            this.Country = addr.Country;
            this.ZipCode = addr.ZipCode;
            this.IsBillingAddress = addr.IsBillingAddress;
            this.IsShippingAddress = addr.IsShippingAddress;
        }

        public Address(string addressLine1, string addressLine2, string city, string state, string country, string zip) : this()
        {
            this.Id = 0;
            this.AddressLine1 = addressLine1;
            this.AddressLine2 = addressLine2;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.ZipCode = zip;
        }

        public Address(DAL.Address addr) : this()
        {
            this.Id = addr.Id;
            this.AddressLine1 = addr.AddressLine1;
            this.AddressLine2 = addr.AddressLine2;
            this.City = addr.City;
            this.State = addr.State;
            this.Country = addr.Country;
            this.ZipCode = addr.ZipCode;
            this.IsBillingAddress = addr.IsBillingAddress.HasValue ? addr.IsBillingAddress.Value : true;
            this.IsShippingAddress = addr.IsShippingAddress.HasValue ? addr.IsShippingAddress.Value : true;
        }
        #endregion

        #region methods

        public static Address GetById(int id)
        {
            DAL.Address addressObj;
            using (DAL.CentralDBEntities context = new DAL.CentralDBEntities())
            {
                addressObj = context.Addresses.Where(a => a.Id == id && a.IsDeleted == false).FirstOrDefault();
            }
            if (addressObj != null)
            {
                Address addr = new Address(addressObj);
                return addr;
            }
            return new Address();
        }

        internal static bool Validate(Address addr)
        {
            if (string.IsNullOrEmpty(addr.City) && string.IsNullOrEmpty(addr.State) && string.IsNullOrEmpty(addr.ZipCode))
                return false;
            else
                return true;
        }

        internal bool Validate()
        {
            if (string.IsNullOrEmpty(City) && string.IsNullOrEmpty(State) && string.IsNullOrEmpty(ZipCode))
                return false;
            else
                return true;
        }

        internal int Save()
        {
            int result = 0;

            if (Validate())
            {
                using (DAL.CentralDBEntities context = new DAL.CentralDBEntities())
                {
                    result = Convert.ToInt32(context.InsertUpdateAddress(this.Id, this.AddressLine1, this.AddressLine2, this.City, this.State, this.Country, this.ZipCode, this.IsBillingAddress, this.IsShippingAddress).FirstOrDefault());
                    if (result > 0)
                        this.Id = result;
                }
            }
            return result;
        }

        internal static int Save(Address addr)
        {
            int result = 0;

            if (Validate(addr))
            {
                using (DAL.CentralDBEntities context = new DAL.CentralDBEntities())
                {
                    result = Convert.ToInt32(context.InsertUpdateAddress(addr.Id, addr.AddressLine1, addr.AddressLine2, addr.City, addr.State, addr.Country, addr.ZipCode, addr.IsBillingAddress, addr.IsShippingAddress).FirstOrDefault());
                    if (result > 0)
                        addr.Id = result;
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
                result = context.DeleteAddressById(this.Id);
                if (result > 0)
                    this.Id = result;
            }
            return result > 0 ? true : false;
        }

        internal static bool DeleteById(int id)
        {
            int result = -1;
            if (id <= 0) return false;

            using (DAL.CentralDBEntities context = new DAL.CentralDBEntities())
            {
                result = context.DeleteAddressById(id);
                if (result > 0)
                    id = result;
            }
            return result > 0 ? true : false;
        }

        #endregion
    }
}
