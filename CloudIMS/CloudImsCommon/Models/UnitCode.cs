using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CloudImsCommon.Models
{
    [Table("unit_code")]
    public class UnitCode
    {
        [Key]
        [Column("uc_code")]
        [Display(Name = "Code")]
        [MaxLength(3)]
        public String Code { get; set; }

        [Required(ErrorMessage = "Unit description is required.")]
        [Display(Name = "Description")]
        [Column("uc_description")]
        [MaxLength(20, ErrorMessage = "Unit description cannot exceed 20 characters.")]
        public String Description { get; set; }

        [Column("uc_short_description")]
        [MaxLength(10)]
        public String ShortDescription { get; set; }
    }
}
