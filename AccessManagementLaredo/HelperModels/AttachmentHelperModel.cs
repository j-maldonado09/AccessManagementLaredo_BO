using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementLaredo.HelperModels
{
	public class AttachmentHelperModel
	{
        public int Id { get; set; }
		public string Name { get; set; }
		public int AttachmentTypeId { get; set; }
		//public int PermitRequestId { get; set; }
        public string? Comment { get; set; }
    }
}
