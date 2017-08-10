using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ClaimEngine;
using ClaimEngine.Contract;
using ClaimEntity;
using ClaimEntity.Contract;
using CreateClaimRequest = ClaimEngine.Contract.CreateClaimRequest;

namespace ClaimService
{
    public class ClaimController : ApiController
    {
        private readonly IClaimEngineService claimEngine;
        private readonly IClaimServiceQuery claimQuery;
        private readonly IClaimServiceCommand claimCommand;

        public ClaimController(IClaimEngineService claimEngine, IClaimServiceQuery claimQuery, IClaimServiceCommand claimCommand)
        {
            this.claimEngine = claimEngine;
            this.claimQuery = claimQuery;
            this.claimCommand = claimCommand;
        }

        // GET api/Claim 
        public IEnumerable<string> Get()
        {
            return claimQuery.GetAll().Select(p => p.ToView());
        }

        // GET api/Claim/5 
        public string Get(int id)
        {
            var claim = claimQuery.Get(id);
            if (claim == null)
                return "no product with that Id";

            return claim.ToView();
        }

        // POST api/Claim 
        public string Post([FromBody]int productId)
        {
            var newClaimResult = claimEngine.CreateClaim(new CreateClaimRequest {ProductId = productId});
            if (newClaimResult.IsValid)
            {
                var newClaim = newClaimResult.Value;
                claimCommand.CreateClaim(new ClaimEntity.Contract.CreateClaimRequest { NewClaim = newClaim.MapTo()} );
                return "New Product Id: " + newClaim.ToView();
            }

            return string.Format("CreateClaimRequest is invalid: {0}",
                newClaimResult.Exceptions.Select(e => e.Message)
                    .Aggregate((next, aggr) => string.Format("{0}, {1}", next, aggr)));
        }

        // PUT api/Product/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Product/5 
        public string Delete(int id)
        {
            var claim = claimQuery.Get(id);
            var cancelClaimResult = claimEngine.CancelClaim(new CancelClaimRequest {Claim = claim.MapTo()});
            if (cancelClaimResult.IsValid)
            {
                claimCommand.CancelClaim(claim.Id);
                return "Success";
            }

            return string.Format("Delete request is invalid: {0}",
                cancelClaimResult.Exceptions.Select(e => e.Message)
                    .Aggregate((next, aggr) => string.Format("{0}, {1}", next, aggr)));
        }
    }

    public static class ClaimExtensions
    {
        public static string ToView(this ClaimEngine.Contract.Claim c)
        {
            return string.Format("Id:{0} Product:{1} Cancelled:{2}", c.Id, c.ProductId, c.IsCancelled);
        }

        public static string ToView(this ClaimEntity.Claim c)
        {
            return string.Format("Id:{0} Product:{1} Cancelled:{2}", c.Id, c.ProductId, c.IsCancelled);
        }

        public static ClaimEngine.Contract.Claim MapTo(this ClaimEntity.Claim c)
        {
            return new ClaimEngine.Contract.Claim
            {
                Id = c.Id,
                ProductId = c.ProductId,
                IsCancelled = c.IsCancelled
            };
        }

        public static ClaimEntity.Claim MapTo(this ClaimEngine.Contract.Claim c)
        {
            return new ClaimEntity.Claim
            {
                Id = c.Id,
                ProductId = c.ProductId,
                IsCancelled = c.IsCancelled
            };
        }
    }
}