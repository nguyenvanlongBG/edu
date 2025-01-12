using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bg.EduSocial.Constract.Authen
{
    public class LoginRequest
    {
        [Required]   
        public string user_name { get; set; }
        [Required]
        public string password { get; set; }

    }
}
