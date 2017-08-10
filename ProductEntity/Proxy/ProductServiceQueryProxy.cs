using System.Collections.Generic;
using Infrastructure;
using ProductEntity.Model;

namespace ProductEntity.Proxy
{
    [InProcServiceProxy]
    public class ProductServiceQueryProxy : IProductQuery
    {
        private readonly IProductService productService;

        public ProductServiceQueryProxy(IProductService service)
        {
            this.productService = service;
        }

        public Product Get(int id)
        {
            return productService.Get(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return productService.GetAll();
        }
    }
}