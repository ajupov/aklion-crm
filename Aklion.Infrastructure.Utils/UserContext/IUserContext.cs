namespace Aklion.Infrastructure.Utils.UserContext
{
    public interface IUserContext
    {
        int UserId { get; set; }

        int StoreId { get; set; }
    }
}