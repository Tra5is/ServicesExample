using ClaimEngine.Contract;
using Infrastructure;

namespace ClaimEngine
{
    public interface IClaimEngineService
    {
        Validated<Claim> CreateClaim(CreateClaimRequest request);
        Validated<CancelClaimResponse> CancelClaim(CancelClaimRequest request);
    }
}