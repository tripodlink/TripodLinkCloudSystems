using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace CloudCmsCommon.Models
{
    [Table("item_master")]
    public class Item
    {
        [Key]
        [Column("im_code")]
        [MaxLength(10, ErrorMessage = "Item code cannot exceed 10 characters.")]
        public String Code { get; set; }

        [Required]
        [Column("im_name")]
        [MaxLength(100, ErrorMessage = "Item name cannot exceed 100 characters.")]
        public String Name { get; set; }

        [Required]
        [Column("im_host_code")]
        [MaxLength(20, ErrorMessage = "Host code cannot exceed 20 characters.")]
        public String HostCode { get; set; }

        [Required]
        [Column("im_category")]
        [MaxLength(1)]
        public String Category { get; set; }

        [Required]
        [Column("im_type")]
        [MaxLength(3)]
        public String Type { get; set; }

        [Required]
        [Column("im_cost_center")]
        [MaxLength(10)]
        public String CostCenter { get; set; }

        [Column("im_remarks")]
        [MaxLength(500, ErrorMessage = "Remarks cannot exceed 500 characters.")]
        public String Remarks { get; set; }

        [Required]
        [Column("im_editable_price", TypeName = "TINYINT")]
        public Boolean IsEditablePrice { get; set; } = true;

        [Required]
        [Column("im_active", TypeName = "TINYINT")]
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
    }
}
