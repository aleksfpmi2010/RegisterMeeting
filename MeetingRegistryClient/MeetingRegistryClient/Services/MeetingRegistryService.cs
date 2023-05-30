using MeetingRegistryClient.Models;
using MeetingRegistryClient.RequestParams;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Reflection;

namespace MeetingRegistryClient.Services
{
    public class MeetingRegistryService: IMeetingRegistryService
    {
        private readonly HttpClient _client;
        public const string GetMeetingsListPath = "/GetMeetingsList";
        public const string RegisterForMeetingPath = "/RegisterForMeeting?=phase=1";
		public const string GetUserPath = "/GetUser";
		public const string AddMeetingPath = "AddMeeting";

        public MeetingRegistryService()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5160/"),
                Timeout = new TimeSpan(0, 1, 0)
            };
        }
        public async Task<IEnumerable<Meeting>> GetMeetingsList()
        {
            var response = await _client.GetAsync(GetMeetingsListPath);
            var meetings = await JsonSerializer.DeserializeAsync<IEnumerable<Meeting>>(await response.Content.ReadAsStreamAsync());
            return meetings;
        }
        public async Task<bool> RegisterForMeeting(RequestParamsRegisterForMeeting requestParamsRegisterForMeeting,
            int phase)
        {
            try
            {
                var response = await _client.PostAsJsonAsync<RequestParamsRegisterForMeeting>
                    (RegisterForMeetingPath, requestParamsRegisterForMeeting);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
		public async Task<User> GetUser(LoginPassword model)
        {
			try
			{
				using (var requestMessage =
			new HttpRequestMessage(HttpMethod.Get, _client.BaseAddress + GetUserPath))
				{
					requestMessage.Headers.Add("Login", model.Login);
					requestMessage.Headers.Add("Password", model.Password);

					var response = await _client.SendAsync(requestMessage);
					var user = await JsonSerializer.DeserializeAsync<User>
                        (await response.Content.ReadAsStreamAsync());
                    return user;
				}
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public async Task<bool> AddMeeting(Meeting meeting)
		{
			try
			{
				using (var requestMessage =
			new HttpRequestMessage(HttpMethod.Post, _client.BaseAddress + AddMeetingPath))
				{
					requestMessage.Content = JsonContent.Create(meeting);

					var response = await _client.SendAsync(requestMessage);
					return await JsonSerializer.DeserializeAsync<bool>
						(await response.Content.ReadAsStreamAsync());
				}
			}
			catch (Exception ex)
			{
				return false;
			}
		}

	}
}
