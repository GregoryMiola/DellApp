using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DellApp.Models
{
    public class UserResults
    {
        public UserSys UserSys { get; set; }
        public List<Customer> Customers { get; set; }
    }
}