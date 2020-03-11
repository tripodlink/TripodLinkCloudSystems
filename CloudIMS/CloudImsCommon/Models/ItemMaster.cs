using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Models
{
    [Table("item_master")]
    public class ItemMaster
    {
        [Key]
        [Required]
        [Column("im_id")]
        [MaxLength(10, ErrorMessage = "ID cannot exceed 10 characters.")]
        public String ID { get; set; }

        [Required]
        [Column("im_item_name")]
        [MaxLength(250, ErrorMessage = "Item Name cannot exceed 250 characters.")]
        public String ItemName { get; set; }


        [Required]
        [Column("im_unit")]
        [MaxLength(100, ErrorMessage = "Unit name cannot exceed 100 characters.")]
        public String Unit { get; set; }

        [Required]
        [Column("im_supp")]
        [MaxLength(100, ErrorMessage = "Supplier name cannot exceed 100 characters.")]
        public String Supplier { get; set; }

        [Required]
        [Column("im_manufact")]
        [MaxLength(100, ErrorMessage = "Manufacturer name cannot exceed 100 characters.")]
        public String Manufacturer { get; set; }
    }
}
