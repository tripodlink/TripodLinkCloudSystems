using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Models
{
    [Table("department")]
    public class Department
    {
        [Key]
        [Required]
        [Column("d_id")]
        [MaxLength(15, ErrorMessage = "ID cannot exceed 15 characters.")]
        public String ID { get; set; }

        [Required]
        [Column("d_depertment_name")]
        [MaxLength(100, ErrorMessage = "Department Name cannot exceed 100 characters.")]
        public String DepartmentName { get; set; }
    }
}
