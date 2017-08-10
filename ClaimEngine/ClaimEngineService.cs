using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimEngine.ClaimDomain.Assemblers;
using ClaimEngine.ClaimDomain.Validators;
using ClaimEngine.Contract;
using ClaimEntity.Contract;
using Infrastructure;
using ProductEntity;
using CreateClaimRequest = ClaimEngine.Contract.CreateClaimRequest;

namespace ClaimEngine
{
    public interface IClaimEngineServiceInternal : IClaimEngineService
    {}

    [InProcService]
    public class ClaimEngineService : IClaimEngineServiceInternal
    {
        private IProductQuery productQuery;
        private IClaimServiceQuery claimQuery;

        public ClaimEngineService(IProductQuery productQuery, IClaimServiceQuery claimQuery)
        {
            this.productQuery = productQuery;
            this.claimQuery = claimQuery;
        }

        public Validated<Claim> CreateClaim(CreateClaimRequest request)
        {
            var validationErrors = new CreateClaimValidator(productQuery).ValidateRequest(request);
            if (validationErrors.Any())
                return new Validated<Claim>(validationErrors);

            return new Validated<Claim>(new ClaimAssembler().Assemble(request));
        }

        public Validated<CancelClaimResponse> CancelClaim(CancelClaimRequest request)
        {
            var validationError = new CancelClaimValidator(claimQuery).ValidateRequest(request);
            if (validationError != null)
                return new Validated<CancelClaimResponse>(new Exception[] {validationError});

            return new Validated<CancelClaimResponse>(new CancelClaimResponse());
        }
    }
}
