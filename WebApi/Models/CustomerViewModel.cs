using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class CustomerViewModel
    {
        public int CostumerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string EmailId { get; set; }
    }
}