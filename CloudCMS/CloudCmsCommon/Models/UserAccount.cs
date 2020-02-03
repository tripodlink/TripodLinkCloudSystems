using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace CloudCmsCommon.Models
{
    [Table("user_account")]
    public class UserAccount
    {
        [Key]
        [Column("ua_user_id")]
        [MaxLength(20, ErrorMessage = "User ID cannot exceed 20 characters.")]
        public String UserID { get; set; }

        [Required]
        [Column("ua_user_name")]
        [MaxLength(100, ErrorMessage = "User name cannot exceed 100 characters.")]
        public String UserName { get; set; }

        [Required]
        [Column("ua_password")]
        [MaxLength(100)]
        public String Password { get; set; }

        [Required]
        [Column("ua_is_active", TypeName = "TINYINT")]
        public Boolean IsActive { get; set; } = true;

        [Required]
        [Column("ua_is_mb_user", TypeName = "TINYINT")]
        public Boolean IsMbUser { get; set; } = true;

        [Required]
        [Column("im_created_on")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Column("im_created_by")]
        [MaxLength(20)]
        public String CreatedBy { get; set; }

        [Required]
        [Column("im_updated_on")]
        public DateTime UpdatedOn { get; set; }

        [Required]
        [Column("im_updated_by")]
        [MaxLength(20)]
        public String UpdatedBy { get; set; }
    }
}
