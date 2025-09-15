using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAUIDMSMobile.Mvvm.Models;

namespace MAUIDMSMobile.Services.Interfaces
{
    public interface IserviceDevis
    {

        Task<List<DevisModel>> GetDevisAsync();
        Task<string> CreateDevisResponseAsync(DevisModelPost devis);


    }
}

