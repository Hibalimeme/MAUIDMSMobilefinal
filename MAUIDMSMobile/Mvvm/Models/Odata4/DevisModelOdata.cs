using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models.Odata4
{
    public class DevisModelOdata
    {
        public string QuoteNo { get; set; }           // Q-0001
        public int LineNo { get; set; }               // 1
        public string ChassisNo { get; set; }         // ABC123XYZ456
        public string LoginMobile { get; set; }       // user.mobile@example.com
        public DateTime Date { get; set; }            // 2024-05-07
        public string Label { get; set; }             // Front bumper replacement
        public decimal Amount { get; set; }           // 450.75
        public string ImageBase64 { get; set; }       // ""
        public string Status { get; set; }             // Statut: pending, accepted, or rejected
    }
}
