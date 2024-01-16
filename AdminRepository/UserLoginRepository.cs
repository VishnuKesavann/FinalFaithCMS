using FinalCMS.Models;
using System.Linq;
using System.Runtime.Intrinsics.X86;

namespace FinalCMS.AdminRepository
{
    public class UserLoginRepository : IUserLoginRepository
    {
        private readonly FinalCMS_dbContext _context;
        private string pwd;
        private object un;

        public UserLoginRepository(FinalCMS_dbContext context)
        {
            _context = context;
        }
        #region find user by redential
        public UserLogin validateUser(string un, string pwd)
        {
            if (_context != null)
            {
                UserLogin user = _context.UserLogin.FirstOrDefault(us => us.UserName == un && us.Password == pwd);
                if (user != null)
                {
                    return user;
                }

            }
            return null;
        }
        #endregion
    }
}
