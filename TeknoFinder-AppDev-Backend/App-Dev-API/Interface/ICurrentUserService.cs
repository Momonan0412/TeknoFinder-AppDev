namespace AppDev.API.Interface
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        Guid StudentId { get; }
    }
}
