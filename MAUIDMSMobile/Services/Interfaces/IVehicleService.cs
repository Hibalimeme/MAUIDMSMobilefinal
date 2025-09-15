using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAUIDMSMobile.Mvvm.Models;


namespace MAUIDMSMobile.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<List<VehiculeValide>> GetValidatedVehiclesAsync();
        Task<List<VehiculeAttent>> GetPendingValidationVehiclesAsync();
        Task<String> CreateVehicleAsync(VehiculeModel vehicle);
        Task<List<marquemodel>> GetVehicleBrandsAsync(); 
    }
}
