using System;
using System.Configuration;
using Infrastructure;

namespace ProductEntity
{
    [InProcServiceConfiguration]
    internal class ProductEntityServiceConfiguration
    {
        private const string MEMCACHED_SERVER_IP_KEY = "ProductEntity.MemCached.Server.Ip";
        private const string MEMCACHED_SERVER_PORT_KEY = "ProductEntity.MemCached.Server.Port";
        public ProductEntityServiceConfiguration()
        {
            this.MemCachedServerIp = ConfigurationManager.AppSettings[MEMCACHED_SERVER_IP_KEY];
            if (string.IsNullOrEmpty(this.MemCachedServerIp)) throw new InvalidOperationException(string.Format("Missing configuration setting: {0}", MEMCACHED_SERVER_IP_KEY));

            int port;
            if (Int32.TryParse(ConfigurationManager.AppSettings[MEMCACHED_SERVER_PORT_KEY], out port))
                this.MemCachedServerPort = port;
            else
                throw new InvalidOperationException(string.Format("Missing configuration setting: {0}", MEMCACHED_SERVER_PORT_KEY));
        }

        public int MemCachedServerPort { get; set; }

        public string MemCachedServerIp { get; set; }
    }
}