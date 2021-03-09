using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificationAPI.Services;
using Shared;

namespace NotificationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;
        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("{code}")]
        public async Task<IActionResult> SendNotification(string code)
        {
            var model =  new ViewModel
            {
                Date = DateTime.Now,
                Code = code
            };

            await _service.SendMessageToBus(model);
            return Ok();
        }
    }
}
