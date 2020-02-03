using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDatabaseIdentityAuthentication.Models
{
    [Table("user_account")]
    public class UserAccount
    {
        [Key]
        [Column("ua_user_id")]
        [MaxLength(20, ErrorMessage = "User ID cannot exceed 20 characters.")]
        public String UserID { get; set; }

        [Column("ua_user_name")]
        [MaxLength(100, ErrorMessage = "User Name cannot exceed 100 characters.")]
        public String UserName { get; set; }

        [Column("ua_password")]
        [MaxLength(100)]
        public String Password { get; set; }
    }
}
