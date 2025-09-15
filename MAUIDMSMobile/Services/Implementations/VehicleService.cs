using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MAUIDMSMobile.Mvvm.Models;
using MAUIDMSMobile.Mvvm.Models.Odata4;
using MAUIDMSMobile.Resx;
using MAUIDMSMobile.Services.Interfaces;
using MAUIDMSMobile.Static;
using Newtonsoft.Json;
using MAUIDMSMobile.Resx;

namespace MAUIDMSMobile.Services.Implementations
{
    public class VehicleService : IVehicleService
    {
        private readonly IAuthService _authService;
        private readonly Methods _methods;
        private readonly IMapper _mapper;

        public VehicleService(IAuthService authService, Methods methods, IMapper mapper)
        {
            _authService = authService;
            _methods = methods;
            _mapper = mapper;
        }

        public static async Task<bool> IsDeviceConnectedToInternet(IPAddress serverIp = null)
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                if (serverIp != null)
                {
                    if (current != Microsoft.Maui.Networking.NetworkAccess.Internet)
                        return false;

                    var ping = new Ping();
                    var reply = await ping.SendPingAsync(serverIp, 5000);
                    return reply.Status == IPStatus.Success;
                }

                return current == Microsoft.Maui.Networking.NetworkAccess.Internet;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<marquemodel>> GetVehicleBrandsAsync()
        {
            try
            {
                var token = await _authService.GetAccessTokenAsync();
                var url = "Company('CRONUS%20FR')/Brand";

                var responseContent = await _methods.SendGetRequestAsync(url, token);
                if (responseContent.StartsWith("Error") || responseContent.StartsWith("Exception"))
                {
                     App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, responseContent, "Fermer");
                    return new();
                }

                var parsed = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                return _mapper.Map<List<marquemodel>>(parsed.value);
            }
            catch (Exception ex)
            {
                 App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, ex.Message, "Fermer");
                return new();
            }
        }

        public async Task<List<VehiculeValide>> GetValidatedVehiclesAsync()
        {
            try
            {
                var token = await _authService.GetAccessTokenAsync();
                var url = $"Company('CRONUS%20FR')/ValidatedVN?$filter=UserLogin eq '{StaticVariables.CurrentUser.email}'";

                Trace.WriteLine(url);
                var responseContent = await _methods.SendGetRequestAsync(url, token);

                if (responseContent.StartsWith("Error") || responseContent.StartsWith("Exception"))
                {
                     App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, responseContent, "Fermer");
                    return new();
                }

                var parsed = JsonConvert.DeserializeObject<ApiResponseValidatedVN>(responseContent);
                return _mapper.Map<List<VehiculeValide>>(parsed.value);
            }
            catch (Exception ex)
            {
                 App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, ex.Message, "Fermer");
                return new();
            }
        }

        public async Task<string> CreateVehicleAsync(VehiculeModel vehicle)
        {
            try
            {
                var token = await _authService.GetAccessTokenAsync();
                var url = "DMSMobileCU_RegisterMobileUserVehicle?company=CRONUS%20FR";
                var odataVehiculeModel = _mapper.Map<OdataVehicule>(vehicle);
                var jsonPayload = JsonConvert.SerializeObject(odataVehiculeModel);

                var responseContent = await _methods.SendPostRequestAsync(url, jsonPayload, token);
                if (responseContent.StartsWith("Error") || responseContent.StartsWith("Exception"))
                     App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, responseContent, "Fermer");

                return responseContent;
            }
            catch (Exception ex)
            {
                 App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, ex.Message, "Fermer");
                return "Error: " + ex.Message;
            }
        }

        public async Task<List<VehiculeAttent>> GetPendingValidationVehiclesAsync()
        {
            try
            {
                var token = await _authService.GetAccessTokenAsync();
                var url = $"Company('CRONUS%20FR')/PendingVN?$filter=UserLogin eq '{StaticVariables.CurrentUser.email}'";

                Trace.WriteLine(url);
                var responseContent = await _methods.SendGetRequestAsync(url, token);

                if (responseContent.StartsWith("Error") || responseContent.StartsWith("Exception"))
                {
                     App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, responseContent, "Fermer");
                    return new();
                }

                var parsed = JsonConvert.DeserializeObject<ApiResponsePendingVN>(responseContent);
                return _mapper.Map<List<VehiculeAttent>>(parsed.value);
            }
            catch (Exception ex)
            {
                 App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, ex.Message, "Fermer");
                return new();
            }
        }
    }
}
