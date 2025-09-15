using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAUIDMSMobile.Mvvm.Models;

namespace MAUIDMSMobile.Services.Interfaces
{
    public interface IServiceRendezVous
    {
        // Méthode pour obtenir la liste des rendez-vous à venir
       
        Task<List<CentreServiceModel>> GetCentresServiceAsync();
        Task<List<PackDeMaintenance>> GetPacksDeMaintenanceAsync();
        Task<List<Creneau>> GetCreneauxAsync();
        Task<List<VehiculeValide>> GetValidatedVehiclesAsync();
        Task<List<RendezVousAvenirModel>> GetRendezVousAsync();

        Task<List<HistoriqueRendezVous>> GetHistoriqueAsync();

        Task<string> CreateAppointmentAsync(RendezVousPosteModel appointment);
    }
    }
