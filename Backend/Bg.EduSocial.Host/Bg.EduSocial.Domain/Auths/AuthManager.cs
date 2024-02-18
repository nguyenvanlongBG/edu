using Bg.EduSocial.Domain.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Domain.Auths
{
    public class AuthManager
    {
        public readonly UserManager<User> _userManager;
        public AuthManager(UserManager<User> userManager)
        {
             _userManager = userManager;
        }
        public Task<User> GetUserByName(string username)
        {
            var user = _userManager.FindByNameAsync(username);
            return user;
        }
    }
}
