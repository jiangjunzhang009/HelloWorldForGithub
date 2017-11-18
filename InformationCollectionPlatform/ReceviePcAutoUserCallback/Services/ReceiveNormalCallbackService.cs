using Gridsum.MediaD.NetCore.Common.Biz;
using ReceviePcAutoUserCallback.DAL;
using ReceviePcAutoUserCallback.Models;
using ReceviePcAutoUserCallback.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ.ConnectionString;
using Newtonsoft.Json.Linq;
using ReceviePcAutoUserCallback.Models.MediaDissectorModel;

namespace ReceviePcAutoUserCallback.Services
{
    public class ReceiveNormalCallbackService
    {
        #region Members
        private static Encoding globalEncoding = Encoding.UTF8;
        #endregion
        #region Methods
        public BaseResponse HandleReceivedMessage(string strConn, PageModule pageData)
        {
            BaseResponse baseResponse = new BaseResponse();
            Encoding encoding = Encoding.UTF8;
            Tb_OperationLog operationLog = new Tb_OperationLog();
            Tb_ExceptionLog exceptionLog = new Tb_ExceptionLog();
            string message = "[PcAutoUserCenter]--Start handle received message.";
            try
            {
                ///check the received message
                if (null == pageData)
                {
                    baseResponse.ErrorId = 40000;
                    baseResponse.Reason = "[PcAutoUserCenter]--ReceivedMessage Callback api received null from clawer platform.";
                    exceptionLog.LogLevel = "Warn";
                    exceptionLog.LogMessage = baseResponse.Reason;
                    CommonService.AddExceptionLog(strConn, globalEncoding, null, exceptionLog);
                    return baseResponse;
                }
                operationLog.LogLevel = "Info";
                operationLog.LogTopic = "HandleReceivedMessage()";
                operationLog.LogMessage = $"Operation start at: {DateTime.UtcNow.ToString()}";
                //string otherMessage = Newtonsoft.Json.JsonConvert.SerializeObject(pageData);
                string otherMessage = string.Empty;
                if (null != pageData.Data && 1 < pageData.Data.Length)
                {
                    otherMessage = Newtonsoft.Json.JsonConvert.SerializeObject(pageData.Data.LastOrDefault());
                }
                else
                {
                    otherMessage = Newtonsoft.Json.JsonConvert.SerializeObject(pageData);
                }
                //record the operation
                int operationId = CommonService.AddOperationLog(strConn, Encoding.UTF8, otherMessage, operationLog);
                //judge whether the operation log store successful
                if (0 < operationId)
                {
                    //check whether the received content is valid
                    if (null != pageData.Data && 0 < pageData.Data.Length && pageData.Data[0].Content.Contains("错误"))
                    {
                        //baseResponse.ErrorId = 40000;
                        baseResponse.Reason = "[PcAutoUserCenter]--ReceivedMessage Callback api received invalid content from clawer platform.";
                        exceptionLog.LogLevel = "Warn";
                        exceptionLog.LogMessage = $"operationId: {operationId}" + baseResponse.Reason;
                        CommonService.AddExceptionLog(strConn, globalEncoding, pageData.Data[0].Content, exceptionLog);
                        return baseResponse;
                    }
                    Task sortingReceiveMessageTask = new Task(() =>
                    {
                        try
                        {
                            if (null == pageData.Payload)
                            {
                                return;
                            }
                            TbUnknowTypeData pageResult = new TbUnknowTypeData();
                            pageResult.OperationId = operationId;
                            if (null != pageData.Payload.BasicInformation)
                            {
                                pageResult.TaskId = pageData.Payload.BasicInformation.TaskID;
                                pageResult.TaskGroupId = pageData.Payload.BasicInformation.TaskGroupID;
                                pageResult.CompatibleTaskId = pageData.Payload.BasicInformation.CompatibleTaskID;
                            }
                            if (null != pageData.Payload.RuntimeStatus)
                            {
                                pageResult.RuntimeRefUrl = pageData.Payload.RuntimeStatus.ReferUrl.AbsoluteUri;
                                pageResult.RuntimeHttpMocker = Newtonsoft.Json.JsonConvert.SerializeObject(pageData.Payload.RuntimeStatus.HttpDataMocker);
                                pageResult.RuntimeFirstPageUrl = pageData.Payload.RuntimeStatus.FirstPageUrl.AbsoluteUri;
                            }
                            //Get the PcAutoUserId from AppendedTags or url
                            if (null != pageData.Payload.RuntimeStatus.AppendedTags)
                            {
                                pageResult.PcAutoUserId = pageData.Payload.RuntimeStatus.AppendedTags.FirstOrDefault();
                            }
                            if (string.IsNullOrWhiteSpace(pageResult.PcAutoUserId))
                            {
                                pageResult.PcAutoUserId = GetUserIdFromUrl(pageResult.RuntimeFirstPageUrl);
                            }
                            if (null != pageData.Data && 0 < pageData.Data.Length)
                            {
                                if (null != pageData.Data[0].Url)
                                {
                                    TbUnknowTypeDataRepository repository = new TbUnknowTypeDataRepository(strConn);
                                    //decide which store table that the infomation will go
                                    string storeTableName = ChooseStoreTableName(pageData.Data[0].Url.ToString());
                                    if (pageData.Data.Length == 1)
                                    {
                                        pageResult.IsMultiPage = 0;
                                        pageResult.CrawledDataUrl = pageData.Data[0].Url.ToString();
                                        if (null != pageData.Data[0].Content)
                                        {
                                            pageResult.CrawledDataContent = globalEncoding.GetBytes(pageData.Data[0].Content);
                                        }
                                        pageResult.CrawledDataTime = pageData.Data[0].CrawlingTime.ToString();
                                        repository.AddRecord(storeTableName, pageResult);
                                    }
                                    else
                                    {
                                        pageResult.IsMultiPage = 1;
                                        foreach (CrawledData singleData in pageData.Data)
                                        {
                                            try
                                            {
                                                if (null == singleData.Url)
                                                {
                                                    continue;
                                                }
                                                pageResult.CrawledDataUrl = singleData.Url.ToString();
                                                //Try to get the page number
                                                int pageNoIndex = pageResult.CrawledDataUrl.IndexOf("pageNo=");
                                                if (-1 == pageNoIndex)
                                                {
                                                    pageResult.PageNo = 1;
                                                }
                                                else
                                                {
                                                    string strPageNo = pageResult.CrawledDataUrl.Substring(pageNoIndex+7);
                                                    int pageNo = 1;
                                                    if (int.TryParse(strPageNo, out pageNo))
                                                    {
                                                        pageResult.PageNo = pageNo;
                                                    }
                                                    else
                                                    {
                                                        pageResult.PageNo = -1;
                                                    }
                                                }
                                                if (null != pageData.Data[0].Content)
                                                {
                                                    pageResult.CrawledDataContent = globalEncoding.GetBytes(singleData.Content);
                                                }
                                                pageResult.CrawledDataTime = singleData.CrawlingTime.ToString();
                                                repository.AddRecord(storeTableName, pageResult);
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
                                Tb_ExceptionLog exceptionDataInvalid = new Tb_ExceptionLog();
                                exceptionDataInvalid.LogLevel = "Error";
                                exceptionDataInvalid.LogMessage = "pageData.Data is null or pageData.Data.Length is 0.";
                                CommonService.AddExceptionLog(strConn, globalEncoding, null, exceptionDataInvalid);
                            }


                        }
                        catch (Exception ex)
                        {
                        }

                    });
                    sortingReceiveMessageTask.Start();
                }
                else
                {
                    exceptionLog.LogLevel = "Info";
                    exceptionLog.LogMessage = "Failed to store record in AddOperationLog.";
                    CommonService.AddExceptionLog(strConn, globalEncoding, null, exceptionLog);
                }

            }
            catch (Exception ex)
            {
                message = "[PcAutoUserCenter]--Some exception occured.";
                if (null != ex)
                {
                    message += $"\nException Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                }
                baseResponse.ErrorId = 40000;
                baseResponse.Reason = message;
                exceptionLog.LogLevel = "Error";
                exceptionLog.LogMessage = baseResponse.Reason;
                CommonService.AddExceptionLog(strConn, globalEncoding, null, exceptionLog);
            }
            return baseResponse;
        }
        /// <summary>
        /// Get the PcAutoUserId from url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetUserIdFromUrl(string url)
        {
            string strUserId = string.Empty;
            if (!string.IsNullOrWhiteSpace(url) && url.Contains(".pcauto.com.cn/"))
            {
                List<string> findUserIdFormatInUrl = new List<string>()
                                { "uid=","userId=","accountId="};
                string singleFindUserIdFormat = ".pcauto.com.cn/";
                try
                {
                    foreach (string findFormat in findUserIdFormatInUrl)
                    {
                        int locationIndex = url.IndexOf(findFormat);
                        if (0 < locationIndex)
                        {
                            int andCharIndex = url.IndexOf("&", locationIndex + findFormat.Length);
                            if (locationIndex < andCharIndex)
                            {
                                strUserId = url.Substring(locationIndex + findFormat.Length, andCharIndex - locationIndex - findFormat.Length);
                            }
                            else
                            {
                                strUserId = url.Substring(locationIndex + findFormat.Length);
                            }
                            break;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(strUserId))
                    {
                        int locationIndex = url.IndexOf(singleFindUserIdFormat);
                        if (0 < locationIndex)
                        {
                            int backSlashIndex = url.IndexOf("/", locationIndex + singleFindUserIdFormat.Length);
                            if (locationIndex < backSlashIndex)
                            {
                                strUserId = url.Substring(locationIndex + singleFindUserIdFormat.Length, backSlashIndex - locationIndex - singleFindUserIdFormat.Length);
                            }
                            else
                            {
                                strUserId = url.Substring(locationIndex + singleFindUserIdFormat.Length);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    strUserId = "-3";
                }
            }
            return strUserId;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">url转换为小写</param>
        /// <returns></returns>
        private string ChooseStoreTableName(string url)
        {
            url = url.ToLower();
            string tableName = string.Empty;
            try
            {
                if (url.Contains("/car"))
                {
                    tableName = "tbhiscar";
                }
                else if (url.Contains("/getquestionforumbyuid"))
                {
                    tableName = "tbhiscarquestion";
                }
                else if (url.Contains("/getanswerstopicbyuid"))
                {
                    tableName = "tbhiscaranswer";
                }
                else if (url.Contains("/my_comment_json"))
                {
                    tableName = "tbhiscommentpost";
                }
                else if (url.Contains("/my_reply_json"))
                {
                    tableName = "tbhiscommentreply";
                }
                else if (url.Contains("/uc_user_cmt"))
                {
                    tableName = "tbhiscomment";
                }
                else if (url.Contains("=getfocusnum"))
                {
                    tableName = "tbfocusnumber";
                }
                else if (url.Contains("=getfocusbynum"))
                {
                    tableName = "tbfansnumber";
                }
                else if (url.Contains("/getcarattr"))
                {
                    tableName = "tblovecars";
                }
                else if (url.Contains("/user_setting_json"))
                {
                    tableName = "tbusersetting";
                }
                else if (url.Contains("/myjoingfclub"))
                {
                    tableName = "tbofficalclub";
                }
                else if (url.Contains("/getjoinedclublistbyuser"))
                {
                    tableName = "tbnormalclob";
                }
                else if (url.Contains("/album"))
                {
                    tableName = "tbalbum";
                }
                else if (url.Contains("/follow"))
                {
                    tableName = "tbhisfollow";
                }
                else if (url.Contains("/fan"))
                {
                    tableName = "tbhisfans";
                }
                else
                {
                    //他的主页
                    tableName = "tbhishome";
                }
            }
            catch (Exception ex)
            {
                tableName = "tbunknowtypedata";
            }
            return tableName;
        }
        public IEnumerable<Tb_OperationLog> GetTb_OperationLog(string strConn)
        {
            BaseRepository<Tb_OperationLog> repository = new BaseRepository<Tb_OperationLog>(strConn);
            string sqlScript = "select Id,LogLevel,LogTopic,LogMessage,CreateDatetime,UpdateDatetime,OtherMessage from Tb_OperationLog";
            return repository.GetAll(sqlScript);
        }
        public IEnumerable<Tb_ErrorCallbackLog> GetTb_ErrorCallbackLog(string strConn)
        {
            BaseRepository<Tb_ErrorCallbackLog> repository = new BaseRepository<Tb_ErrorCallbackLog>(strConn);
            string sqlScript = "select Id,LogLevel,LogTopic,LogMessage,CreateDatetime,UpdateDatetime,OtherMessage,TestTimeInterval from Tb_ErrorCallbackLog";
            return repository.GetAll(sqlScript);
        }
        public IEnumerable<Tb_ExceptionLog> GetTb_ExceptionLog(string strConn)
        {
            BaseRepository<Tb_ExceptionLog> repository = new BaseRepository<Tb_ExceptionLog>(strConn);
            string sqlScript = "select Id,LogLevel,LogTopic,LogMessage,CreateDatetime,UpdateDatetime,OtherMessage from Tb_ExceptionLog";
            return repository.GetAll(sqlScript);
        }
        public IEnumerable<TbhisHome> GetTbhisHome(string strConn, int startIndex, int recordsCount)
        {
            BaseRepository<TbhisHome> repository = new BaseRepository<TbhisHome>(strConn);
            string sqlScript = $"select OperationId,Id,TaskId,TaskGroupId,CompatibleTaskId,RuntimeRefUrl,RuntimeHttpMocker,RuntimeFirstPageUrl,PcAutoUserId,IsMultiPage,PageNo,CrawledDataUrl,CrawledDataContent,CrawledDataTime,CreateDatetime,UpdateDatetime,OtherMessage from tbhisHome limit {startIndex},{recordsCount}";
            return repository.GetAll(sqlScript);
        }
        public IEnumerable<piep_templates> Getpiep_templates(string strConn, string recordIds)
        {
            BaseRepository<piep_templates> repository = new BaseRepository<piep_templates>(strConn);
            string sqlScript = $"select id,domain,url_regex_rule,`name`,`desc`,appkey,template,create_by,create_at,modify_by,modify_at,enable,status from piep_templates WHERE id IN ({recordIds})";
            return repository.GetAll(sqlScript);
        }
        public void Addpiep_templates(string strConn, piep_templates record)
        {
            BaseRepository<piep_templates> repository = new BaseRepository<piep_templates>(strConn);
            string sqlScript = $"INSERT INTO piep_templates(domain,url_regex_rule,`name`,`desc`,appkey,template,create_by,create_at,modify_by,modify_at,enable,status) " +
                $"VALUES(@domain,@url_regex_rule,@name,@desc,@appkey,@template,@create_by,@create_at,@modify_by,@modify_at,@enable,@status)";
            
            repository.Add(record, sqlScript);
        }
        #endregion
    }
}
