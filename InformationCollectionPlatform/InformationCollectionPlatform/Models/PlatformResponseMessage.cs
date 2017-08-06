using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace InformationCollectionPlatform.Models
{
    public class PlatformResponseMessage
    {

    }
    /// <summary>
    ///		Api请求的基础响应对象
    /// </summary>
    class BaseResponse
    {
        /// <summary>
        ///  获取或设置错误码
        /// </summary>
        [JsonProperty(PropertyName = "error-id")]
        public int ErrorId { get; set; }
        /// <summary>
        ///  获取或设置错误提示信息,如果操作成功，则不包含此字段
        /// </summary>
        [JsonProperty(PropertyName = "reason", NullValueHandling = NullValueHandling.Ignore)]
        public string Reason { get; set; }
    }
}