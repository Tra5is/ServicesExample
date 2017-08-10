namespace ClaimEngine.ClaimDomain.Services
{
    public class ClaimIdGeneratorService
    {
        private static int nextId = 1;
        public int GetNext()
        {
            return nextId++;
        }
    }
}