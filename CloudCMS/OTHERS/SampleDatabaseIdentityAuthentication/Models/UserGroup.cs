using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDatabaseIdentityAuthentication.Models
{
    [Table("user_account_group")]
    public class UserGroup: IdentityRole
    {
    }
}
