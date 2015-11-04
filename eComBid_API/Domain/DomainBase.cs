using System;
using System.Runtime.Serialization;

namespace eComBid_API.Domain
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
        #endregion
    }
}