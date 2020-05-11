using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Models
{
    [Table("Auto_Number")]
    public class AutoNumber
    {
        [Key]
        [Required]
        [Column("an_type")]
        [MaxLength(100, ErrorMessage = "ID cannot exceed 20 characters.")]
        public String ID { get; set; }

        [Required]
        [Column("an_text_prefix")]
        [MaxLength(255, ErrorMessage = "Text Prefix cannot exceed 255 characters.")]
        public String Text_Prefix { get; set; }

        [Required]
        [Column("an_date_prefix")]
        [MaxLength(255, ErrorMessage = "Date Prefix cannot exceed 255 characters.")]
        public String Date_Prefix { get; set; }

        [Required]
        [Column("an_auto_length")]
        [MaxLength(255, ErrorMessage = "Auto Length cannot exceed 255 characters.")]
        public String Auto_Length { get; set; }

        [Required]
        [Column("an_last_value")]
        [MaxLength(255, ErrorMessage = "Last Value cannot exceed 255 characters.")]
        public String Last_Value { get; set; }

        [Required]
        [Column("an_current_year")]
        [MaxLength(255, ErrorMessage = "Current Year cannot exceed 255 characters.")]
        public String Current_year { get; set; }
    }
}
