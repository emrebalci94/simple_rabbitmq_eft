using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using simple_rabbitmq_eft.Api.Helpers;
using simple_rabbitmq_eft.Database.Models;

namespace simple_rabbitmq_eft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EftController : ControllerBase
    {
        [HttpPost("send")]
        public IActionResult SendToMoney([FromBody] SendingEftModel model)
        {
            EftProducerHelper.SendMoney(model);
            return Ok("EFT İsteğiniz listelenmiştir.");
        }
    }
}
