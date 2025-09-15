using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAUIDMSMobile.Mvvm.Models;

namespace MAUIDMSMobile.Services
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(Utilisateurcreationdecompte usercreationdecompte);
        Task<string> UpdateUserAsync(Utilisateurupdate userupdate);
        Task<string> AuthenticateMobileUserAsync(UtilisateurLogin userlogin);
        
        Task<string> ResetMobileUserPasswordAsync(Utilisateurmotdepasseoublié usermotdepasseoublié );


    }
}
