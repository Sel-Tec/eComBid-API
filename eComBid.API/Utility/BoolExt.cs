using System.Runtime.Serialization;

namespace eComBid.API.Utility
{
    [DataContract]
    public class BoolExt
    {
        [DataMember]
        public bool IsSuccessful { get; set; }
        [DataMember]
        public int UpdatedId { get; set; }
        [DataMember]
        public string Message { get; set; }

        //TODO: Need to work futher and add more fields probably

        public BoolExt(bool isSuccessful)
        {
            this.IsSuccessful = isSuccessful;
        }

        public BoolExt(bool isSuccessful, string message)
        {
            this.IsSuccessful = isSuccessful;
            this.Message = message;
        }

        public BoolExt(bool isSuccessful, string message, int updateId)
        {
            this.IsSuccessful = isSuccessful;
            this.Message = message;
            this.UpdatedId = updateId;
        }
    }
}
