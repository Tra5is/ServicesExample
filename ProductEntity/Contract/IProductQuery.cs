using System.Collections.Generic;
using ProductEntity.Model;

namespace ProductEntity
{
    public interface IProductQuery
    {
        Product Get(int id);
        IEnumerable<Product> GetAll();
    }
}