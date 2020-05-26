using DapperCore.Interfaces;
using DapperCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DapperCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet]
        public ActionResult<List<Product>> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<Product> GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }


        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            _productRepository.AddProduct(product);

            //return Ok(new {message = "Added Successfully", Product = product});

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut]
        public ActionResult UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);

            return Ok(new { Message = "Updated Successfully", Product = product });
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);

            return Ok(new { Message = "Deleted Successfully" });
        }


    }
}
