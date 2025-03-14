using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementLaredo.HelperModels
{
    // Class used for PermitEvent class (is it needed???)
    public class PermitRequestStatusHelperModel
    {
        public int Id { get; set; }
        public string? StatusAreaOfficeCode { get; set; }
        public string? StatusTrafficCode { get; set; }
        public string? StatusTPDCode { get; set; }
        public string? StatusExternalCode { get; set; }
    }
}