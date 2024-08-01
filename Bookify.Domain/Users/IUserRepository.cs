namespace Bookify.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetBYIdAsync(Guid id, CancellationToken cancellationToken=default);
        void Add(User user);
    }
}
