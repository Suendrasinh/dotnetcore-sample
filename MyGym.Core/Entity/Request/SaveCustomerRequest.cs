using System;
using System.Collections.Generic;
using System.Text;

namespace MyGym.Core.Entity
{
    public class SaveCustomerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
