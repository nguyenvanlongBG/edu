using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bg.EduSocial.Domain
{
    [Table("account")]
    public class AccountEntity: IdentityUser { 
    
        public Guid user_id { get; set; }
    }
}
