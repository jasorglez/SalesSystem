using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using SalesSystema.Data;
using SalesSystema.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp_Sales_System.Library
{
    public class ListObject

    {
        public LUsersRoles _usersRole; 
        public IdentityError _identityError;
        public ApplicationDbContext _context;
        public IWebHostEnvironment _enviroment;

        public RoleManager<IdentityRole> _roleManager;
        public UserManager<IdentityUser> _userManager;
        public SignInManager<IdentityUser> _signInManager;


    }
}
