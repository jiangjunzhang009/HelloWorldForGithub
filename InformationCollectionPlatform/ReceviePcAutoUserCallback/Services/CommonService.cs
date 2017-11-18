using Newtonsoft.Json.Linq;
using ReceviePcAutoUserCallback.DAL;
using ReceviePcAutoUserCallback.Models;
using ReceviePcAutoUserCallback.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services
{
    public class CommonService
    {
        public static string GetCommonLogInsertScript(string tableName)
        {
            return $"INSERT INTO {tableName}(Id,LogLevel,LogTopic,LogMessage,OtherMessage) VALUES(@Id,@LogLevel,@LogTopic,@LogMessage,@OtherMessage)";
        }
        public static void AddExceptionLog(string strConn, Encoding encoding, string otherMessgae, Tb_ExceptionLog log)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(strConn))
                {
                    if (!string.IsNullOrWhiteSpace(otherMessgae))
                    {
                        log.OtherMessage = encoding.GetBytes(otherMessgae);
                    }
                    BaseRepository<Tb_ExceptionLog> exceptionRepository = new BaseRepository<Tb_ExceptionLog>(strConn);
                    exceptionRepository.Add(log, GetCommonLogInsertScript("Tb_ExceptionLog"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int AddOperationLog(string strConn, Encoding encoding, string otherMessgae, Tb_OperationLog log)
        {
            int result = -1;
            if (!string.IsNullOrWhiteSpace(strConn))
            {
                if (!string.IsNullOrWhiteSpace(otherMessgae))
                {
                    log.OtherMessage = encoding.GetBytes(otherMessgae);
                }
                BaseRepository<Tb_OperationLog> repository = new BaseRepository<Tb_OperationLog>(strConn);
                string sqlScript = GetCommonLogInsertScript("Tb_OperationLog") + ";SELECT LAST_INSERT_ID();";
                result = repository.AddAndReturnId(log, sqlScript);
            }
            return result;
        }
        public static void AddErrorCallbackLog(string strConn, Encoding encoding, string otherMessgae, Tb_ErrorCallbackLog log)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(strConn))
                {
                    if (!string.IsNullOrWhiteSpace(otherMessgae))
                    {
                        log.OtherMessage = encoding.GetBytes(otherMessgae);
                    }
                    BaseRepository<Tb_ErrorCallbackLog> errorcallbackRepository = new BaseRepository<Tb_ErrorCallbackLog>(strConn);
                    errorcallbackRepository.Add(log, GetCommonLogInsertScript("Tb_ErrorCallbackLog"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<string> ExtractTemporaryFields(JObject value)
        {
            List<string> results = new List<string>();
            string temporaryFieldsValue = string.Empty;
            string companyGuid = string.Empty;
            string taskType = string.Empty;
            try
            {
                JProperty payLoad = value.Property("payload");
                if (null != payLoad)
                {
                    JProperty temporaryFields = (payLoad.Value as JObject).Property("temporary-fields");
                    if (null != temporaryFields)
                    {
                        JObject fields = temporaryFields.Value as JObject;
                        JProperty companyGuidProp = fields.Property("company-guid");
                        if (null != companyGuidProp)
                        {
                            companyGuid = companyGuidProp.Value.ToString();
                        }
                        JProperty taskTypeProp = fields.Property("column-name");
                        if (null != taskTypeProp)
                        {
                            taskType = taskTypeProp.Value.ToString();
                        }
                        temporaryFieldsValue = $"{companyGuid},{taskType}";
                    }
                    else
                    {
                        temporaryFieldsValue = "temporary-fields is null";
                    }
                }
                else
                {
                    temporaryFieldsValue = "payLoad is null.";
                }
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    temporaryFieldsValue = $"Exception:{ex.Message}";
                }
                else
                {
                    temporaryFieldsValue = "Exception:null";
                }
            }
            if (70 < temporaryFieldsValue.Length)
            {
                temporaryFieldsValue = temporaryFieldsValue.Substring(0, 70);
            }
            results.Add(temporaryFieldsValue);
            results.Add(companyGuid);
            results.Add(taskType);
            return results;
        }

        public static List<string> ExtractTemporaryFields(PageModule pageData)
        {
            List<string> results = new List<string>();
            string temporaryFieldsValue = string.Empty;
            string companyGuid = string.Empty;
            string taskType = string.Empty;
            try
            {
                if (null != pageData && null != pageData.Payload)
                {
                    if (null != pageData.Payload.TemporaryFields)
                    {
                        JObject fields = pageData.Payload.TemporaryFields;
                        JProperty companyGuidProp = fields.Property("company-guid");
                        if (null != companyGuidProp)
                        {
                            companyGuid = companyGuidProp.Value.ToString();
                        }
                        JProperty taskTypeProp = fields.Property("column-name");
                        if (null != taskTypeProp)
                        {
                            taskType = taskTypeProp.Value.ToString();
                        }
                        temporaryFieldsValue = $"{companyGuid},{taskType}";
                    }
                    else
                    {
                        temporaryFieldsValue = "temporary-fields is null";
                    }
                }
                else
                {
                    temporaryFieldsValue = "payLoad is null.";
                }
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    temporaryFieldsValue = $"Exception:{ex.Message}";
                }
                else
                {
                    temporaryFieldsValue = "Exception:null";
                }
            }
            if (70 < temporaryFieldsValue.Length)
            {
                temporaryFieldsValue = temporaryFieldsValue.Substring(0, 70);
            }
            results.Add(temporaryFieldsValue);
            results.Add(companyGuid);
            results.Add(taskType);
            return results;
        }
    }
}
