using System;
using ClaimEngine.Contract;
using ClaimEntity.Contract;

namespace ClaimEngine.ClaimDomain.Validators
{
    public class CancelClaimValidator
    {
        private readonly IClaimServiceQuery claimQuery;

        public CancelClaimValidator(IClaimServiceQuery claimQuery)
        {
            this.claimQuery = claimQuery;
        }

        public Exception ValidateRequest(CancelClaimRequest request)
        {
            return (null == claimQuery.Get(request.Claim.Id))
                ? new InvalidOperationException(string.Format("Claim with Id {0} does not exist", request.Claim.Id))
                : null;
        }
    }
}