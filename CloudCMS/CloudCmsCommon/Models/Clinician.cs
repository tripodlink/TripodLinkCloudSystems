using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CloudCmsCommon.Models
{
    [Table("clinician")]
    public class Clinician
    {
        [Key]
        [Column("clin_id")]
        [Display(Name = "Clinician ID")]
        [Required(ErrorMessage = "Clinician ID is required.")]
        [MaxLength(20, ErrorMessage = "Clinician ID cannot exceed 20 characters.")]
        public String ID { get; set; }


        [Required(ErrorMessage = "Clinician name is required.")]
        [Display(Name = "Clinician Name")]
        [Column("clin_name")]
        [MaxLength(100, ErrorMessage = "Clinician name cannot exceed 100 characters.")]
        public String Name { get; set; }


        [Column("clin_email")]
        [MaxLength(100, ErrorMessage = "Clinician email cannot exceed 100 characters.")]
        [RegularExpression(pattern: @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email format.")]
        public String Email { get; set; }


        [Column("clin_mobile_no")]
        [MaxLength(40, ErrorMessage = "Mobile number cannot exceed 40 characters.")]
        public String MobileNo { get; set; }
    }
}
