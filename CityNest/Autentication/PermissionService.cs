namespace CityNest
{
    public class PermissionService : IPermissionService
    {
        private readonly IUsersRepository _usersRepository;
        public PermissionService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Task<HashSet<Permission>> GetPermissionAsync(Guid userId)
        {
            return _usersRepository.GetUserPermissions(userId);
        }
    }
}
