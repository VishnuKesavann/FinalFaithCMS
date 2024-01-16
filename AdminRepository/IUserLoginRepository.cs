using FinalCMS.Models;
using System.Runtime.Intrinsics.X86;

namespace FinalCMS.AdminRepository
{
    public interface IUserLoginRepository
    {
        //get user by credentials
        public UserLogin validateUser(string username, string password);
    }
}
