namespace WebUI.Interfaces
{
    public interface IPostitDataService
    {
        Task<string?> GetPostCodeByAddressAsync(string address);
    }
}
