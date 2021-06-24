namespace ClayTest.Application.Services.Interfaces
{
    public interface IClaimService
    {
        string GetUserId();

        string GetClaim(string key);
    }
}
