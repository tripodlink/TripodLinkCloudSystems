using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace CloudImsCommon.Models
{
    [Table("event_table")]
    public class EventTable
    {
        [Key]
        [Column("et_code")]
        [MaxLength(20, ErrorMessage = "Event code cannot exceed 20 characters.")]
        public String Code { get; set; }

        [Required]
        [Column("et_description")]
        [MaxLength(100, ErrorMessage = "Event description cannot exceed 100 characters.")]
        public String Description { get; set; }

        [Column("et_remarks")]
        [MaxLength(500, ErrorMessage = "Event remarks cannot exceed 500 characters.")]
        public String Remarks { get; set; }
    }
}
