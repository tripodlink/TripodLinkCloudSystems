using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CloudCmsCommon.Models
{
    [Table("user_account_group")]
    public class UserAccountGroup
    {
        [Key]
        [MaxLength(20)]
        [Required]
        public String UserAccountID { get; set; }
        public UserAccount UserAccount { get; set; }

        [Key]
        [MaxLength(20)]
        [Required]
        public String UserGroupID { get; set; }
        public UserGroup UserGroup { get; set; }
    }
}
