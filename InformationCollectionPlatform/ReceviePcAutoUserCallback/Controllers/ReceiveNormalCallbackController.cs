using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ReceviePcAutoUserCallback.Models;
using Microsoft.Extensions.Options;
using ReceviePcAutoUserCallback.Services;
using System.Text;
using ReceviePcAutoUserCallback.Models.MediaDissectorModel;

namespace ReceviePcAutoUserCallback.Controllers
{
    [Produces("application/json")]
    [Route("api/ReceiveNormalCallback")]
    public class ReceiveNormalCallbackController : Controller
    {
        #region Members
        readonly ReceiveNormalCallbackService _receiveNormalCallbackService = new ReceiveNormalCallbackService();
        private static CustomSetting _customSetting { get; set; }
        #endregion

        #region Constructor
        public ReceiveNormalCallbackController(IOptions<CustomSetting> customSettings)
        {
            _customSetting = customSettings.Value;
        }
        #endregion

        #region Methods
        [HttpPost]
        public JsonResult Post([FromBody]PageModule value)
        {
            BaseResponse baseResponse = _receiveNormalCallbackService.HandleReceivedMessage(_customSetting.MySqlConnection, value);
            return Json(new { baseResponse});
        }

        [HttpGet]
        public IActionResult Get()
        {
            ////转移生产环境中的mediadissector中的piep_templates到测试环境
            //IEnumerable<piep_templates> transferRecord = _receiveNormalCallbackService.Getpiep_templates(_customSetting.MySqlDevMediaDissectorConnection, "47");
            ////IEnumerable<piep_templates> transferRecord = _receiveNormalCallbackService.Getpiep_templates(_customSetting.MySqlProMediaDissectorConnection, "56,57");
            //if (null != transferRecord)
            //{
            //    foreach (var item in transferRecord)
            //    {
            //        _receiveNormalCallbackService.Addpiep_templates(_customSetting.MySqlDevMediaDissectorConnection, item);
            //    }
            //}

            //BaseResponse baseResponse = _receiveNormalCallbackService.HandleReceivedMessage(_customSetting.MySqlConnection, null);
            //return Json(new { baseResponse });

            var tb_ErrorCallbackRecords = _receiveNormalCallbackService.GetTb_ErrorCallbackLog(_customSetting.MySqlConnection);
            var tb_ExceptionRecordsOne = _receiveNormalCallbackService.GetTb_ExceptionLog(_customSetting.MySqlConnection);
            var tbhishomeRecords = _receiveNormalCallbackService.GetTbhisHome(_customSetting.MySqlConnection, 0, 100);
            var tb_OperationRecords = _receiveNormalCallbackService.GetTb_OperationLog(_customSetting.MySqlConnection);
            List<string> errorCallbackMessages = new List<string>();
            List<string> exceptionMessages = new List<string>();
            List<string> tbHishomeContents = new List<string>();
            List<string> operationLogMessages = new List<string>();
            if (null != tb_ErrorCallbackRecords)
            {
                foreach (var item in tb_ErrorCallbackRecords)
                {
                    if (null != item.OtherMessage)
                    {
                        errorCallbackMessages.Add(Encoding.UTF8.GetString(item.OtherMessage));
                    }
                    else
                    {
                        errorCallbackMessages.Add(string.Empty);
                    }
                }
            }
            if (null != tb_ExceptionRecordsOne)
            {
                foreach (var item in tb_ExceptionRecordsOne)
                {
                    if (null != item.OtherMessage)
                    {
                        exceptionMessages.Add(Encoding.UTF8.GetString(item.OtherMessage));
                    }
                    else
                    {
                        exceptionMessages.Add(string.Empty);
                    }
                }
            }
            if (null != tbhishomeRecords)
            {
                foreach (var item in tbhishomeRecords)
                {
                    if (null != item.CrawledDataContent)
                    {
                        tbHishomeContents.Add(Encoding.UTF8.GetString(item.CrawledDataContent));
                    }
                    else
                    {
                        tbHishomeContents.Add(string.Empty);
                    }
                }
            }
            if (null != tb_OperationRecords)
            {
                foreach (var item in tb_OperationRecords)
                {
                    if (null != item.OtherMessage)
                    {
                        operationLogMessages.Add(Encoding.UTF8.GetString(item.OtherMessage));
                    }
                    else
                    {
                        operationLogMessages.Add(string.Empty);
                    }
                }
            }
            return Forbid();
        }
        #endregion
    }
}