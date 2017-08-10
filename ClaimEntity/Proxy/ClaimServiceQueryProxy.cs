using System.Collections.Generic;
using ClaimEntity.Contract;
using Infrastructure;

namespace ClaimEntity.Proxy
{
    [InProcServiceProxy]
    public class ClaimServiceQueryProxy : IClaimServiceQuery
    {
        private readonly IClaimService claimService;

        public ClaimServiceQueryProxy(IClaimService claimService)
        {
            this.claimService = claimService;
        }

        public Claim Get(int id)
        {
            return claimService.Get(id);
        }

        public IEnumerable<Claim> GetAll()
        {
            return claimService.GetAll();
        }
    }
}