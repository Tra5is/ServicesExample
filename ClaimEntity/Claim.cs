namespace ClaimEntity
{
    public class Claim
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public bool IsCancelled { get; set; }
    }
}