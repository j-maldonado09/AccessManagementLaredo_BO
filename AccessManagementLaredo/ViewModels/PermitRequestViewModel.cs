using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementLaredo.ViewModels
{
    public class PermitRequestViewModel
    {
        public int Id { get; set; }
        public string RequestorFullName { get; set; }
        public DateTime DateCreated { get; set;}
        public DateTime DateUpdated { get; set;}
        public string StatusTrafficCode { get; set; }
		public string StatusTrafficName { get; set; }
		//public string StatusTrafficNameInternal { get; set; }
		public string StatusTPDCode { get; set; }
		public string StatusTPDName { get; set; }
		//public string StatusTPDNameInternal { get; set; }
		public string StatusAreaOfficeCode { get; set; }
		public string StatusAreaOfficeName { get; set; }
        //public string StatusAreaOfficeNameInternal { get; set; }
        public string StatusExternalCode { get; set; }
        public string StatusExternalName { get; set; }
        public bool RequiresTraffic {  get; set; }
        public bool RequiresTPD { get; set; }

    }
}
