﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CloudImsCommon.Models
{
    [Table("program_folder")]
    public class ProgramFolder
    {
        [Key]
        [Column("pf_folder_id")]
        [MaxLength(100)]
        public String ID { get; set; }


        [Column("pf_folder_name")]
        [MaxLength(255)]
        [Required]
        public String Name { get; set; }

                           
        [Column("pf_folder_route")]
        [MaxLength(255)]
        [Required]
        public String RouteAttribute { get; set; }

        [Column("pf_icon_type")]
        [MaxLength(255)]
        public String IconType { get; set; }


        [Column("pf_icon_provider")]
        [MaxLength(255)]
        public String IconProvider { get; set; }


        [Column("pf_icon")]
        [MaxLength(255)]
        public String Icon { get; set; }


        [Column("pf_seqno")]
        [Required]
        public int SequenceNo { get; set; }
        
        [NotMapped]
        IEnumerable<ProgramMenu> ProgramMenus { get; set; }

    }
}
