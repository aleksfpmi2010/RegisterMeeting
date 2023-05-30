using MeetingRegistryClient.Services;
using Microsoft.AspNetCore.Mvc;
using MeetingRegistryClient.Models;
using MeetingRegistryClient.RequestParams;

namespace MeetingRegistryClient.Controllers
{
    public class MeetingRegistryController : Controller
    {
        private readonly IMeetingRegistryService _service;

        public MeetingRegistryController(IMeetingRegistryService service)
        {
            _service = service;
        }

        public ActionResult MeetingsList()
        {
            var meetings = (_service.GetMeetingsList()).Result.ToList<Meeting>();
            var meetingsList = new MeetingsList();
            meetingsList.Meetings = new List<Meeting>();
            meetingsList.Meetings.AddRange(meetings);
            return View(meetingsList);
        }

        public IActionResult RegisterToMeeting(string login, int id)
        {
            try
            {
                var requestParams = new RequestParamsRegisterForMeeting()
                {
                    Login = login,
                    Id = id
                };

                var result = _service.RegisterForMeeting(requestParams, 1).Result;
                if (result)
                    return new OkResult();
                return new BadRequestResult();
            }
            catch(Exception ex)
            {
                return new BadRequestResult();
            }
        }

		public IActionResult CreateMeeting([FromQuery] string name,
			[FromQuery] string description)
		{
			try
			{
				Random rnd = new Random();
				var meeting = new Meeting
                {
                    MeetingId = rnd.Next(100),
                    Name = name,
                    Description = description,
                    Users = new string[0]
                };
				var result = _service.AddMeeting(meeting).Result;
				if (result)
					return new OkResult();
				return new BadRequestResult();
			}
			catch (Exception ex)
			{
				return new BadRequestResult();
			}
		}
	}
}
