using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessManagementLaredo.ViewModels
{
    public class HighwayViewModel
    {
        public int Id { get; set; } 
        public int PrefixId { get; set; }
        public string PrefixCode { get; set; }
        public string Number { get; set; }
    }
}
