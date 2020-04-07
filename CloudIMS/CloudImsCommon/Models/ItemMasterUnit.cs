using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Models
{
    [Table("item_master_unit")]
    public class ItemMasterUnit
    {
        [Key]
        [Required]
        [Column("imu_id")]
        [MaxLength(100, ErrorMessage = "ID cannot exceed 100 characters.")]
        public String ID { get; set; }

        [Key]
        [Required]
        [Column("imu_unit")]
        [MaxLength(250, ErrorMessage = "Item Master Unit Unit cannot exceed 250 characters.")]
        public String itemMasterUnitUnit { get; set; }

        [Column("im_item_conversion")]
        [MaxLength(250, ErrorMessage = "Item Master Unit Comversion cannot exceed 250 characters.")]
        public String itemMasterUnitConversion { get; set; }
    }
}
