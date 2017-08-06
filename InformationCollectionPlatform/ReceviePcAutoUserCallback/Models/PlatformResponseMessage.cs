using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Gridsum.MediaD.NetCore.Common.Biz;

namespace ReceviePcAutoUserCallback.Models
{
    public class PlatformResponseMessage
    {

    }
    /// <summary>
    ///		Api请求的基础响应对象
    /// </summary>
    public class BaseResponse
    {
        #region Members
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
        #endregion
    }
    /// <summary>
    /// 爬虫平台回调数据接口
    /// </summary>
    public class PageModule
    {
        #region Members
        /// <summary>
        /// 获取或设置已爬取数据的详细配置性信息
        /// </summary>
        [JsonProperty("payload")]
        public Payload Payload { get; set; }
        /// <summary>
        ///     获取或设置已经爬取下来的数据集合
        /// </summary>
        [JsonProperty("crawled-data")]
        public CrawledData[] Data { get; set; }

        #endregion
    }
}