using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class BaseRepository<T>
        where T: class
    {
        private string connectionString;
        public BaseRepository(string strConn)
        {
            if (string.IsNullOrWhiteSpace(strConn))
            {
                throw new InvalidOperationException("Please ensure the 'strConn' is valid.");
            }
            connectionString = strConn;
        }

        public IDbConnection Connection
        {
            get
            {
                //return new MySqlConnection(connectionString);
                return new SqlConnection(connectionString);
            }
        }

        public void Add(T bizObject, string sqlScript)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(sqlScript, bizObject);
            }
        }
        public int AddAndReturnId(T bizObject, string sqlScript)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.ExecuteScalar<int>(sqlScript, bizObject);
            }
        }

        public IEnumerable<T> GetAll(string sqlScript)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<T>(sqlScript);
            }
        }

        public T GetByID(int id, string sqlScript)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<T>(sqlScript, new { Id = id }).FirstOrDefault();
            }
        }

        public IEnumerable<T> GetByPrice(double price, string sqlScript)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<T>(sqlScript, new { Price=price});
            }
        }

        public void Delete(int id, string sqlScript)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(sqlScript, new { Id = id });
            }
        }

        public void Update(T bizObject, string sqlScript)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(sqlScript, bizObject);
            }
        }

        public void TransactionTest(string sqlScript)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    dbConnection.Execute(sqlScript);
                    ///to do throw exception
                    transaction.Commit();
                }
            }
        }
    }
}
