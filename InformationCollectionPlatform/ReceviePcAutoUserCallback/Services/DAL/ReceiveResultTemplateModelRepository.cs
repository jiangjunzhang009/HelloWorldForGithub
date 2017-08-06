using ReceviePcAutoUserCallback.Models.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class ReceiveResultTemplateModelRepository:BaseRepository<ReceiveResultTemplateModel>
    {
        public ReceiveResultTemplateModelRepository(string strConn)
            :base(strConn)
        { }

        public void Add(ReceiveResultTemplateModel pageResult, string tableName)
        {
            string sqlScript = $"INSERT INTO {tableName}(OperationId, TaskId, TaskGroupId, CompatibleTaskId, RuntimeRefUrl, RuntimeHttpMocker, RuntimeFirstPageUrl, PcAutoUserId, IsMultiPage, CrawledDataUrl, CrawledDataContent, CrawledDataTime, OtherMessage)"
                + "VALUES(@OperationId, @TaskId, @TaskGroupId, @CompatibleTaskId, @RuntimeRefUrl, @RuntimeHttpMocker, @RuntimeFirstPageUrl, @PcAutoUserId, @IsMultiPage, @CrawledDataUrl, @CrawledDataContent, @CrawledDataTime, @OtherMessage)";
            base.Add(pageResult, sqlScript);
        }
    }
}
