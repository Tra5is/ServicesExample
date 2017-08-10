using System.Collections.Generic;
using ClaimEntity.Contract;
using Infrastructure;

namespace ClaimEntity
{
    public interface IClaimService : IClaimServiceCommand, IClaimServiceQuery { }

    [InProcService]
    internal class ClaimService : IClaimService
    {
        private Dictionary<int, Claim> claims;

        public ClaimService()
        {
            this.claims = new Dictionary<int, Claim>();
        }

        public void CreateClaim(CreateClaimRequest request)
        {
            var newId = GetNextClaimId();
            claims.Add(newId, request.NewClaim);
        }

        public void CancelClaim(int claimId)
        {
            var claim = Get(claimId);
            claim.IsCancelled = true;
        }

        public Claim Get(int id)
        {
            Claim claim;
            return (claims.TryGetValue(id, out claim))
                ? claim
                : null;
        }

        public IEnumerable<Claim> GetAll()
        {
            return claims.Values;
        }

        private int GetNextClaimId()
        {
            return claims.Count + 1;
        }
    }
}
