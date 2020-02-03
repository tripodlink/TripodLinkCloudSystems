using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace CloudCmsCommon.Models
{
    [Table("user_group_programs")]
    public class UserGroupProgram
    {
        [Key]
        [Column("ugp_user_group", Order = 0)]
        [MaxLength(20)]
        [Required]
        public String UserGroupID { get; set; }
        public UserGroup UserGroup { get; set; }


        [Key]
        [Column("ugp_program_id", Order = 1)]
        [MaxLength(20)]
        [Required]
        public String ProgramMenuID { get; set; }
        public ProgramMenu ProgramMenu { get; set; }

    }
}
