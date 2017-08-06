using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ReceviePcAutoUserCallback.Models.AbstractModel;

namespace ReceviePcAutoUserCallback.Services
{
    public class DALService
    {
        #region methods for ClassicalOrientationTraining
        //private static string connectionString { get; set; }
        //public static string StrDbConn
        //{
        //    get
        //    {
        //        if (string.IsNullOrWhiteSpace(connectionString))
        //        {
        //            connectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
        //        }
        //        return connectionString;
        //    }
        //}
        //public int GetAdminUserId(string name, string word)
        //{
        //    int result = -2;
        //    try
        //    {
        //        string sqlScript = string.Format("SELECT ID FROM [dbo].[AdminUser] WHERE [UserName] = N'{0}' AND [Password] = N'{1}'", name, word);
        //        SqlHelper sqlHelper = new SqlHelper(StrDbConn);
        //        result = sqlHelper.ExecuteScalar<int>(sqlScript);
        //    }
        //    catch (Exception ex)
        //    {
        //        result = -3;
        //    }
        //    return result;
        //}

        //public int CheckBatchCodeExists(string code)
        //{
        //    int result = -1;
        //    try
        //    {
        //        string sqlScript = string.Format("SELECT [ID] FROM [dbo].[TrainingBatch] WHERE [ProgressStatus] = 1 AND [Code] = N'{0}'", code);
        //        SqlHelper sqlHelper = new Service.SqlHelper(StrDbConn);
        //        result = sqlHelper.ExecuteScalar<int>(sqlScript);
        //    }
        //    catch (Exception ex)
        //    {
        //        result = -2;
        //    }
        //    return result;
        //}
        //public List<int> GetLatestFinishedQuestionnaireStageAndUserId(string name, int existsBatchId)
        //{
        //    List<int> result = new List<int>() { -1,-1};
        //    try
        //    {
        //        string selectUserIdsqlScript = string.Format("SELECT [ID] FROM [dbo].[PublicUser] WHERE [Name] = N'{0}'", name);
        //        SqlHelper sqlHelper = new Service.SqlHelper(StrDbConn);
        //        int userId = sqlHelper.ExecuteScalar<int>(selectUserIdsqlScript);
        //        if (0 < userId)
        //        {
        //            string sqlScript = string.Format("SELECT [LatestFinishedQuestionnaireStage] FROM [dbo].BatchAndPublicUser WHERE [BatchID] = {0} AND [UserID] = {1}", existsBatchId, userId);
        //            int stage = sqlHelper.ExecuteScalar<int>(sqlScript);
        //            if (0 < stage)
        //            {
        //                result.Clear();
        //                result.Add(userId);
        //                result.Add(stage);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = new List<int>() { -1, -1 };
        //    }
        //    return result;
        //}
        //public int GetLatestFinishedQuestionnaireStage(int publicUserId, int existsBatchId)
        //{
        //    int result = -1;
        //    try
        //    {
        //        string sqlScript = string.Format("SELECT [LatestFinishedQuestionnaireStage] FROM [dbo].BatchAndPublicUser WHERE [BatchID] = {0} AND [UserID] = {1}", existsBatchId, publicUserId);
        //        SqlHelper sqlHelper = new Service.SqlHelper(StrDbConn);
        //        result = sqlHelper.ExecuteScalar<int>(sqlScript);
        //    }
        //    catch (Exception ex)
        //    {
        //        result = -1;
        //    }
        //    return result;
        //}

        //public int GetUserIdAndSavePublicUserInfo(Table_PublicUser user)
        //{
        //    return new SqlHelper(StrDbConn).ExecProc_CreatePublicUserAndBatchUserLink(user);
        //}
        //public int SaveQuestionnaireTwo(int publicUserId, string trainerIntentaion, int batchId)
        //{
        //    int result = -2;
        //    try
        //    {
        //        if (1 > publicUserId)
        //        {
        //            string strUserId = CookieStorage.GetCookie(CookieKey.Front_NewJoinPublicUserId);
        //            if (!string.IsNullOrWhiteSpace(strUserId))
        //            {
        //                publicUserId = int.Parse(strUserId);
        //            }
        //        }
        //        string[] trainerIntends = trainerIntentaion.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries);
        //        if (0 < publicUserId && 6 == trainerIntends.Length)
        //        {
        //            string sqlScript = string.Format("INSERT INTO [dbo].[QuestionnaireTwo](UserID,Q1,Q2,Q3,Q4,Q5,Q6) VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6});UPDATE [BatchAndPublicUser] SET [LatestFinishedQuestionnaireStage] = [LatestFinishedQuestionnaireStage] + 1 WHERE [BatchID] = {7} AND [UserID] = {8};", publicUserId, trainerIntends[0], trainerIntends[1], trainerIntends[2], trainerIntends[3], trainerIntends[4], trainerIntends[5], batchId, publicUserId);
        //            SqlHelper sqlHelper = new SqlHelper(StrDbConn);
        //            result = sqlHelper.ExecuteNonQuery(sqlScript);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = -3;
        //    }
        //    return result;
        //}
        //public int SaveQuestionnaireThree(int publicUserId, string elevenScores, int batchId)
        //{
        //    int result = -2;
        //    try
        //    {
        //        if (1 > publicUserId)
        //        {
        //            string strUserId = CookieStorage.GetCookie(CookieKey.Front_NewJoinPublicUserId);
        //            if (!string.IsNullOrWhiteSpace(strUserId))
        //            {
        //                publicUserId = int.Parse(strUserId);
        //            }
        //        }
        //        string[] scores = elevenScores.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries);
        //        if (0 < publicUserId && 11 == scores.Length)
        //        {
        //            string sqlScript = string.Format("INSERT INTO [dbo].[QuestionnaireThree](UserID,Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,Q9,Q10,Q11) VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11});UPDATE [BatchAndPublicUser] SET [LatestFinishedQuestionnaireStage] = [LatestFinishedQuestionnaireStage] + 1 WHERE [BatchID] = {12} AND [UserID] = {13};", publicUserId, scores[0], scores[1], scores[2], scores[3], scores[4], scores[5], scores[6], scores[7], scores[8], scores[9], scores[10], batchId, publicUserId);
        //            SqlHelper sqlHelper = new SqlHelper(StrDbConn);
        //            result = sqlHelper.ExecuteNonQuery(sqlScript);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = -3;
        //    }
        //    return result;
        //}
        //public string[] GetQuestionnaireTwoSumScoreByBatchId(int batchId)
        //{
        //    return new SqlHelper(StrDbConn).ExecProc_GetQuestionnaireTwoSumScoreByBatchId(batchId);
        //}

        //public string[] GetQuestionnaireThreeAvgScoreByBatchId(int batchId)
        //{
        //    return new SqlHelper(StrDbConn).ExecProc_GetQuestionnaireThreeAvgScoreByBatchId(batchId);
        //}

        //public DataTable GetStaffNamesAndPropertyIds(int batchId, string fieldName)
        //{
        //    DataTable result = new DataTable();
        //    try
        //    {
        //        string sqlScript = string.Format("SELECT [Name],{0} FROM [dbo].[PublicUser] WHERE ID IN( SELECT [UserID] FROM [dbo].[BatchAndPublicUser] WHERE [BatchID] = {1})", fieldName, batchId);
        //        SqlHelper sqlHelper = new SqlHelper(StrDbConn);
        //        result = sqlHelper.ExecuteDataTable(sqlScript);
        //    }
        //    catch (Exception ex)
        //    {
        //        result = null;
        //    }
        //    return result;
        //}

        //public List<string> GetStaffPropertyNamesAndCount(int batchId, string fieldName)
        //{
        //    List<string> result = new List<string>();
        //    try
        //    {
        //        string sqlScript = string.Format("SELECT {0} FROM [dbo].[PublicUser] WHERE ID IN( SELECT [UserID] FROM [dbo].[BatchAndPublicUser] WHERE [BatchID] = {1})", fieldName, batchId);
        //        SqlHelper sqlHelper = new SqlHelper(StrDbConn);
        //        DataTable dataTable = sqlHelper.ExecuteDataTable(sqlScript);
        //        if (null != dataTable && 0 < dataTable.Rows.Count)
        //        {
        //            if ("[ProjectTeamProperty]" != fieldName)
        //            {
        //                foreach (DataRow row in dataTable.Rows)
        //                {
        //                    string propertyValue = row[0].ToString();
        //                    if (!string.IsNullOrWhiteSpace(propertyValue))
        //                    {
        //                        result.Add(propertyValue);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                foreach (DataRow row in dataTable.Rows)
        //                {
        //                    //!!!!筛选掉系统自用的标志，不展现给普通用户
        //                    string propertyValue = row[0].ToString();
        //                    if ("-1" != propertyValue)
        //                    {
        //                        result.Add(propertyValue);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = null;
        //    }
        //    return result;
        //}
        #endregion

        #region methods for ReceviePcAutoUserCallback
        private static string connectionString { get; set; }
        public static string StrDbConn
        {
            get
            {
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    //connectionString = ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
                }
                return connectionString;
            }
        }

        public int SaveTb_ExceptionLog(Tb_ExceptionLog exceptionLog)
        {
            int result = -2;
            try
            {
                string tableName = exceptionLog.GetType().FullName.Split(new char[] { '.' }).Last() + exceptionLog.LogBizType;
                //string sqlScript = $"INSERT INTO [{tableName}](LogLevel,LogMessage,LogBusinessTopic,OtherMessage)" +
                //    $"VALUES(N'{exceptionLog.LogLevel}',N'{exceptionLog.LogMessage}',N'{exceptionLog.LogBusinessTopic}',N'{exceptionLog.OtherMessage}')";
                string sqlScript = $"INSERT INTO [{tableName}](LogLevel,LogMessage,LogBusinessTopic,OtherMessage)" +
                    $"VALUES(@LogLevel,@LogMessage,@LogBusinessTopic,@OtherMessage)";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@LogLevel", SqlDbType.NVarChar, 20),
                    new SqlParameter("@LogMessage", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@LogBusinessTopic", SqlDbType.NVarChar, 200),
                    new SqlParameter("@OtherMessage", SqlDbType.NVarChar, 2000)
                };
                sqlParameters[0].SqlValue = exceptionLog.LogLevel;
                sqlParameters[1].SqlValue = exceptionLog.LogMessage;
                sqlParameters[2].SqlValue = exceptionLog.LogBusinessTopic;
                sqlParameters[3].SqlValue = exceptionLog.OtherMessage;
                SqlHelper sqlHelper = new SqlHelper(StrDbConn);
                result = sqlHelper.ExecuteNonQuery(sqlScript, sqlParameters);
            }
            catch (Exception ex)
            {
                result = -3;
                throw;
            }
            return result;
        }

        public int SaveTb_OperationLog(Tb_OperationLog operationLog)
        {
            int result = -2;
            try
            {
                string tableName = operationLog.GetType().FullName.Split(new char[] { '.' }).Last() + operationLog.LogBizType;
                //string sqlScript = $"INSERT INTO [{tableName}](OperationLevel,OperationMessage,OperationBusinessTopic,OtherMessage)" +
                //    $"VALUES(N'{operationLog.OperationLevel}',N'{operationLog.OperationMessage}',N'{operationLog.OperationBusinessTopic}',N'{operationLog.OtherMessage}')";
                string sqlScript = $"INSERT INTO [{tableName}](OperationLevel,OperationMessage,OperationBusinessTopic,OtherMessage)" +
                    $"VALUES(@OperationLevel,@OperationMessage,@OperationBusinessTopic,@OtherMessage)";
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                    new SqlParameter("@OperationLevel", SqlDbType.NVarChar, 20),
                    new SqlParameter("@OperationMessage", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@OperationBusinessTopic", SqlDbType.NVarChar, 200),
                    new SqlParameter("@OtherMessage", SqlDbType.NVarChar, 2000)
                };
                sqlParameters[0].SqlValue = operationLog.OperationLevel;
                sqlParameters[1].SqlValue = operationLog.OperationMessage;
                sqlParameters[2].SqlValue = operationLog.OperationBusinessTopic;
                sqlParameters[3].SqlValue = operationLog.OtherMessage;
                SqlHelper sqlHelper = new SqlHelper(StrDbConn);
                result = sqlHelper.ExecuteNonQuery(sqlScript, sqlParameters);
            }
            catch (Exception ex)
            {
                result = -3;
                throw;
            }
            return result;
        }

        public int SaveTb_RawRequest(Tb_RawRequest rawRequest)
        {
            int result = -2;
            try
            {
                string tableName = rawRequest.GetType().FullName.Split(new char[] { '.' }).Last() + rawRequest.LogBizType;
                //string sqlScript = $"INSERT INTO [{tableName}](RequestUrl,RequestHeaders,RequestMethod,RequestBody,OtherMessage)" +
                //    $"VALUES(N'{rawRequest.RequestUrl}',N'{rawRequest.RequestHeaders}','{rawRequest.RequestMethod}',N'{rawRequest.RequestBody}',N'{rawRequest.OtherMessage}')";
                string sqlScript = $"INSERT INTO [{tableName}](RequestUrl,RequestHeaders,RequestMethod,RequestBody,OtherMessage)" +
                    $"VALUES(@RequestUrl,@RequestHeaders,@RequestMethod,@RequestBody,@OtherMessage)";
                SqlParameter[] sqlParameters = new SqlParameter[] 
                {
                    new SqlParameter("@RequestUrl", SqlDbType.NVarChar, 2000),
                    new SqlParameter("@RequestHeaders", SqlDbType.NVarChar, 2000),
                    new SqlParameter("@RequestMethod", SqlDbType.VarChar, 10),
                    new SqlParameter("@RequestBody", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@OtherMessage", SqlDbType.NVarChar, 2000)
                };
                sqlParameters[0].SqlValue = rawRequest.RequestUrl;
                sqlParameters[1].SqlValue = rawRequest.RequestHeaders;
                sqlParameters[2].SqlValue = rawRequest.RequestMethod;
                sqlParameters[3].SqlValue = rawRequest.RequestBody;
                sqlParameters[4].SqlValue = rawRequest.OtherMessage;
                SqlHelper sqlHelper = new SqlHelper(StrDbConn);
                result = sqlHelper.ExecuteNonQuery(sqlScript, sqlParameters);
            }
            catch (Exception ex)
            {
                result = -3;
                throw;
            }
            return result;
        }
        #endregion
    }
}
