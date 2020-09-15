using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Models
{
    [Table("item_tracking")]
    public class ItemTracking
    {
        [Key]
        [Required]
        [Column("it_trxno")]
        [MaxLength(25, ErrorMessage = "Transaction Number cannot exceed 10 characters.")]
        public String TransactionNo { get; set; }

        [Required]
        [Column("it_item_id")]
        [MaxLength(250, ErrorMessage = "Item ID cannot exceed 250 characters.")]
        public String ItemID { get; set; }

        [Required]
        [Column("it_lot_no")]
        [MaxLength(250, ErrorMessage = "Lot Number cannot exceed 250 characters.")]
        public String LotNo { get; set; }

        [Required]
        [Column("it_date_updated")]
        [MaxLength(250, ErrorMessage = "Date Updated cannot exceed 250 characters.")]
        public String DateUpdated { get; set; }

        [Required]
        [Column("it_location")]
        [MaxLength(250, ErrorMessage = "Location cannot exceed 250 characters.")]
        public String Location { get; set; }
    }
}
