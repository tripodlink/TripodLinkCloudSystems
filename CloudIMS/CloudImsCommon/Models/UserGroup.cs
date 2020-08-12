using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CloudImsCommon.Models
{
    [Table("user_group")]
    public class UserGroup
    {
        [Key]
        [Column("ug_id")]
        [MaxLength(20)]
        public String ID { get; set; }


        [Column("ug_name")]
        [MaxLength(100)]
        [Required]
        public String Name { get; set; }

        [Required]
        [Column("ug_is_approver", TypeName = "TINYINT")]
        public Boolean IsApprover { get; set; } = true;

        [NotMapped]
        public IEnumerable<ProgramFolder> ProgramFolders { get; set; }

        [NotMapped]
        public IEnumerable<ProgramMenu> ProgramMenus { get; set; }
    }
}
