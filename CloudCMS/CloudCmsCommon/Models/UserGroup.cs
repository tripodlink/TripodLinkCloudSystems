using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CloudCmsCommon.Models
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


        [Column("ug_module")]
        [MaxLength(10)]
        [Required]
        public String ModuleID { get; set; }
    }
}
