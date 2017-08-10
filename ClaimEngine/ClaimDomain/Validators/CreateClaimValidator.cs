using System;
using System.Collections.Generic;
using ClaimEngine.Contract;
using ProductEntity;

namespace ClaimEngine.ClaimDomain.Validators
{
    public class CreateClaimValidator
    {
        private readonly IProductQuery productQuery;

        public CreateClaimValidator(IProductQuery productQuery)
        {
            this.productQuery = productQuery;
        }

        public IEnumerable<Exception> ValidateRequest(CreateClaimRequest request)
        {
            if (productQuery.Get(request.ProductId) == null)
                return new List<Exception>() { new InvalidOperationException(string.Format("Product {0} does not exist", request.ProductId))};

            return new List<Exception>();
        }
    }
}