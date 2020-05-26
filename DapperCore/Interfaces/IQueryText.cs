using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperCore.Interfaces
{
    public interface IQueryText
    {
         string GetAllProducts { get; }
         string GetProductById { get; }
         string AddProduct { get; }
         string UpdateProduct { get; }
         string DeleteProduct { get; }
    }

    public class QueryText : IQueryText
    {
        public string GetAllProducts => "Select * From Products";
        public string GetProductById => "Select * From Products Where Id=@Id";
        public string AddProduct => "Insert Into Products(Name,Cost,CreatedDate) Values (@Name,@Cost,@CreatedDate)";
        public string UpdateProduct => "Update Products Set Name=@Name,Cost=@Cost,CreatedDate=@CreatedDate Where Id = @Id";
        public string DeleteProduct => "Delete From Products Where Id=@Id";
    }
}
