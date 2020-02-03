using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CloudCmsCommon.Models
{
    [Table("program_folder")]
    public class ProgramFolder
    {
        [Key]
        [Column("pf_folder_id")]
        [MaxLength(10)]
        public String ID { get; set; }


        [Column("pf_folder_name")]
        [MaxLength(100)]
        [Required]
        public String Name { get; set; }


        [Column("pf_root")]
        [MaxLength(10)]
        [Required]
        public String ProgramRootID { get; set; }
        public ProgramRoot ProgramRoot { get; set; }


        [Column("pf_module")]
        [MaxLength(10)]
        [Required]
        public String ModuleID { get; set; }


        [Column("pf_module_route")]
        [MaxLength(10)]
        [Required]
        public String ModuleRouteAttribute { get; set; }


        [Column("pf_folder_route")]
        [MaxLength(25)]
        [Required]
        public String RouteAttribute { get; set; }

        [Column("pf_icon_type")]
        [MaxLength(10)]
        public String IconType { get; set; }


        [Column("pf_icon_provider")]
        [MaxLength(20)]
        public String IconProvider { get; set; }


        [Column("pf_icon")]
        [MaxLength(10)]
        public String Icon { get; set; }


        [Column("pf_seqno")]
        [Required]
        public int SequenceNo { get; set; }
        
        IEnumerable<ProgramMenu> ProgramMenus { get; set; }

    }
}
