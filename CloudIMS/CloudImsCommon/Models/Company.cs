using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Models
{
    [Table("company")]
    public class Company
    {
        [Required]
        [Column("id")]
        [MaxLength(3, ErrorMessage ="ID cannot exceed 3 characters.")]
        public String  ID { get; set; }

        [Required]
        [Column("company_id")]
        [MaxLength(10, ErrorMessage = "Company ID cannot exceed 10 characters.")]
        public String CompanyID { get; set; }


        [Required]
        [Column("company_name")]
        [MaxLength(100, ErrorMessage = "Company name cannot exceed 100 characters.")]
        public String  CompanyName { get; set; }
    }
}
