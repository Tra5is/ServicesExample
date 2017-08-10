namespace ClaimEntity.Contract
{
    public interface IClaimServiceCommand
    {
        void CreateClaim(CreateClaimRequest request);
        void CancelClaim(int claimId);
    }
}