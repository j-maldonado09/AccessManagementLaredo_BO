using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementLaredo.HelperModels
{
    // Class specific for internal users review of permit requests
    public class PermitRequestInternalReviewHelperModel
    {
        public int PermitRequestId { get; set; }
        public string? OtherConditions { get; set; }
        public bool VarianceOneFlag { get; set; }
        public bool VarianceTwoFlag { get; set; }
        public string? VarianceJustification { get; set; }
        public bool VarianceDenialOneFlag { get; set; }
        public bool VarianceDenialTwoFlag { get; set; }
        public bool RequiresTraffic { get; set; }
        public bool RequiresTPD { get; set; }
        // Comment belongs to event table
        public string? Comment { get; set; }
        // Property to detect if internal user approves/rejects/completes (buttons) review
        public string? ReviewAction { get; set; }
    }
}
