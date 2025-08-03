namespace CustomerRelationManagement.Services
{
    public interface IUserService 
    {
        public bool ValidateUser(string username, string password, out string roleName);

    }
}
