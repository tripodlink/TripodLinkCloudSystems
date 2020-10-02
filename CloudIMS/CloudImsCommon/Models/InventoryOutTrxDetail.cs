using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Models
{
    [Table("inventoryout_trx_detail")]
    public class InventoryOutTrxDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int itoh_ID { get; set; }

        [Key]
        [Required]
        [Column("itoh_trxno")]
        [MaxLength(100, ErrorMessage = "TransactionNo cannot exceed 10 characters.")]
        public String TransactionNo { get; set; }

        [Required]
        [Column("itoh_item_id")]
        [MaxLength(100, ErrorMessage = "Item ID cannot exceed 100 characters.")]
        public String ItemID { get; set; }

        [Required]
        [Column("itoh_unit")]
        [MaxLength(100, ErrorMessage = "Unit cannot exceed 100 characters.")]
        public String Unit { get; set; }

        [Required]
        [Column("itoh_in_trxno")]
        [MaxLength(100, ErrorMessage = "In Transaction Number cannot exceed 100 characters.")]
        public String In_TrxNo { get; set; }

        [Required]
        [Column("itoh_quantity")]
        public double Quantity { get; set; }

        [Column("itoh_remarks")]
        [MaxLength(300, ErrorMessage = "Remarks cannot exceed 300 characters.")]
        public String Remarks { get; set; }

        [Required]
        [Column("itoh_mincount")]
        public double MinCount { get; set; }

    }
}
