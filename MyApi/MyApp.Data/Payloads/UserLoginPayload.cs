using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Data.Payloads
{
    public class UserLoginPayload
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
