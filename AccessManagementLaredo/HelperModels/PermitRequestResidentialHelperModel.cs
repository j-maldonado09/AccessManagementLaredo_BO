using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementLaredo.HelperModels
{
	// Class specific for residential requests, inherits PermitRequestHelperModel base class
	public class PermitRequestResidentialHelperModel : PermitRequestHelperModel
    {
        public int PermitRequestResidentialId { get; set; }
        public string PipeLength { get; set; }
        public string DrivewayWidth { get; set; }
        public string DistanceToCenter { get; set; }
        public string DistanceFromEdge { get; set; }
        public string RadiusOne { get; set; }
        public string RadiusTwo { get; set; }
        public string DrainagePipe { get; set; }
        public string DrainageStructure { get; set; }
        public string WidthGate { get; set; }
        public string WidthROW { get; set; }
        public string ThroatLength { get; set; }
    }
}
