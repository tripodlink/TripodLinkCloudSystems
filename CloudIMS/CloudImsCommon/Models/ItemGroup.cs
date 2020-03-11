using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Models
{
    [Table("item_group")]
    public class ItemGroup
    {
        [Key]
        [Required]
        [Column("ig_id")]
        [MaxLength(10, ErrorMessage = "ID cannot exceed 10 characters.")]
        public String ID { get; set; }

        [Required]
        [Column("ig_item_group_name")]
        [MaxLength(100, ErrorMessage = "Item Group Name cannot exceed 100 characters.")]
        public String ItemGroupName { get; set; }
    }
}
