using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAUIDMSMobile.Mvvm.Models;
using MAUIDMSMobile.Mvvm.Models.Odata4;

namespace MAUIDMSMobile.Mappers
{
    public static class Automapperinit
    {
        // Map from DTO to OData model
        public static UtilisateurLoginOdata ToOdata(UtilisateurLogin dto)
        {
            if (dto == null) return null;

            return new UtilisateurLoginOdata
            {
                email = dto.email,
                password = dto.password
            };
        }

        // Map from OData model to DTO (if ever needed)
        public static UtilisateurLogin ToDto(UtilisateurLoginOdata odata)
        {
            if (odata == null) return null;

            return new UtilisateurLogin
            {
                email = odata.email,
                password = odata.password,
                Checkkeepsessionopen = false // default or override if needed
            };
        }
    }
}
