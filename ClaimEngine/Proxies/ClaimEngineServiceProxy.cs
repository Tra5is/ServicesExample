using ClaimEngine.Contract;
using Infrastructure;

namespace ClaimEngine.Proxies
{
    [InProcServiceProxy]
    public class ClaimEngineServiceProxy : IClaimEngineService
    {
        private IClaimEngineServiceInternal claimEngineService;

        public ClaimEngineServiceProxy(IClaimEngineServiceInternal claimEngineService)
        {
            this.claimEngineService = claimEngineService;
        }

        public Validated<Claim> CreateClaim(CreateClaimRequest request)
        {
            return claimEngineService.CreateClaim(request);
        }

        public Validated<CancelClaimResponse> CancelClaim(CancelClaimRequest request)
        {
            return claimEngineService.CancelClaim(request);
        }
    }
}