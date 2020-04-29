using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Models
{
    [Table("inventoryin_trx_detail")]
    public class InventoryInTrxDetail
    {
        [Key]
        [Required]
        [Column("trxno")]
        [MaxLength(100, ErrorMessage = "TransactionNo cannot exceed 10 characters.")]
        public String TransactionNo { get; set; }

        [Required]
        [Column("item_id")]
        [MaxLength(100, ErrorMessage = "Item ID cannot exceed 100 characters.")]
        public String ItemID { get; set; }

        [Required]    
        [Column("unit")]    

        [MaxLength(100, ErrorMessage = "Unit cannot exceed 100 characters.")]
        public String Unit { get; set; }


        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }

        [Required]
        [Column("lotno")]
        [MaxLength(100, ErrorMessage = "LotNumber cannot exceed 100 characters.")]
        public String LotNumber { get; set; }

        [Required]
        [Column("exp_date")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Column("count")]
        public double Count { get; set; }

        [Required]
        [Column("remaning_count")]
        public double RemainigCount { get; set; }

    }
}
