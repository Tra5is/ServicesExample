using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ProductEntity;
using ProductEntity.Model;

namespace ProductService
{
    public class ProductController : ApiController
    {
        private readonly IProductQuery productQuery;
        private readonly IProductCommand productCommand;

        public ProductController(IProductQuery productQuery, IProductCommand productCommand)
        {
            this.productQuery = productQuery;
            this.productCommand = productCommand;
        }

        // GET api/Product 
        public IEnumerable<string> Get()
        {
            return productQuery.GetAll().Select(p => p.ToView());
        }

        // GET api/Product/5 
        public string Get(int id)
        {
            var product = productQuery.Get(id);
            if (product == null)
                return "no product with that Id";

            return product.ToView();
        }

        // POST api/Product 
        public string Post([FromBody]string value)
        {
            return "New Product Id: " + productCommand.Create(value);
        }

        // PUT api/Product/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Product/5 
        public void Delete(int id)
        {
        }
    }

    public static class ProductExtensions
    {
        public static string ToView(this Product p)
        {
            return string.Format("{0}-{1}", p.Id, p.Name);
        }
    }
}