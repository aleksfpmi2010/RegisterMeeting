using MeetingRegistryClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using MeetingRegistryClient.RequestParams;

namespace MeetingRegistryClient.Services
{
    public interface IMeetingRegistryService
    {
        public Task<IEnumerable<Meeting>> GetMeetingsList();
        public Task<bool> RegisterForMeeting(RequestParamsRegisterForMeeting requestParamsRegisterForMeeting,
            int phase);
		public Task<User> GetUser(LoginPassword model);
        public Task<bool> AddMeeting(Meeting meeting);
	}
}
