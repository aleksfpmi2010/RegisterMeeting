using System.Text.Json.Serialization;

namespace MeetingRegistryClient.RequestParams
{
    public class RequestParamsRegisterForMeeting
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }
        [JsonPropertyName("id ")]
        public int Id { get; set; }
    }
}
