using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementLaredo.HelperModels
{
    // Class used for PermitEvent class (is it needed???)
    public class PermitEventHelperModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public int PermitRequestId { get; set; }
        public string PermitEventTypeCode { get; set;}
        public string UserName { get; set; }
        public string UserRoleName { get; set; }
        public string? PermitEventComment { get; set; }

    }
}
