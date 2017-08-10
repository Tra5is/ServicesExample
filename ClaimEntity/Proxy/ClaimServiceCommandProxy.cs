using ClaimEntity.Contract;
using Infrastructure;

namespace ClaimEntity.Proxy
{
    [InProcServiceProxy]
    public class ClaimServiceCommandProxy : IClaimServiceCommand
    {
        private readonly IClaimService claimService;

        public ClaimServiceCommandProxy(IClaimService claimService)
        {
            this.claimService = claimService;
        }
        
        public void CreateClaim(CreateClaimRequest request)
        {
            claimService.CreateClaim(request);
        }

        public void CancelClaim(int claimId)
        {
            claimService.CancelClaim(claimId);
        }
    }
}