using firebase.pushnotification.Models;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace firebase.pushnotification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SendMessageAsync([FromBody] NotificationRequest request)
        {
            var message = new Message()
            {
                Token = request.DeviceToken,

                Notification = new Notification
                {
                    Title = request.Title,
                    Body = request.Body,
                },

                Data = request.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(request, null))

                //Data = new Dictionary<string, string>()
                //{
                //    ["ResourceURI"] = "/core/leave/approveLeave/339/1257",
                //    ["Title"] = "1 day Casual Leave request from Sai Maram"
                //},
                //Data = new Dictionary<string, string>(),
            };

            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);

            if (!string.IsNullOrEmpty(result))
            {
                // Message was sent successfully
                return Ok("Message sent successfully!");
            }
            else
            {
                // There was an error sending the message
                throw new Exception("Error sending the message.");
            }
        }
    }
}
