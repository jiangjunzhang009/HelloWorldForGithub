using Microsoft.AspNetCore.Mvc;
using ReceviePcAutoUserCallback.Services;
using ReceviePcAutoUserCallback.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ReceviePcAutoUserCallback.Controllers
{
    [Produces("application/json")]
    [Route("api/ReceiveErrorCallback")]
    public class ReceiveErrorCallbackController : Controller
    {
        #region Members
        readonly ReceiveErrorCallbackService _errorCallbackService = new ReceiveErrorCallbackService();
        private static CustomSetting _customSetting { get; set; }
        #endregion

        #region Methods
        public ReceiveErrorCallbackController(IOptions<CustomSetting> customSettings)
        {
            _customSetting = customSettings.Value;
        }
        [HttpPost]
        public JsonResult Post([FromBody]JObject value)
        {
            BaseResponse baseResponse = _errorCallbackService.AddErrorCallbackLog(_customSetting.MySqlConnection, Encoding.UTF8, value);
            return Json(new { baseResponse });
        }
        [HttpGet]
        public ActionResult Get()
        {
            //return Forbid();
            //System.Net.Http.HttpResponseMessage httpResponseMessage = new System.Net.Http.HttpResponseMessage();
            //httpResponseMessage.Content = new StringContent("Give you no content type response message.", Encoding.UTF8, string.Empty);
            //return httpResponseMessage as ActionResult;
            ContentResult contentResult = new ContentResult();
            contentResult.Content = "Give you no content type response message.";
            //contentResult.ContentType = "excel";
            contentResult.StatusCode = 200;
            return contentResult;
        }
        #endregion
    }
}