using Dapper;
using DapperCore.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DapperCore.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly IQueryText _query;
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration, IQueryText query)
        {
            _query = query;
            _connectionString = configuration.GetConnectionString("Dapper");
        }
        public List<Product> GetAllProducts()
        {
            var query = ExecuteCommand(_connectionString, conn => conn.Query<Product>(_query.GetAllProducts)).ToList();

            return query;

        }

        public Product GetProductById(int id)
        {
            var query = ExecuteCommand(_connectionString,
                conn => conn.QuerySingleOrDefault<Product>(_query.GetProductById, new { Id = id }));
            return query;
        }

        public void AddProduct(Product product)
        {
            ExecuteCommand(_connectionString,
                conn => conn.Query(_query.AddProduct,
                    new { Name = product.Name, Cost = product.Cost, CreatedDate = product.CreatedDate }));
        }

        public void UpdateProduct(Product product)
        {
            ExecuteCommand(_connectionString,
                conn => conn.Query(_query.UpdateProduct,
                    new { Name = product.Name, Cost = product.Cost, CreatedDate = product.CreatedDate, Id = product.Id }));
        }

        public void DeleteProduct(int id)
        {
            ExecuteCommand(_connectionString, conn => conn.Query(_query.DeleteProduct, new { Id = id }));
        }


        #region Helpers

        private void ExecuteCommand(string connectionString, Action<SqlConnection> task)
        {
            using var conn = new SqlConnection(connectionString);

            conn.Open();

            task(conn);
        }

        private T ExecuteCommand<T>(string connectionString, Func<SqlConnection, T> task)
        {
            using var conn = new SqlConnection(connectionString);

            conn.Open();

            return task(conn);
        }

        #endregion
    }
}
