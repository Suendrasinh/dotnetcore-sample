using System;
using System.Collections.Generic;
using System.Text;

namespace MyGym.Core.Entity
{
    public class UpdateCustomerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
