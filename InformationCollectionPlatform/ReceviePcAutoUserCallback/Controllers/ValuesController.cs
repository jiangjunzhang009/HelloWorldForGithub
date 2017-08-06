using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReceviePcAutoUserCallback.Models;
using Microsoft.Extensions.Options;
using System.Text;

namespace ReceviePcAutoUserCallback.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static CustomSetting _customSetting { get; set; }
        public ValuesController(IOptions<CustomSetting> customSettings)
        {
            _customSetting = customSettings.Value;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Services.ReceiveMessageService service = new Services.ReceiveMessageService();
            //service.TestVisitMysql(_customSetting.MySqlConnection);
            TestTb_ExceptionLog exceptionLog = new TestTb_ExceptionLog();
            exceptionLog.LogLevel = "LogLevel009";
            exceptionLog.LogMessage = "LogMessage009";
            exceptionLog.LogTopic = "LogTopic009";
            exceptionLog.OtherMessage = Encoding.UTF8.GetBytes("A lot of message...................胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀" +
                "胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀" +
                "胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀" +
                "胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀" +
                "胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀" +
                "胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀" +
                "胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀" +
                "胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀胜多负少撒法大使啊手动阀");
            service.TestUseVarbinaryType(exceptionLog, _customSetting.SqlServerConnection);
            var resultList = service.GetTestExceptions(_customSetting.SqlServerConnection);
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
