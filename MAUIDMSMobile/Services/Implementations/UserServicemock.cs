using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAUIDMSMobile.Mvvm.Models;
using Newtonsoft.Json;

namespace MAUIDMSMobile.Services.Implementations
{
    public class UserServicemock : IUserService
    {
        public async Task<string> CreateUserAsync(Utilisateurcreationdecompte usercreationdecompte)
        {
            try
            {
                return "1";

            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                return "Error: " + ex.Message;
            }
        }
        public async Task<string> UpdateUserAsync(Utilisateurupdate userupdate)
        {
            try
            {
                return "1";

            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        public async Task<string> AuthenticateMobileUserAsync(UtilisateurLogin userlogin)
        {
            try
            {
                if (userlogin.email.Equals("29521100") && userlogin.password.Equals("hibahiba"))
                {
                    return "1";
                }
                else return "-5";

            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                return "Error: " + ex.Message;
            }
        }
        public async Task<string> ResetMobileUserPasswordAsync(Utilisateurmotdepasseoublié usermotdepasseoublié)
        {
            try
            {
                if (string.IsNullOrEmpty(usermotdepasseoublié.email)) { return "-2"; }
                else
                    return "1";
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur
                return "Error: " + ex.Message;
            }
        }
    }
}
