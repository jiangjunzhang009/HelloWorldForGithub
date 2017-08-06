using Dapper;
using MySql.Data.MySqlClient;
using ReceviePcAutoUserCallback.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ReceviePcAutoUserCallback.Services.DAL
{
    public class ProductRepository:BaseRepository<Product>
    {
        public ProductRepository(string strConn)
            :base(strConn)
        {
        }

        public void Add(Product prod)
        {
            string sqlScript = "INSERT INTO Products (Name, Quantity, Price)"
                            + " VALUES(@Name, @Quantity, @Price)";
            base.Add(prod, sqlScript);
        }

        public IEnumerable<Product> GetAll()
        {
            string sqlScript = "SELECT * FROM Products";
            return base.GetAll(sqlScript);
        }

        public Product GetByID(int id)
        {
            string sqlScript = "SELECT * FROM Products"
                           + " WHERE ProductId = @Id";
            return base.GetByID(id, sqlScript);
        }

        public IEnumerable<Product> GetByPrice(Product product)
        {
            string sqlScript = "SELECT * FROM Products WHERE Price = @Price";
            return base.GetByPrice(product.Price, sqlScript);
        }

        public void Delete(int id)
        {
            string sqlScript = "DELETE FROM Products"
                         + " WHERE ProductId = @Id";
            base.Delete(id, sqlScript);
        }

        public void Update(Product prod)
        {
            string sqlScript = "UPDATE Products SET Name = @Name,"
                           + " Quantity = @Quantity, Price= @Price"
                           + " WHERE ProductId = @ProductId";
            base.Update(prod, sqlScript);
        }

        public void TransactionTest()
        {
            string sqlScript = "UPDATE Products SET Name = 'xishuai222'"
                           + " WHERE ProductId = 1";
            base.TransactionTest(sqlScript);
        }
    }
}
