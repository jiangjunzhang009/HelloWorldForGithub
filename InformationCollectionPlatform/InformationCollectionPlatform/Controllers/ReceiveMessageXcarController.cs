using InformationCollectionPlatform.Models;
using InformationCollectionPlatform.Models.AbstractModel;
using InformationCollectionPlatform.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InformationCollectionPlatform.Controllers
{
    public class ReceiveMessageXcarController : ApiController
    {
        ReceiveMessageService service = new ReceiveMessageService();
        // GET api/ReceiveMessage
        public IEnumerable<string> Get()
        {
            return new string[] { "Get()"};
        }

        // GET api/ReceiveMessage/5
        public string Get(int id)
        {
            return "Get(int id)";
        }

        // POST api/ReceiveMessage
        public HttpResponseMessage Post([FromBody]JObject value)
        {   
            string responseMessage = string.Empty;
            try
            {
                Tb_OperationLogXcar operationLog = new Tb_OperationLogXcar();
                operationLog.OperationBusinessTopic = "ReceiveMessage";
                operationLog.OperationLevel = "Info";
                operationLog.OperationMessage = $"Operation start at: {DateTime.UtcNow.ToString()}";

                Tb_RawRequestXcar rawRequest = new Tb_RawRequestXcar();
                //if (string.IsNullOrWhiteSpace(value))
                //{
                //    try
                //    {
                //        value = this.Request.Content.ReadAsStringAsync().Result;
                //    }
                //    catch (Exception ex)
                //    {
                //        value = "Exception occured in value = this.Request.Content.ReadAsStringAsync().Result;";
                //    }
                //}
                if(null != value)
                //if (!string.IsNullOrWhiteSpace(value))
                {
                    string strJsonObject = value.ToString();
                    if (4000 < strJsonObject.Length)
                    {
                        rawRequest.RequestBody = strJsonObject.Substring(0, 3999);
                    }
                    else
                    {
                        rawRequest.RequestBody = strJsonObject;
                    }
                }
                else
                {
                    rawRequest.RequestBody = "value == null";
                }
                rawRequest.RequestHeaders = this.Request.Headers.ToString();
                rawRequest.RequestMethod = this.Request.Method.Method;
                // Web-hosting. Needs reference to System.Web.dll
                rawRequest.RequestUrl = "null";
                //if (this.Request.Properties.ContainsKey("MS_HttpContext"))
                //{
                //    try
                //    {
                //        var ctx = Request.Properties["MS_HttpContext"] as System.Web.HttpContextWrapper;
                //        if (ctx != null && ctx.Request != null && ctx.Request.UrlReferrer != null)
                //        {
                //            rawRequest.RequestUrl = ctx.Request.UrlReferrer.AbsoluteUri;
                //        }
                //    }
                //    catch (Exception innerException)
                //    {
                //    }
                //}
                service.SaveTb_RawRequest(rawRequest);

                operationLog.OperationMessage += $"====Operation finish at: {DateTime.UtcNow.ToString()}";
                service.SaveTb_OperationLog(operationLog);
            }
            catch (Exception ex)
            {
                responseMessage = $"[ReceiveMessageApi]Exception Occured in Post([FromBody]JObject value), Message: {ex.Message}, StackTrace: {ex.StackTrace}.";
                Tb_ExceptionLogXcar exceptionLog = new Tb_ExceptionLogXcar();
                exceptionLog.LogBusinessTopic = "ReceiveMessage";
                exceptionLog.LogLevel = "Error";
                exceptionLog.LogMessage = responseMessage;
                try
                {
                    service.SaveTb_ExceptionLog(exceptionLog);
                }
                catch (Exception innerEx)
                {
                    responseMessage += "Save exception message to database occured error too.";
                }
            }
            finally
            {
            }
            Dictionary<string, object> messageDict = new Dictionary<string, object>();
            messageDict.Add("error-id", 0);
            messageDict.Add("reason", responseMessage);
            responseMessage = Newtonsoft.Json.JsonConvert.SerializeObject(messageDict);
            return Request.CreateResponse<string>(HttpStatusCode.OK, responseMessage);
        }

        // PUT api/ReceiveMessage/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/ReceiveMessage/5
        public void Delete(int id)
        {
        }
    }
}
