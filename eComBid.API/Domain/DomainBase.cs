using System;
using System.Runtime.Serialization;

namespace eComBid.API.Domain
{
    [DataContract]
    public class DomainBase
    {
        #region members
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime DateAdded { get; set; }
        [DataMember]
        public DateTime DateModified { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool InSyncWithDB { get; set; }
        #endregion

        #region constructor

        internal DomainBase()
        {
            this.Id = 0;
            this.Name = "";
            this.DateAdded = DateTime.Now;
            this.DateModified = DateTime.Now;
            this.IsActive = true;
            this.IsDeleted = false;
            this.InSyncWithDB = false;
        }

        internal DomainBase(string name) : base()
        {
            this.Name = name;
        }

        internal DomainBase(int id, string name) : base()
        {
            this.Id = id;
            this.Name = name;
        }

        internal DomainBase(DomainBase domainBase)
        {
            this.Id = domainBase.Id;
            this.Name = domainBase.Name;
            this.DateAdded = domainBase.DateAdded;
            this.DateModified = domainBase.DateModified;
            this.IsActive = domainBase.IsActive;
            this.IsDeleted = domainBase.IsDeleted;
            this.InSyncWithDB = domainBase.InSyncWithDB;
        }

        #endregion
    }
}