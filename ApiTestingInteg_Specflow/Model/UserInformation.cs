using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTestingInteg_Specflow.Model
{
    public class UserInformation
    {
        public int userId { get; set; }
        public Accountinformation accountinformation { get; set; }
    }

    public class Accountinformation
    {
        public int account { get; set; }
    }
}
