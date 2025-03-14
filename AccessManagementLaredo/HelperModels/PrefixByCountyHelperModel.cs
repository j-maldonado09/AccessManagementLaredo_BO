using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementLaredo.HelperModels
{
	// Used in NewRequestController, GetPrefixesByCounty method to obtain unique prefixes
	// and set them in a ViewData for Highway Prefix dropdown list in View 
	public class PrefixByCountyHelperModel
    {
        public int CountyId { get; set; }
        public int PrefixId { get; set; }
        public string PrefixCode { get; set; }
		public string CountyPrefix { get; set; }

	}
}
