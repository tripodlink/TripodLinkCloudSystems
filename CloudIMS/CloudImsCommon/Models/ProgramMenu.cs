using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CloudImsCommon.Models
{
    [Table("program_menu")]
    public class ProgramMenu
    {
        [Key]
        [Column("pm_id")]
        [MaxLength(20)]
        public String ID { get; set; }


        [Column("pm_name")]
        [MaxLength(100)]
        [Required]
        public String Name { get; set; }


        [Column("pm_folder")]
        [MaxLength(10)]
        [Required]
        public String ProgramFolderID { get; set; }
        public ProgramFolder ProgramFolder { get; set; }


        [Column("pm_folder_route")]
        [MaxLength(25)]
        [Required]
        public String FolderRouteAttribute { get; set; }


        [Column("pm_controller_route")]
        [MaxLength(25)]
        [Required]
        public String ControllerRouteAttribute { get; set; }


        [Column("pm_action_route")]
        [MaxLength(25)]
        [Required]
        public String ActionRouteAttribute { get; set; }


        [Column("pm_icon_type")]
        [MaxLength(10)]
        public String IconType { get; set; }



        [Column("pm_icon_provider")]
        [MaxLength(20)]
        public String IconProvider { get; set; }



        [Column("pm_icon_name")]
        [MaxLength(100)]
        public String IconName { get; set; }


        [Column("pm_seqno")]
        public int SequenceNo { get; set; }
    }
}
