using ClaimEngine.ClaimDomain.Services;
using ClaimEngine.Contract;

namespace ClaimEngine.ClaimDomain.Assemblers
{
    public class ClaimAssembler
    {
        public Claim Assemble(CreateClaimRequest request)
        {
            return new Claim
            {
                Id = new ClaimIdGeneratorService().GetNext(),
                ProductId = request.ProductId,
                IsCancelled = false
            };
        }
    }
}