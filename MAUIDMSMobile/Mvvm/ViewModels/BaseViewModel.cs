using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MAUIDMSMobile.Mvvm.Models;
using MAUIDMSMobile.Services;
using static System.Net.Mime.MediaTypeNames;
using YourApp.Services;
using MAUIDMSMobile.Static;
using CommunityToolkit.Mvvm.ComponentModel;
using MAUIDMSMobile.Mvvm.Views;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using MAUIDMSMobile.Resx;
using System.Globalization;
using MAUIDMSMobile.Services.Interfaces;
using System.Collections.ObjectModel;
namespace MAUIDMSMobile.Mvvm.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        private readonly IVehicleService _VehicleService;

        [ObservableProperty]
        public bool _isBusy;
        public BaseViewModel()
        {
            _VehicleService = ServiceHelper.GetService<IVehicleService>();
            VehiculesValides = new ObservableCollection<VehiculeValide>();
        }
        [ObservableProperty]
        public ObservableCollection<VehiculeValide> _vehiculesValides;


        public async Task LoadValidatedVehiclesInternalAsync()
        {
            try
            {

                var allvehicule = await _VehicleService.GetValidatedVehiclesAsync();

                if (allvehicule != null && allvehicule.Count > 0)
                {
                    VehiculesValides = new ObservableCollection<VehiculeValide>(allvehicule);
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }



    }
}
