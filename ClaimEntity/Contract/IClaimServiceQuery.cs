using System.Collections.Generic;

namespace ClaimEntity.Contract
{
    public interface IClaimServiceQuery
    {
        Claim Get(int id);
        IEnumerable<Claim> GetAll();
    }
}