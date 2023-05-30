using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MeetingRegistry.RequestParams
{
    public class RequestParametersGetUser
    {
        [FromHeader]
        public string Login { get; set; } = string.Empty;
        [FromHeader]
        public string Password { get; set; } = string.Empty;
    }
}
