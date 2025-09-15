using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MAUIDMSMobile.Mvvm.Models
{
    public class ReponseJeton
    {
        [JsonProperty("access_token")]
        public string Access_Token { get; set; }
    }
}
