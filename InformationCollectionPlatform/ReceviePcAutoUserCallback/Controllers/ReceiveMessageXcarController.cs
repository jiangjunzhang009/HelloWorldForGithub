using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReceviePcAutoUserCallback.Services;
using ReceviePcAutoUserCallback.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace ReceviePcAutoUserCallback.Controllers
{
    [Produces("application/json")]
    public class ReceiveMessageXcarController : Controller
    {
        ReceiveMessageService receiveMessageService = new ReceiveMessageService();
        private static CustomSetting _customSetting { get; set; }
        public ReceiveMessageXcarController(IOptions<CustomSetting> customSettings)
        {
            _customSetting = customSettings.Value;
        }
        [Route("api/ReceiveMessageXcar")]
        [HttpPost]
        public JsonResult Post([FromBody]JObject value)
        {
            BaseResponse baseRespones = new BaseResponse();
            string message = "Xcar====default009";
            try
            {
                if (null != value)
                {
                    //message = $"Xcar====value.Count: {value.Count}";
                    message = "Xcar====" + value.ToString();
                }
                else
                {
                    message = "Xcar====arguement value is null.";
                }
            }
            catch (Exception ex)
            {
                message = "Xcar====some exception occured.";
                if (null != ex)
                {
                    message += $" Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                }
            }
            receiveMessageService.RecordOperationMessage(message, _customSetting.MySqlConnection);
            baseRespones.ErrorId = 0;
            return Json(new { baseRespones });
        }
        [Route("api/ReceiveMessageXcar")]
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(403);
        }
    }
}