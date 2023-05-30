
using System.Text.Json.Serialization;

namespace MeetingRegistryClient.Models
{
    public class Meeting
    {
        [JsonPropertyName("meetingId")]
        public int MeetingId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("users")]
        public string[] Users { get; set; }
    }
}
