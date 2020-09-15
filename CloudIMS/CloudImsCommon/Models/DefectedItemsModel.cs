using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Models
{
    [Table("defected_items")]
    public class DefectedItemsModel
    {
        [Key]
        [Required]
        [Column("defect_trxno")]
        [MaxLength(100, ErrorMessage = "TransactionNo cannot exceed 10 characters.")]
        public String DefectTransactionNo { get; set; }

        [Required]
        [Column("item_id")]
        [MaxLength(100, ErrorMessage = "Item ID cannot exceed 100 characters.")]
        public String ItemID { get; set; }

        [Required]
        [Column("item_unit")]
        [MaxLength(100, ErrorMessage = "Item Unit cannot exceed 10 characters.")]
        public String ItemUnit { get; set; }

        [Required]
        [Column("quantity")]
        public Double Quantity { get; set; }

        [Required]    
        [Column("lot_number")]    
        [MaxLength(100, ErrorMessage = "Lot Number cannot exceed 100 characters.")]
        public String LotNumber { get; set; }

        [Required]
        [Column("trxno")]
        [MaxLength(100, ErrorMessage = "TransactionNo cannot exceed 10 characters.")]
        public String TransactionNo { get; set; }

        [Required]
        [Column("trx_date")]
        public DateTime TransactionDate { get; set; }

        [Required]
        [Column("remarks")]
        [MaxLength(100, ErrorMessage = "Remarks cannot exceed 10 characters.")]
        public String Remarks { get; set; }

        [Required]
        [Column("status")]
        [MaxLength(10, ErrorMessage = "Status cannot exceed 1 character")]
        public String Status { get; set; }



    }
}
