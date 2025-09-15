//using System;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using MAUIDMSMobile.Mvvm.Models;
//using Newtonsoft.Json;
//using MAUIDMSMobile.Mvvm.Models;
//using MAUIDMSMobile.Services;
//using MAUIDMSMobile.Static;
//using MAUIDMSMobile.Services.Interfaces;


//namespace MAUIDMSMobile.Services.Implementations
//{
//    public class TacheServicemock : ITacheService
//    {
       
//        // Enregistrer un véhicule pour un utilisateur mobile
//        public async Task<string> RegisterMobileUserVehiculeAsync(VehiculeModel vehicule)
//        {
//            try
//            {
//                return "responseContent";
//            }
//            catch (Exception ex)
//            {
//                // Gérer les exceptions
//                return "Error: " + ex.Message;
//            }
//        }
//        public async Task<List<VehiculeModel>> GetUserVehiculesAsync()
//        {
//            try
//            {
//                await Task.Delay(10);
//                List<VehiculeModel> lst = new List<VehiculeModel>()
//                {
//                    new VehiculeModel()
//                    {
//                        UserLogin ="Hiba",
//                        DateMiseEnCirculation = "15/09/2024",
//                        Immatriculation="AA-001-AA",
//                        Marque="Audi",
//                        Modele="Audi Q8",
//                        NumeroChassis="1HGCM82633A123456",

//                    }
//                };
//                return lst;
//            }
//            catch (JsonException jsonEx)
//            {
//                Console.WriteLine("JSON Parsing Error: " + jsonEx.Message);
//                return new List<VehiculeModel>();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Error: " + ex.Message);
//                return new List<VehiculeModel>();
//            }
//        }
//    }
//}


