using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementLaredo.HelperModels
{
	// Used in NewRequestController, GetHighways method to obtain highway numbers
	// related to a specific county and prefix, and set them in a ViewData for Highway Number dropdown list in View 
	public class HighwayHelperModel
    {
        public int HighwayId { get; set; }
        public int CountyId { get; set; }
        public string HighwayNumber { get; set; }
        public int PrefixId { get; set; }
        public string PrefixCode { get; set; }
        public string CountyPrefix { get; set; }
    }
}
