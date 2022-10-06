using MyApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Data.Payloads
{
    public class UpdateUserPayload
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
    }
}
