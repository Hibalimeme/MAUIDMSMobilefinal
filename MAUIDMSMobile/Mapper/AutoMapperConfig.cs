using System;
using AutoMapper;
using MAUIDMSMobile.Mvvm.Models;
using MAUIDMSMobile.Mvvm.Models.Odata4;

namespace MAUIDMSMobile.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper AutoMapperInit()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Appel des méthodes pour chaque mapping
                ConfigureOdatamarquemodelToMarquemodel(cfg);
                ConfigureVehiculeModelToOdataVehiculeModel(cfg);
                ConfigurePendingVNModelToVehiculeAttent(cfg);
                ConfigureValidatedVNModelToVehiculeValide(cfg);
            });

            return config.CreateMapper();
        }

        // Méthode pour le mapping entre Odatamarquemodel et marquemodel
        private static void ConfigureOdatamarquemodelToMarquemodel(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Odatamarquemodel, marquemodel>()
                .ForMember(dest => dest.marque, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.model, opt => opt.MapFrom(src => src.No));
        }

        // Méthode pour le mapping entre VehiculeModel et OdataVehiculeModel
        private static void ConfigureVehiculeModelToOdataVehiculeModel(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<VehiculeModel, OdataVehicule>()
                .ForMember(dest => dest.brand, opt => opt.MapFrom(src => src.Marque))
                .ForMember(dest => dest.model, opt => opt.MapFrom(src => src.Modele))
                .ForMember(dest => dest.chassisNumber, opt => opt.MapFrom(src => src.NumeroChassis))
                .ForMember(dest => dest.firstRegistrationDate, opt => opt.MapFrom(src =>
                    src.DateMiseEnCirculation.HasValue
                        ? DateOnly.FromDateTime(src.DateMiseEnCirculation.Value)
                        : default)) // Or DateOnly.FromDateTime(DateTime.Today) if you prefer
                .ForMember(dest => dest.registrationNumber, opt => opt.MapFrom(src => src.Immatriculation))
                .ForMember(dest => dest.mobileUser, opt => opt.MapFrom(src => src.UserLogin))
                .ForMember(dest => dest.registrationCardPhoto, opt => opt.MapFrom(src => src.PhotoCarteGrise))
                .ForMember(dest => dest.photoTwo, opt => opt.MapFrom(src => src.Photo2))
                .ForMember(dest => dest.photoThree, opt => opt.MapFrom(src => src.Photo3));
        }
        public static void ConfigurePendingVNModelToVehiculeAttent(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<PendingVNModel, VehiculeAttent>()
                .ForMember(dest => dest.userlogin, opt => opt.MapFrom(src => src.User_ID))
                .ForMember(dest => dest.Marque, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.NumChassis, opt => opt.MapFrom(src => src.NumChassis))
                .ForMember(dest => dest.DateMiseEnCirculation, opt => opt.MapFrom(src => src.DateMiseEnCirculation))
                .ForMember(dest => dest.Immatriculation, opt => opt.MapFrom(src => src.Immatriculation));
        }
        public static void ConfigureValidatedVNModelToVehiculeValide(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ValidatedVNModel, VehiculeValide>()
                .ForMember(dest => dest.userlogin, opt => opt.MapFrom(src => src.User_ID))
                .ForMember(dest => dest.Marque, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.NumChassis, opt => opt.MapFrom(src => src.NumChassis))
                .ForMember(dest => dest.DateMiseEnCirculation, opt => opt.MapFrom(src => src.DateMiseEnCirculation))
                .ForMember(dest => dest.Immatriculation, opt => opt.MapFrom(src => src.Immatriculation));
        }
        public static IMapper MapRendezVousToAppointmentPostModel()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RendezVousPosteModel, AppointmentPostModel>()
                    .ForMember(dest => dest.validatedVehicleID, opt => opt.MapFrom(src => src.IdVehiculeValide))
                    .ForMember(dest => dest.vehicleMileage, opt => opt.MapFrom(src => src.KilometrageVehicule))
                    .ForMember(dest => dest.maintenancePackageCode, opt => opt.MapFrom(src => src.CodePackageMaintenance))
                    .ForMember(dest => dest.serviceCenterCode, opt => opt.MapFrom(src => src.CodeCentreService))
                    .ForMember(dest => dest.slotDataJson, opt => opt.MapFrom(src => src.DonneesSlotJson))
                    .ForMember(dest => dest.mobileLogin, opt => opt.MapFrom(src => src.LoginMobile));
            });

            return config.CreateMapper();
        }

        public static IMapper MapServiceCenterOdataToCentreServiceModel()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ServiceCenterOdata, CentreServiceModel>()
                    .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.ServiceCenterCode))
                    .ForMember(dest => dest.NomCentre, opt => opt.MapFrom(src => src.ServiceCenterName))
                    .ForMember(dest => dest.AddressECentre, opt => opt.MapFrom(src => src.ServiceCenterAddress));
            });

            return config.CreateMapper();
        }
        public static IMapper CreateMapperForValidatedVehicle()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ValidatedVNModel, VehiculeValide>()
                    .ForMember(dest => dest.userlogin, opt => opt.MapFrom(src => src.User_ID))
                    .ForMember(dest => dest.Marque, opt => opt.MapFrom(src => src.Brand))
                    .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                    .ForMember(dest => dest.NumChassis, opt => opt.MapFrom(src => src.NumChassis))
                    .ForMember(dest => dest.DateMiseEnCirculation, opt => opt.MapFrom(src => src.DateMiseEnCirculation))
                    .ForMember(dest => dest.Immatriculation, opt => opt.MapFrom(src => src.Immatriculation));
            });

            return config.CreateMapper();
        }



        public static IMapper MapPackOdataToPackDeMaintenance()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PackOdata, PackDeMaintenance>()
                    .ForMember(dest => dest.CodePackMaintenance, opt => opt.MapFrom(src => src.MaintenancePackCode))
                    .ForMember(dest => dest.Marque, opt => opt.MapFrom(src => src.Brand))
                    .ForMember(dest => dest.Modele, opt => opt.MapFrom(src => src.Model))
                    .ForMember(dest => dest.Kilometrage, opt => opt.MapFrom(src => src.Mileage))
                    .ForMember(dest => dest.DureeEnMinutes, opt => opt.MapFrom(src => src.DurationInMinutes));
            });

            return config.CreateMapper();
        }

        public static IMapper MapCreneauxOdataToCreneau()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreneauxOdata, Creneau>()
                    .ForMember(dest => dest.CodeCreneau, opt => opt.MapFrom(src => src.ScheduledTimeNo))
                    .ForMember(dest => dest.IdentifiantUtilisateur, opt => opt.MapFrom(src => src.LoginMobile))
                    .ForMember(dest => dest.IdVehicule, opt => opt.MapFrom(src => src.IdentifiedVehicleID))
                    .ForMember(dest => dest.Marque, opt => opt.MapFrom(src => src.Brand))
                    .ForMember(dest => dest.Modele, opt => opt.MapFrom(src => src.Model))
                    .ForMember(dest => dest.NumeroChassis, opt => opt.MapFrom(src => src.ChassisNumber))
                    .ForMember(dest => dest.CodePackMaintenance, opt => opt.MapFrom(src => src.MaintenancePackCode))
                    .ForMember(dest => dest.Kilometrage, opt => opt.MapFrom(src => src.Mileage))
                    .ForMember(dest => dest.DatePlanifiee, opt => opt.MapFrom(src => src.ScheduledDate))
                    .ForMember(dest => dest.HeurePlanifiee, opt => opt.MapFrom(src => src.ScheduledTime))
                    .ForMember(dest => dest.CodeCentre, opt => opt.MapFrom(src => src.Box))
                    .ForMember(dest => dest.Duree, opt => opt.MapFrom(src => src.Duration));
            });

            return config.CreateMapper();
        }

        public static IMapper MapRendezVousOdataToCreneau()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GetRendezVousOdata, GetRendezVous>()
                    .ForMember(dest => dest.Heure_programmée, opt => opt.MapFrom(src => src.Scheduled_Time_No))
                    .ForMember(dest => dest.IdentifiantUtilisateur, opt => opt.MapFrom(src => src.LoginMobile))
                    .ForMember(dest => dest.IdVehicule, opt => opt.MapFrom(src => src.IdentifiedVehicleID))
                    .ForMember(dest => dest.Marque, opt => opt.MapFrom(src => src.Brand))
                    .ForMember(dest => dest.Modele, opt => opt.MapFrom(src => src.Model))
                    .ForMember(dest => dest.NumeroChassis, opt => opt.MapFrom(src => src.ChassisNumber))
                    .ForMember(dest => dest.CodePackMaintenance, opt => opt.MapFrom(src => src.MaintenancePackCode))
                    .ForMember(dest => dest.Kilometrage, opt => opt.MapFrom(src => src.Mileage))
                    .ForMember(dest => dest.DatePlanifiee, opt => opt.MapFrom(src => src.ScheduledDate))
                    .ForMember(dest => dest.HeurePlanifiee, opt => opt.MapFrom(src => src.ScheduledTime))
                    .ForMember(dest => dest.CodeCentre, opt => opt.MapFrom(src => src.Box));
            });

            return config.CreateMapper();
        }

        public static IMapper MapHistoriqueOdataToHistoriqueRendezVous()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HistoriqueRendezVousOdata, HistoriqueRendezVous>()
                    .ForMember(dest => dest.NumeroCreneau, opt => opt.MapFrom(src => src.ScheduledTimeNo))              // Mapping du créneau
                    .ForMember(dest => dest.DatePlanifiee, opt => opt.MapFrom(src => src.ScheduledDate))                // Mapping de la date
                    .ForMember(dest => dest.HeurePlanifiee, opt => opt.MapFrom(src => src.ScheduledTime))              // Mapping de l'heure
                    .ForMember(dest => dest.CodeCentreResponsable, opt => opt.MapFrom(src => src.ResponsibilityCenter)) // Mapping du centre
                    .ForMember(dest => dest.Statut, opt => opt.MapFrom(src => src.Status))                              // Mapping du statut
                    .ForMember(dest => dest.DureeEnMinutes, opt => opt.MapFrom(src => src.Duration))                   // Mapping de la durée
                    .ForMember(dest => dest.IdentifiantUtilisateur, opt => opt.MapFrom(src => src.UserLogin));          // Mapping du login utilisateur
            });

            return config.CreateMapper();
        }
        public static IMapper MapRendezVousAvenirOdataToRendezVousAvenir()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RendezVousAvenirModelOdata, RendezVousAvenirModel>()
                    .ForMember(dest => dest.NumeroCreneau, opt => opt.MapFrom(src => src.ScheduledTimeNo))
                    .ForMember(dest => dest.EmailUtilisateur, opt => opt.MapFrom(src => src.LoginMobile))
                    .ForMember(dest => dest.IdentifiantVehicule, opt => opt.MapFrom(src => src.IdentifiedVehicleID))
                    .ForMember(dest => dest.Marque, opt => opt.MapFrom(src => src.Brand))
                    .ForMember(dest => dest.Modele, opt => opt.MapFrom(src => src.Model))
                    .ForMember(dest => dest.NumeroChassis, opt => opt.MapFrom(src => src.ChassisNumber))
                    .ForMember(dest => dest.CodePackEntretien, opt => opt.MapFrom(src => src.MaintenancePackCode))
                    .ForMember(dest => dest.Kilometrage, opt => opt.MapFrom(src => src.Mileage))
                    .ForMember(dest => dest.DatePlanifiee, opt => opt.MapFrom(src => src.ScheduledDate))
                    .ForMember(dest => dest.HeurePlanifiee, opt => opt.MapFrom(src => src.ScheduledTime))
                    .ForMember(dest => dest.CodeBox, opt => opt.MapFrom(src => src.Box))
                    .ForMember(dest => dest.DureeMinutes, opt => opt.MapFrom(src => src.Duration));
            });

            return config.CreateMapper();
        }



        public static IMapper MapDevisOdataToDevis()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DevisModelOdata, DevisModel>()
                    .ForMember(dest => dest.NumeroDevis, opt => opt.MapFrom(src => src.QuoteNo))
                    .ForMember(dest => dest.NumeroLigne, opt => opt.MapFrom(src => src.LineNo))
                    .ForMember(dest => dest.NumeroChassis, opt => opt.MapFrom(src => src.ChassisNo))
                    .ForMember(dest => dest.LoginMobile, opt => opt.MapFrom(src => src.LoginMobile))
                    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                    .ForMember(dest => dest.Libelle, opt => opt.MapFrom(src => src.Label))
                    .ForMember(dest => dest.Montant, opt => opt.MapFrom(src => src.Amount))
                    .ForMember(dest => dest.ImageBase64, opt => opt.MapFrom(src => src.ImageBase64))
                    .ForMember(dest => dest.Statut, opt => opt.MapFrom(src => src.Status));
            });

            return config.CreateMapper();
        }

        public static IMapper MapDevisToDevisPostOdata()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DevisModelPost, DevisModelPostOdata>()
                    .ForMember(dest => dest.quoteNo, opt => opt.MapFrom(src => src.NumDevis))
                    .ForMember(dest => dest.clientResponse, opt => opt.MapFrom(src => src.ReponseClient))
                    .ForMember(dest => dest.loginMobile, opt => opt.MapFrom(src => src.LoginMobile));
            });

            return config.CreateMapper();
        }







    }
}
