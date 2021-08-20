using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdEngineAPI.Models
{
    public class UserClientModel
    {
       public string email { get; set; }
       public string password { get; set; }

       public int companyId { get; set; }
    }
}
