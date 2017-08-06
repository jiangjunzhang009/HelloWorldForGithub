using ReceviePcAutoUserCallback.Models;
using ReceviePcAutoUserCallback.Models.AbstractModel;
using ReceviePcAutoUserCallback.Models.DbModel;
using ReceviePcAutoUserCallback.Services.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReceviePcAutoUserCallback.Services
{
    public class ReceiveMessageService
    {
        #region Methods
        public IEnumerable<TestTb_ExceptionLog> GetTestExceptions(string strConn)
        {

            TestTb_ExceptionLogRepository exceptionRepository = new TestTb_ExceptionLogRepository(strConn);
            return exceptionRepository.GetAll();
        }
        public void TestUseVarbinaryType(TestTb_ExceptionLog exceptionLog, string strConn)
        {
            try
            {
                bool isConvertStringToHxen = true;
                if (isConvertStringToHxen && null != exceptionLog && null != exceptionLog.OtherMessage)
                {
                    //byte[] bytes = Encoding.UTF8.GetBytes(exceptionLog.OtherMessage);
                    //if (null != bytes)
                    //{
                    //    exceptionLog.OtherMessage = BitConverter.ToString(bytes);
                    //    exceptionLog.OtherMessage = "0x" + exceptionLog.OtherMessage.Replace("-", "");
                    //}
                    //exceptionLog.OtherMessage = TestHexdenConvert.ToHex(exceptionLog.OtherMessage, "utf-8", false);
                }
                TestTb_ExceptionLogRepository exceptionRepository = new TestTb_ExceptionLogRepository(strConn);
                exceptionRepository.Add(exceptionLog);
            }
            catch (Exception ex)
            {
                
            }
        }
        public void AddExceptionLog(Tb_ExceptionLogPcAuto exceptionLog, string strConn)
        {
            try
            {
                Tb_ExceptionLogPcAutoRepository exceptionRepository = new Tb_ExceptionLogPcAutoRepository(strConn);
                exceptionRepository.Add(exceptionLog);
            }
            catch (Exception ex)
            {
            }
        }
        public int AddOperationLog(Tb_OperationLogPcAuto operationLog, string strConn)
        {
            int result = -1;
            try
            {
                Tb_OperationLogPcAutoRepository operationRepository = new Tb_OperationLogPcAutoRepository(strConn);
                result = operationRepository.AddAndReturnId(operationLog);
            }
            catch (Exception ex)
            {
                result = -1;
            }
            return result;
        }
        public void AddPageResult(ReceiveResultTemplateModel pageResult, string tableName, string strConn)
        {
            try
            {
                ReceiveResultTemplateModelRepository pageResultRepository = new ReceiveResultTemplateModelRepository(strConn);
                pageResultRepository.Add(pageResult, tableName);
            }
            catch (Exception ex)
            {
            }
        }
        public string ChooseStoreTableName(string url)
        {
            string tableName = string.Empty;
            try
            {
                if (url.Contains("/car"))
                {
                    tableName = "tb_hiscar";
                }else if(url.Contains("/getQuestionForumByUid"))
                {
                    tableName = "tb_hiscarquestion";
                }
                else if (url.Contains("/getAnswersTopicByUid"))
                {
                    tableName = "tb_hiscaranswer";
                }
                else if (url.Contains("/my_comment_json"))
                {
                    tableName = "tb_hiscommentpost";
                }
                else if (url.Contains("/my_reply_json"))
                {
                    tableName = "tb_hiscommentreply";
                }
                else if (url.Contains("/uc_user_cmt"))
                {
                    tableName = "tb_hiscomment";
                }
                else if (url.Contains("=getFocusNum"))
                {
                    tableName = "tb_hishomefocusnumber";
                }
                else if (url.Contains("=getFocusByNum"))
                {
                    tableName = "tb_hishomefansnumber";
                }
                else if (url.Contains("/getCarAttr"))
                {
                    tableName = "tb_hishomelovecars";
                }
                else if (url.Contains("/user_setting_json"))
                {
                    tableName = "tb_hishomeusersetting";
                }
                else if (url.Contains("/myJoinGfClub"))
                {
                    tableName = "tb_hishomeofficalclub";
                }
                else if (url.Contains("/getJoinedClubListByUser"))
                {
                    tableName = "tb_hishomenormalclob";
                }
                //else if (url.Contains("/getJoinedClubListByUser"))
                //{
                //    tableName = "tb_hishomenormalclob";
                //}
                //else if (url.Contains("/getJoinedClubListByUser"))
                //{
                //    tableName = "tb_hishomenormalclob";
                //}
                //else if (url.Contains("/getJoinedClubListByUser"))
                //{
                //    tableName = "tb_hishomenormalclob";
                //}
                else
                {
                    //他的主页
                    tableName = "tb_hishome";
                }
            }
            catch (Exception ex)
            {
                tableName = null;
            }
            return tableName;
        }
        #endregion

        #region For testing
        public void RecordOperationMessage(string message, string connectionString)
        {
            try
            {
                ProductRepository productRepository = new ProductRepository(connectionString);
                Product singleProduct = new Product()
                {
                    Name = message,
                    Price = -9,
                    Quantity = -999
                };
                productRepository.Add(singleProduct);
            }
            catch (Exception ex)
            {

            }
        }
        public void TestVisitMysql(string connectionString)
        {
            try
            {
                ProductRepository productRepository = new ProductRepository(connectionString);
                Product singleProduct = new Product()
                {
                    Name = "product009",
                    Price = 9.99,
                    Quantity = 999
                };
                var result0 = productRepository.GetAll();
                productRepository.Add(singleProduct);
                singleProduct = productRepository.GetByPrice(singleProduct).FirstOrDefault();
                var result1 = productRepository.GetAll();
                singleProduct.Quantity += 1;
                productRepository.Update(singleProduct);
                productRepository.TransactionTest();
                var result2 = productRepository.GetAll();
                productRepository.Delete(1);
                var result4 = productRepository.GetAll();
            }
            catch (Exception ex)
            {
                
            }


            //MySqlConnection
            //MySqlConnection con = new MySqlConnection("server=127.0.0.1;database=test;uid=root;pwd=;charset='gbk';SslMode=None");
            //新增数据
            //con.Execute("insert into user values(null, '测试', 'http://www.cnblogs.com/linezero/', 18)");
            ////新增数据返回自增id
            //var id = con.QueryFirst<int>("insert into user values(null, 'linezero', 'http://www.cnblogs.com/linezero/', 18);select last_insert_id();");
            ////修改数据
            //con.Execute("update user set UserName = 'linezero123' where Id = @Id", new { Id = id });
            ////查询数据
            //var list = con.Query<User>("select * from user");
            //foreach (var item in list)
            //{
            //    Console.WriteLine($"用户名：{item.UserName} 链接：{item.Url}");
            //}
            ////删除数据
            //con.Execute("delete from user where Id = @Id", new { Id = id });
            //Console.WriteLine("删除数据后的结果");
            //list = con.Query<User>("select * from user");
            //foreach (var item in list)
            //{
            //    Console.WriteLine($"用户名：{item.UserName} 链接：{item.Url}");
            //}
        }



        //DALService dalService = new DALService();

        //public int SaveTb_ExceptionLog(Tb_ExceptionLog exceptionLog)
        //{
        //    return dalService.SaveTb_ExceptionLog(exceptionLog);
        //}

        //public int SaveTb_OperationLog(Tb_OperationLog operationLog)
        //{
        //    return dalService.SaveTb_OperationLog(operationLog);
        //}

        //public int SaveTb_RawRequest(Tb_RawRequest rawRequest)
        //{
        //    return dalService.SaveTb_RawRequest(rawRequest);
        //}
        #endregion
    }
}