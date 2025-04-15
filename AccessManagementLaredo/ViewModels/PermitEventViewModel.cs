using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementLaredo.ViewModels
{
    public class PermitEventViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public int PermitRequestId { get; set; }
        public int EventTypeCode { get; set; }
        public string EventTypeName { get; set; }
        //public string UserId { get; set; }
        public string UserName { get; set; } 
        //public string UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        public string EventComment { get; set; }
        //public string StatusCode { get; set; }
        //public string StatusName { get; set; }
    }
}
