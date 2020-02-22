using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CloudImsCommon.Models
{
    [Table("supplier")]
    public class Supplier 
    {
        [Key]
        [Column("sup_id")]
        [Display(Name = "Supplier ID")]
        [Required(ErrorMessage = "Supplier ID is required.")]
        [MaxLength(10, ErrorMessage = "Supplier ID cannot exceed 10 characters.")]
        public String ID { get; set; }


        [Required(ErrorMessage = "Supplier name is required.")]
        [Display(Name = "Supplier Name")]
        [Column("sup_name")]
        [MaxLength(100, ErrorMessage = "Supplier name cannot exceed 100 characters.")]
        public String Name { get; set; }
    }
}
