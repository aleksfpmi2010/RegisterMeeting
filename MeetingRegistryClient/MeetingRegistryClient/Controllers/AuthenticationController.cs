using MeetingRegistryClient.Models;
using MeetingRegistryClient.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingRegistryClient.Controllers
{
    public class AuthenticationController : Controller
    {
		private readonly IMeetingRegistryService _service;

		public AuthenticationController(IMeetingRegistryService service)
		{
			_service = service;
		}

		public ActionResult Login()
		{
			var model = new LoginPassword();
            return View("LoginPassword", model);
        }

		public ActionResult Accept([FromQuery] string login, [FromQuery] string password)
        {
			var model = new LoginPassword
			{ 
				Login = login,
				Password = password
			};

			var user = _service.GetUser(model).Result;
			if (user != null && user.Login != "admin")
			{
					return RedirectToAction("MeetingsList", "MeetingRegistry");
			}
			else
                return View("Meeting", new Meeting());
        }

	}
}
