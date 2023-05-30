using MongoDB.Bson.Serialization.Attributes;

namespace MeetingRegistry.RequestParams
{
    public class RequestParamsArrangeMeeting
    {
        public int MeetingId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
