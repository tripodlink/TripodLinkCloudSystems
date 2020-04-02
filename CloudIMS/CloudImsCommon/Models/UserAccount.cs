using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CloudImsCommon.Models
{
    [Table("user_account")]
    public class UserAccount
    {
        [Key]
        [Column("ua_user_id")]
        public String UserID { get; set; }

        [Required]
        [Column("ua_user_name")]
        public String UserName { get; set; }

        [Required]
        [Column("ua_password")]
        [MaxLength(100)]
        public String Password { get; set; }

        [Required]
        [Column("ua_is_active", TypeName = "TINYINT")]
        public Boolean IsActive { get; set; } = true;

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

        [NotMapped]
        public IEnumerable<UserAccountGroup> UserAccountGroups { get; set; }

        [NotMapped]
        public IEnumerable<UserGroup> UserGroups { get; set; }

        [NotMapped]
        public IEnumerable<ProgramFolder> ProgramFolders { get; set; }

        [NotMapped]
        public IEnumerable<ProgramMenu> ProgramMenus { get; set; }

        // Token to for authentication
        [NotMapped]
        public string Token { get; set; }
    }
}
