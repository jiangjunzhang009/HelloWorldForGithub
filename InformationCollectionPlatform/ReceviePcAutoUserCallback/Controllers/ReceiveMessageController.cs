using System;
using Microsoft.AspNetCore.Mvc;
using ReceviePcAutoUserCallback.Models;
using Microsoft.Extensions.Options;
using ReceviePcAutoUserCallback.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReceviePcAutoUserCallback.Services.DAL;
using ReceviePcAutoUserCallback.Models.DbModel;
using Gridsum.MediaD.NetCore.Common.Biz;

namespace ReceviePcAutoUserCallback.Controllers
{
    [Produces("application/json")]
    [Route("api/ReceiveMessagePcAuto")]
    public class ReceiveMessageController : Controller
    {
        #region Members
        ReceiveMessageService receiveMessageService = new ReceiveMessageService();
        private static CustomSetting _customSetting { get; set; }
        #endregion

        #region Methods
        public ReceiveMessageController(IOptions<CustomSetting> customSettings)
        {
            _customSetting = customSettings.Value;
        }
        [HttpPost]
        public JsonResult Post([FromBody]PageModule value)
        {
            BaseResponse baseResponse = new BaseResponse();
            baseResponse.ErrorId = 0;
            Tb_OperationLogPcAuto operationLog = new Tb_OperationLogPcAuto();
            Tb_ExceptionLogPcAuto exceptionLog = new Tb_ExceptionLogPcAuto();
            string message = "[PcAuto]--Start handle statictic.";
            try
            {
                operationLog.OperationBusinessTopic = "ReceiveMessage";
                operationLog.OperationLevel = "Info";
                operationLog.OperationMessage = $"Operation start at: {DateTime.UtcNow.ToString()}";
                //check the received message
                if (null == value)
                {
                    baseResponse.Reason = "[PcAuto]--PaAutoUserCenter Callback api received null from clawer platform.";
                    exceptionLog.LogLevel = "Warn";
                    exceptionLog.LogMessage = baseResponse.Reason;
                    receiveMessageService.AddExceptionLog(exceptionLog, _customSetting.MySqlConnection);
                    return Json(new { baseResponse});
                }
                operationLog.OtherMessage = Newtonsoft.Json.JsonConvert.SerializeObject(value);
                //record the operation
                int operationId = receiveMessageService.AddOperationLog(operationLog, _customSetting.MySqlConnection);
                if (1 > operationId)
                {
                    Task.Delay(250);
                    if (1 < receiveMessageService.AddOperationLog(operationLog, _customSetting.MySqlConnection))
                    {
                        baseResponse.Reason = "[PcAuto]--PaAutoUserCenter failed to store the received message.";
                        exceptionLog.LogLevel = "Warn";
                        exceptionLog.LogMessage = baseResponse.Reason;
                        receiveMessageService.AddExceptionLog(exceptionLog, _customSetting.MySqlConnection);
                    }
                }
                //judge whether the operation log store successful
                if (0 < operationId)
                {
                    Task sortingReceiveMessageTask = new Task(() =>
                    {
                        try
                        {
                            if (null == value.Payload)
                            {
                                return;
                            }
                            ReceiveResultTemplateModel pageResult = new ReceiveResultTemplateModel();
                            pageResult.OperationId = operationId;
                            if (null != value.Payload.BasicInformation)
                            {
                                pageResult.TaskId = value.Payload.BasicInformation.TaskID;
                                pageResult.TaskGroupId = value.Payload.BasicInformation.TaskGroupID;
                                pageResult.CompatibleTaskId = value.Payload.BasicInformation.CompatibleTaskID;
                            }
                            if (null != value.Payload.RuntimeStatus)
                            {
                                pageResult.RuntimeRefUrl = value.Payload.RuntimeStatus.ReferUrl.AbsoluteUri;
                                pageResult.RuntimeHttpMocker = Newtonsoft.Json.JsonConvert.SerializeObject(value.Payload.RuntimeStatus.HttpDataMocker);
                                pageResult.RuntimeFirstPageUrl = value.Payload.RuntimeStatus.FirstPageUrl.AbsoluteUri;
                            }
                            if (!string.IsNullOrWhiteSpace(pageResult.RuntimeFirstPageUrl) && pageResult.RuntimeFirstPageUrl.Contains(".pcauto.com.cn/"))
                            {
                                //Get the PcAutoUserId from url
                                List<string> findUserIdFormatInUrl = new List<string>()
                                { "uid=","userId=","accountId=","carId="};
                                string singleFindUserIdFormat = ".pcauto.com.cn/";
                                string strUserId = string.Empty;
                                try
                                {
                                    foreach (string findFormat in findUserIdFormatInUrl)
                                    {
                                        int locationIndex = pageResult.RuntimeFirstPageUrl.IndexOf(findFormat);
                                        if (0 < locationIndex)
                                        {
                                            int andCharIndex = pageResult.RuntimeFirstPageUrl.IndexOf("&", locationIndex + findFormat.Length);
                                            if (locationIndex < andCharIndex)
                                            {
                                                strUserId = pageResult.RuntimeFirstPageUrl.Substring(locationIndex + findFormat.Length, andCharIndex - locationIndex - findFormat.Length);
                                            }
                                            else
                                            {
                                                strUserId = pageResult.RuntimeFirstPageUrl.Substring(locationIndex + findFormat.Length);
                                            }
                                            break;
                                        }
                                    }
                                    if (string.IsNullOrWhiteSpace(strUserId))
                                    {
                                        int locationIndex = pageResult.RuntimeFirstPageUrl.IndexOf(singleFindUserIdFormat);
                                        if (0 < locationIndex)
                                        {
                                            int backSlashIndex = pageResult.RuntimeFirstPageUrl.IndexOf("/", locationIndex + singleFindUserIdFormat.Length);
                                            if (locationIndex < backSlashIndex)
                                            {
                                                strUserId = pageResult.RuntimeFirstPageUrl.Substring(locationIndex + singleFindUserIdFormat.Length, backSlashIndex - locationIndex - singleFindUserIdFormat.Length);
                                            }
                                            else
                                            {
                                                strUserId = pageResult.RuntimeFirstPageUrl.Substring(locationIndex + singleFindUserIdFormat.Length);
                                            }
                                        }
                                    }
                                    if (!string.IsNullOrWhiteSpace(strUserId))
                                    {
                                        int userId = -1;
                                        if (int.TryParse(strUserId, out userId))
                                        {
                                            pageResult.PcAutoUserId = userId.ToString();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    pageResult.PcAutoUserId = "-3";
                                }
                            }
                            //pageResult.TaskId = "9";
                            //pageResult.TaskGroupId = "99";
                            //pageResult.CompatibleTaskId = "999";
                            //pageResult.RuntimeRefUrl = "RuntimeRefUrl";
                            //pageResult.RuntimeHttpMocker = "RuntimeHttpMocker";
                            //pageResult.RuntimeFirstPageUrl = "RuntimeFirstPageUrl";
                            //pageResult.PcAutoUserId = "PcAutoUserId";
                            //pageResult.IsMultiPage = 9;
                            //pageResult.CrawledDataUrl = "CrawledDataUrl";
                            //pageResult.CrawledDataContent = "CrawledDataContent";
                            //pageResult.CrawledDataTime = "CrawledDataTime";
                            if (null != value.Data && 0 < value.Data.Length)
                            {
                                if (null != value.Data[0].Url)
                                {
                                    string storeTableName = receiveMessageService.ChooseStoreTableName(value.Data[0].Url.ToString());
                                    if (value.Data.Length == 1)
                                    {
                                        pageResult.IsMultiPage = 0;
                                        pageResult.CrawledDataUrl = value.Data[0].Url.ToString();
                                        pageResult.CrawledDataContent = value.Data[0].Content;
                                        pageResult.CrawledDataTime = value.Data[0].CrawlingTime.ToString();
                                        receiveMessageService.AddPageResult(pageResult, storeTableName, _customSetting.MySqlConnection);
                                    }
                                    else
                                    {
                                        pageResult.IsMultiPage = 1;
                                        foreach (CrawledData pageData in value.Data)
                                        {
                                            try
                                            {
                                                if (null == pageData.Url)
                                                {
                                                    continue;
                                                }
                                                //decide which store table that the infomation will go

                                                pageResult.CrawledDataUrl = pageData.Url.ToString();
                                                pageResult.CrawledDataContent = pageData.Content;
                                                pageResult.CrawledDataTime = pageData.CrawlingTime.ToString();
                                                receiveMessageService.AddPageResult(pageResult, storeTableName, _customSetting.MySqlConnection);
                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Tb_ExceptionLogPcAuto exceptionDataInvalid = new Tb_ExceptionLogPcAuto();
                                exceptionDataInvalid.LogLevel = "Fatal";
                                exceptionDataInvalid.LogMessage = "value.Data is null or value.Data.Length is 1.";
                                receiveMessageService.AddExceptionLog(exceptionLog, _customSetting.MySqlConnection);
                            }


                        }
                        catch (Exception ex)
                        {
                        }

                    });
                    sortingReceiveMessageTask.Start();
                }
            }
            catch (Exception ex)
            {
                message = "[PcAuto]--Some exception occured.";
                if (null != ex)
                {
                    message += $"\nException Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                }
                baseResponse.Reason = message;
                exceptionLog.LogLevel = "Error";
                exceptionLog.LogMessage = baseResponse.Reason;
                receiveMessageService.AddExceptionLog(exceptionLog, _customSetting.MySqlConnection);
            }
            return Json(new { baseResponse });
        }
        //[HttpPost]
        //public JsonResult Post([FromBody]JObject value)
        //{
        //    BaseResponse baseResponse = new BaseResponse();
        //    string message = "default009";
        //    try
        //    {
        //        if (null != value)
        //        {
        //            //message = $"value.Count: {value.Count}";
        //            message = "PcAuto====" + value.ToString();
        //        }
        //        else
        //        {
        //            message = "arguement value is null.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        message = "some exception occured.";
        //        if (null != ex)
        //        {
        //            message += $"====Message: {ex.Message}, StackTrace: {ex.StackTrace}";
        //        }
        //    }
        //    receiveMessageService.RecordOperationMessage(message, _customSetting.MySqlConnection);
        //    baseResponse.ErrorId = 0;
        //    return Json(new { baseResponse });
        //}
        [HttpGet]
        public IActionResult Get()
        {
            string strJsonFormat = "[\"http://my.pcauto.com.cn/{0}\",\"http://my.pcauto.com.cn/{0}/\",\"http://my.pcauto.com.cn/{0}/car\",\"http://bbs.pcauto.com.cn/intf/getQuestionForumByUid.do?uid={0}&isbest=0&status=0&pageNo=1&pageSize=1&callback=showReviews&resp_enc=gbk\",\"http://bbs.pcauto.com.cn/intf/getAnswersTopicByUid.do?uid={0}&status=0&pageNo=1&pageSize=1&callback=showReviews&resp_enc=gbk\",\"http://price.pcauto.com.cn/comment/interface/personalCenter/my_comment_json.jsp?accountId={0}&pageNo=1&size=1&callback=showReviews\",\"http://price.pcauto.com.cn/comment/interface/personalCenter/my_reply_json.jsp?accountId={0}&pageNo=1&size=1&callback=showReply\",\"http://cmt.pcauto.com.cn/intf/uc_user_cmt.jsp?userId={0}&webSite=pcauto&pageNo=4&refTimes=1&noUbb=false&callback=showCmt&_=1499762002544\",\"http://my.pcauto.com.cn/bip/intf/focus.jsp?act=getFocusNum&accountId={0}&callback=?'\",\"http://my.pcauto.com.cn/bip/intf/focus.jsp?act=getFocusByNum&accountId={0}&callback=?\",\"http://my.pcauto.com.cn/intf/getCarAttr.jsp?callback=?&act=getCarAttr&carId={0}\",\"http://bbs.pcauto.com.cn/action/user/user_setting_json.jsp?uid={0}&callback=?\",\"http://club.pcauto.com.cn/ucappapi/gf/1.0/myJoinGfClub/get.do?userId={0}&callback=?\",\"http://club.pcauto.com.cn/usercenter/1.0/club/getJoinedClubListByUser.do?uid={0}&callback=?\"]";
            var value = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(strJsonFormat);
            try
            {
                foreach (var url in value)
                {
                    ReceiveResultTemplateModel pageResult = new ReceiveResultTemplateModel();
                    pageResult.RuntimeFirstPageUrl = url;
                    if (!string.IsNullOrWhiteSpace(pageResult.RuntimeFirstPageUrl) && pageResult.RuntimeFirstPageUrl.Contains(".pcauto.com.cn/"))
                    {
                        List<string> findUserIdFormatInUrl = new List<string>()
                                { "uid=","userId=","accountId=","carId="};
                        string singleFindUserIdFormat = ".pcauto.com.cn/";
                        string strUserId = string.Empty;
                        try
                        {
                            foreach (string findFormat in findUserIdFormatInUrl)
                            {
                                int locationIndex = pageResult.RuntimeFirstPageUrl.IndexOf(findFormat);
                                if (0 < locationIndex)
                                {
                                    int andCharIndex = pageResult.RuntimeFirstPageUrl.IndexOf("&", locationIndex + findFormat.Length);
                                    if (locationIndex < andCharIndex)
                                    {
                                        strUserId = pageResult.RuntimeFirstPageUrl.Substring(locationIndex+findFormat.Length, andCharIndex - locationIndex-findFormat.Length);
                                    }
                                    else
                                    {
                                        strUserId = pageResult.RuntimeFirstPageUrl.Substring(locationIndex + findFormat.Length);
                                    }
                                    break;
                                }
                            }
                            if (string.IsNullOrWhiteSpace(strUserId))
                            {
                                int locationIndex = pageResult.RuntimeFirstPageUrl.IndexOf(singleFindUserIdFormat);
                                if (0 < locationIndex)
                                {
                                    int backSlashIndex = pageResult.RuntimeFirstPageUrl.IndexOf("/", locationIndex + singleFindUserIdFormat.Length);
                                    if (locationIndex < backSlashIndex)
                                    {
                                        strUserId = pageResult.RuntimeFirstPageUrl.Substring(locationIndex+ singleFindUserIdFormat.Length, backSlashIndex - locationIndex- singleFindUserIdFormat.Length);
                                    }
                                    else
                                    {
                                        strUserId = pageResult.RuntimeFirstPageUrl.Substring(locationIndex+ singleFindUserIdFormat.Length);
                                    }
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(strUserId))
                            {
                                int userId = -1;
                                if (int.TryParse(strUserId, out userId))
                                {
                                    pageResult.PcAutoUserId = userId.ToString();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            pageResult.PcAutoUserId = "-3";
                        }
                    }
                }
                //JObject experimentTry = JObject.Parse(strJsonFormat);
            }
            catch (Exception ex)
            {
                
            }
            BaseResponse baseResponse = new BaseResponse();
            Tb_OperationLogPcAuto operationLog = new Tb_OperationLogPcAuto();
            Tb_ExceptionLogPcAuto exceptionLog = new Tb_ExceptionLogPcAuto();
            string message = "[PcAuto]--Start handle statictic.";
            try
            {
                operationLog.OperationBusinessTopic = "ReceiveMessage";
                operationLog.OperationLevel = "Info";
                operationLog.OperationMessage = $"Operation start at: {DateTime.UtcNow.ToString()}";
                //check the received message
                if (null == value)
                {
                    baseResponse.Reason = "[PcAuto]--PaAutoUserCenter Callback api received null from clawer platform.";
                    exceptionLog.LogLevel = "Warn";
                    exceptionLog.LogMessage = baseResponse.Reason;
                    receiveMessageService.AddExceptionLog(exceptionLog, _customSetting.MySqlConnection);
                    return Json(new { baseResponse });
                }
                operationLog.OtherMessage = Newtonsoft.Json.JsonConvert.SerializeObject(value);
                //record the operation
                int operationId = receiveMessageService.AddOperationLog(operationLog, _customSetting.MySqlConnection);
                if (1 > operationId)
                {
                    Task.Delay(250);
                    if (1 < receiveMessageService.AddOperationLog(operationLog, _customSetting.MySqlConnection))
                    {
                        baseResponse.Reason = "[PcAuto]--PaAutoUserCenter failed to store the received message.";
                        exceptionLog.LogLevel = "Warn";
                        exceptionLog.LogMessage = baseResponse.Reason;
                        receiveMessageService.AddExceptionLog(exceptionLog, _customSetting.MySqlConnection);
                    }
                }
                if (0 < operationId)
                {
                    Task sortingReceiveMessageTask = new Task(() =>
                    {
                        ReceiveResultTemplateModel pageResult = new ReceiveResultTemplateModel();
                        pageResult.OperationId = 9;
                        pageResult.TaskId = "9";
                        //pageResult.TaskGroupId = "99";
                        pageResult.CompatibleTaskId = "999";
                        pageResult.RuntimeRefUrl = "RuntimeRefUrl";
                        pageResult.RuntimeHttpMocker = "RuntimeHttpMocker";
                        pageResult.RuntimeFirstPageUrl = "RuntimeFirstPageUrl";
                        pageResult.PcAutoUserId = "PcAutoUserId";
                        pageResult.IsMultiPage = 9;
                        pageResult.CrawledDataUrl = "CrawledDataUrl";
                        pageResult.CrawledDataContent = "CrawledDataContent";
                        pageResult.CrawledDataTime = "CrawledDataTime";
                        pageResult.OtherMessage = "OtherMessage";
                        receiveMessageService.AddPageResult(pageResult, "tb_hishome", _customSetting.MySqlConnection);

                    });
                    sortingReceiveMessageTask.Start();
                }
            }
            catch (Exception ex)
            {
                message = "[PcAuto]--Some exception occured.";
                if (null != ex)
                {
                    message += $"\nException Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                }
                baseResponse.Reason = message;
                exceptionLog.LogLevel = "Error";
                exceptionLog.LogMessage = baseResponse.Reason;
                receiveMessageService.AddExceptionLog(exceptionLog, _customSetting.MySqlConnection);
            }
            return Ok("Hi, I am feeling you right now.000");
        }
        #endregion

    }
}