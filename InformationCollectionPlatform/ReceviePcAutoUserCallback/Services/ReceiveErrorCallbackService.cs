using Newtonsoft.Json.Linq;
using ReceviePcAutoUserCallback.Models;
using ReceviePcAutoUserCallback.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services
{
    public class ReceiveErrorCallbackService
    {
        #region Methods
        public BaseResponse AddErrorCallbackLog(string strConn, Encoding encoding, JObject value)
        {
            BaseResponse baseResponse = new BaseResponse();
            try
            {
                if (!string.IsNullOrWhiteSpace(strConn))
                {
                    //get the temporary fields
                    List<string> extractRresults = CommonService.ExtractTemporaryFields(value);
                    //record received data
                    Tb_ErrorCallbackLog log = new Tb_ErrorCallbackLog();
                    log.LogLevel = "Error";
                    log.LogTopic = "AddErrorCallbackLog()";
                    log.LogMessage = $"Received crawled platform error info callback.=={extractRresults.ElementAt(0)}";
                    if (null != value)
                    {
                        string message = Newtonsoft.Json.JsonConvert.SerializeObject(value);
                        log.OtherMessage = encoding.GetBytes(message);
                    }
                    CommonService.AddErrorCallbackLog(strConn, encoding, null, log);

                }
            }
            catch (Exception ex)
            {
                string exceptionMsg = "[PcAutoUserCenter]--Some exception occured in AddErrorCallbackLog()";
                if (null != ex)
                {
                    exceptionMsg += $"\nException Message: {ex.Message}, StackTrace: {ex.StackTrace}";
                }
                baseResponse.Reason = exceptionMsg;
            }
            return baseResponse;
        }
        #endregion
    }
}
