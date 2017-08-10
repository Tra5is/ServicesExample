using Infrastructure;

namespace ProductEntity.Proxy
{
    [InProcServiceProxy]
    public class ProductServiceCommandProxy : IProductCommand
    {
        private readonly IProductService productService;

        public ProductServiceCommandProxy(IProductService productService)
        {
            this.productService = productService;
        }

        public int Create(string name)
        {
            return productService.Create(name);
        }
    }
}