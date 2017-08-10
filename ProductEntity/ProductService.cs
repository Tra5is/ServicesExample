using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using Infrastructure;
using ProductEntity.Model;

namespace ProductEntity
{
    public interface IProductService : IProductQuery, IProductCommand
    { }

    [InProcService]
    internal class ProductService : IProductService, IDisposable
    {
        private readonly MemcachedClientConfiguration cacheConfig;
        private readonly MemcachedClient client;

        public ProductService(ProductEntityServiceConfiguration config)
        {
            cacheConfig = new MemcachedClientConfiguration
            {
                Servers = {new IPEndPoint(IPAddress.Parse(config.MemCachedServerIp), config.MemCachedServerPort)},
                Protocol = MemcachedProtocol.Binary,
            };
            client = new MemcachedClient(cacheConfig);
        }
        public void Dispose()
        {
            client.Dispose();
        }

        public Product Get(int id)
        {
            return client.Get<Product>(id.ToString());
        }

        public IEnumerable<Product> GetAll()
        {
            var keys = GetProductKeys();
            foreach (string key in keys.Keys)
            {
                yield return client.Get<Product>(key);
            }
        }

        public int Create(string name)
        {
            var keys = GetProductKeys();
            var newKey = keys.GetNextKey();

            client.Store(StoreMode.Add, newKey.ToString(), new Product {Id = newKey, Name = name});

            UpdateProductKeys(keys);

            return newKey;
        }

        private void UpdateProductKeys(ProductKeys keys)
        {
            client.Store(StoreMode.Set, typeof(ProductKeys).FullName, keys);
        }

        private ProductKeys GetProductKeys()
        {
            var keys = client.Get<ProductKeys>(typeof(ProductKeys).FullName);

            if (null == keys)
                keys = new ProductKeys();

            return keys;
        }
    }

    [Serializable]
    internal class ProductKeys
    {
        public List<string> Keys { get; set; }
        private int nextKey = INITIAL_ID;

        private const int INITIAL_ID = 1;
        private const int ID_INCREMENT = 1;

        public ProductKeys()
        {
            Keys = new List<string>();
        }

        public int GetNextKey()
        {
            var currentKey = nextKey;

            Keys.Add(currentKey.ToString());

            nextKey += ID_INCREMENT;
            return currentKey;
        }
    }
}
