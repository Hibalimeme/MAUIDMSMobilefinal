using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MAUIDMSMobile.Mvvm.Models;
using Newtonsoft.Json;
using MAUIDMSMobile.Mvvm.Models;
using MAUIDMSMobile.Services;
using MAUIDMSMobile.Static;
using MAUIDMSMobile.Mvvm.Models.Odata4;
using MAUIDMSMobile.Mappers;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;
using MAUIDMSMobile.Resx;

namespace YourApp.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthService _authService;
        private readonly Methods _methods;
        private readonly string apiUrl = "https://api.businesscentral.dynamics.com/v2.0/6db6dfc0-0dec-4ea8-8818-5d7d08b50ca4/ODataV4/DMSMobileCU_CreateMobileUserAccount?company=CRONUS%20FR";
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _authService = ServiceHelper.GetService<IAuthService>();
            _httpClient = httpClient;

            _methods = new Methods(_httpClient);
        }
        //public static async Task<bool> IsDeviceConnectedToInternet(IPAddress serverIp = null)
        //{
        //    try
        //    {
        //        var current = Connectivity.NetworkAccess;
        //        if (serverIp != null)
        //        {
        //            if (current != Microsoft.Maui.Networking.NetworkAccess.Internet)
        //                return false;

        //            var timer = new Stopwatch();
        //            timer.Start();
        //            var ping = new Ping();
        //            var reply = await ping.SendPingAsync(serverIp, 5000);
        //            timer.Stop();
        //            TimeSpan timeTaken = timer.Elapsed;
        //            if (reply.Status == IPStatus.Success)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }

        //        }
        //        if (current == Microsoft.Maui.Networking.NetworkAccess.Internet)
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //todo recherche bonne pratique httpclient+timeout
        public async Task<string> CreateUserAsync(Utilisateurcreationdecompte usercreationdecompte)
        {

            try
            {
               
                // Get the access token
                string token = await _authService.GetAccessTokenAsync();
                ;

                // Define the API URL for creating a user
                string apiUrl = "DMSMobileCU_CreateMobileUserAccount?company=CRONUS%20FR";  // Replace with your actual API URL

                // Serialize the user object to JSON
                string jsonPayload = JsonConvert.SerializeObject(usercreationdecompte);

          

                // Use the SendPostRequestAsync method from the Methods class
                string responseContent = await _methods.SendPostRequestAsync(apiUrl, jsonPayload, token);

                // Return the response content (it will either be the response body or an error message)
                return responseContent;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, ex.Message, "Fermer");
                // Handle any exceptions that may occur
                return "Error: " + ex.Message;
            }
        }
        public async Task<string> UpdateUserAsync(Utilisateurupdate userupdate)
        {
            try
            {
                
                
                string token = await _authService.GetAccessTokenAsync();
                string apiUrl = "DMSMobileCU_UpdateMobileUserAccount?company=CRONUS%20FR";
                string jsonPayload = JsonConvert.SerializeObject(userupdate);


                string responseContent = await _methods.SendPostRequestAsync(apiUrl, jsonPayload, token);

                // Afficher un message de succès
              

                return responseContent;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, ex.Message, "Fermer");

                return "Error: " + ex.Message;
            }
        }

        public async Task<string> AuthenticateMobileUserAsync(UtilisateurLogin userlogin )
        {
            try
            {
               
                string token = await _authService.GetAccessTokenAsync();
                ;
                // Define the API URL for authentication
                string authApiUrl = "DMSMobileCU_AuthenticateMobileUser?company=CRONUS%20FR";
                UtilisateurLoginOdata authOdataModel = Automapperinit.ToOdata(userlogin);

                //// Prepare the authentication request data
                //UtilisateurLoginOdata authOdataModel = new UtilisateurLoginOdata
                //{
                //    email = userlogin.email,
                //    password = userlogin.password
                //};

                // Serialize the request data to JSON
                string jsonPayload = JsonConvert.SerializeObject(authOdataModel);

                // Get the access token using your existing AuthService
                

     

                // Use the SendPostRequestAsync method from the Methods class
                string responseContent = await _methods.SendPostRequestAsync(authApiUrl, jsonPayload, token);

                // Return the response content (it will be either the response body or an error message)
                return responseContent;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, ex.Message, "Fermer");

                return "Error: " + ex.Message;
            }
        }
        public async Task<string> ResetMobileUserPasswordAsync(Utilisateurmotdepasseoublié usermotdepasseoublié)
        {
            try
            {
             
                // Get the access token
                string token = await _authService.GetAccessTokenAsync();

                // Define the API URL for resetting the password
                string apiUrl = "DMSMobileCU_ResetMobileUserPassword?company=CRONUS%20FR";

                string jsonPayload = JsonConvert.SerializeObject(usermotdepasseoublié);
    

                // Use the SendPostRequestAsync method from the Methods class
                string responseContent = await _methods.SendPostRequestAsync(apiUrl, jsonPayload, token);

                // Return the response content (it will either be the response body or an error message)
                return responseContent;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, ex.Message, "Fermer");

                return "Error: " + ex.Message;
            }
        }



    }
}
