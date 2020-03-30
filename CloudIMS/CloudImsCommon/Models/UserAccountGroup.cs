using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudImsCommon.Models
{
    [Table("user_account_group")]
    public class UserAccountGroup
    {
        [Key]
        [Column("uag_user_id")]
        [MaxLength(20)]
        [Required]
        public String UserAccountID { get; set; }

        [Key]
        [Column("uag_group_id")]
        [MaxLength(20)]
        [Required]
        public String UserGroupID { get; set; }
    }
}
