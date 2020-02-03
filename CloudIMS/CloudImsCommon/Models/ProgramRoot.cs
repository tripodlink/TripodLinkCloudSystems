using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CloudImsCommon.Models
{
    [Table("program_root")]
    public class ProgramRoot
    {
        [Key]
        [Column("pr_id")]
        [MaxLength(10)]
        public String ID { get; set; }

        
        [Column("pr_name")]
        [MaxLength(100)]
        [Required]
        public String Name { get; set; }


        [Column("pr_seqno")]
        [Required]
        public int SequenceNo { get; set; }

        IEnumerable<ProgramFolder> ProgramFolders { get; set; }
    }
}
