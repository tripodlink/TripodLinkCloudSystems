using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Models
{
    [Table("manufacturer")]
    public class Manufacturer
    {
        [Key]
        [Required]
        [Column("m_id")]
        [MaxLength(10, ErrorMessage = "ID cannot exceed 10 characters.")]
        public String ID { get; set; }

        [Required]
        [Column("m_item_group_name")]
        [MaxLength(100, ErrorMessage = "Item Group Name cannot exceed 100 characters.")]
        public String ManufactName { get; set; }
    }
}
