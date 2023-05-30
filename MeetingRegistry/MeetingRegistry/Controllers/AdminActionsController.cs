using MeetingRegistry.MongoDbModels;
using MeetingRegistry.RequestParams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using MeetingRegistry.EmailService;

namespace MeetingRegistryApp.Controllers
{
    [ApiController]
    public class MeetingRegistryController : ControllerBase
    {
        private readonly ILogger<MeetingRegistryController> _logger;
        private IMongoDataManager _mongoDataManager;
        private IEmailService _emailService;

        public MeetingRegistryController(ILogger<MeetingRegistryController> logger,
            IMongoDataManager mongoDataManager,
            IEmailService emailService)
        {
            _logger = logger;
            _mongoDataManager = mongoDataManager;
            _emailService = emailService;
        }

        [HttpPost("AddMeeting")]
        public async Task<bool> AddMeeting([FromBody] RequestParamsArrangeMeeting requestParamsArrangeMeeting)
        {
            try
            {
                var meeting = new Meeting
                {
                    MeetingId = requestParamsArrangeMeeting.MeetingId,
                    Name = requestParamsArrangeMeeting.Name,
                    Description = requestParamsArrangeMeeting.Description,
                    Users = new string[] { }
                };
                return await _mongoDataManager.CreateMeetingAsync(meeting);
            }
            catch
            {
                return false;
            }
        }

        [HttpGet("GetUser")]
        public async Task<User> GetUser([FromQuery] RequestParametersGetUser requestParametersGetUser)
        {
            var user = await _mongoDataManager.GetUserByLoginAndPasswordAsync(requestParametersGetUser.Login,
                requestParametersGetUser.Password);
            return user;
        }

		[HttpGet("GetMeetingsList")]
        public IEnumerable<Meeting> GetMeetingsList()
        {
            return _mongoDataManager.GetMeetings();
        }

        [HttpPost("RegisterForMeeting")]
        public async Task<bool> RegisterForMeeting([FromBody, Required] RequestParamsRegisterForMeeting requestParamsRegisterForMeeting,
            [FromQuery] int phase)
        {
            var email = (await _mongoDataManager.GetUserByLoginAsync(requestParamsRegisterForMeeting.Login))?.Email;
            if (phase == 1 && email != null)
            {
                await _emailService.SendEmailAsync(email);
                return true;
            }
            if (phase == 2)
            {
                return await _mongoDataManager.Update(requestParamsRegisterForMeeting.Id,
                requestParamsRegisterForMeeting.Login);
                return true;
            }
            else
                return false;
        }
    }
}