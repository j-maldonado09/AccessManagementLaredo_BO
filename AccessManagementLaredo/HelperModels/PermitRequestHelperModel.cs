using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementLaredo.HelperModels
{
	// Base class with default properties for a permit request
	// will be inherited by residential, commercial energy and commercial industrial HelperModels
	public class PermitRequestHelperModel
	{
		// PermitRequest Table
		public int PermitRequestId { get; set; }
		public int HighwayId { get; set; }
		public DateTime SubmitDate { get; set; }
		public int ConstructionTypeId { get; set; }
		public int HighwayPrefixId { get; set; }
		public string RequestType { get; set; }
		public int CountyId { get; set; }
		public int DistrictId { get; set; }
		public string RequestorFirstName { get; set; }
		public string RequestorLastName { get; set; }
		public string RequestorAddress { get; set; }
		public string RequestorCity { get; set; }
		public string RequestorZipCode { get; set; }
		public string RequestorState { get; set; }
		public string RequestorPhoneNumber { get; set; }
		public string Longitude { get; set; }
		public string Latitude { get; set; }
        public DateTime ConstructionStartDate { get; set; }
        public string LitigationFlag { get; set; }
		public string ConstructionFlag { get; set; }
		public string StateRepresentativeName { get; set; }
		public string StateRepresentativePhoneNumber { get; set; }
		//public string? OtherConditions { get; set; }
		//public bool VarianceOneFlag { get; set; }
		//public bool VarianceTwoFlag { get; set; }
		//public string? VarianceJustification { get; set; }
		//public bool VarianceDenialOneFlag { get; set; }
		//public bool VarianceDenialTwoFlag { get; set; }
		//public bool RequiresTraffic { get; set; }
		//public bool RequiresTPD { get; set; }
        public List<AttachmentHelperModel>? Attachments { get; set; }
        
		//public string StatusTraffic {  get; set; }
        //      public string StatusTPD { get; set; }
        //      public string StatusAreaOffice { get; set; }

    }
}
