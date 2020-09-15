using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Models
{
    [Table("inventoryout_trx_header")]
    public class InventoryOutTrxHeader
    {
        [Key]
        [Required]
        [Column("itoh_trxno")]
        [MaxLength(100, ErrorMessage = "TransactionNo cannot exceed 100 characters.")]
        public String TransactionNo { get; set; }

        [Required]
        [Column("itoh_trx_date")]
        public DateTime TransactionDate { get; set; }


       [Column("itoh_issued_by")]
        [MaxLength(100, ErrorMessage = "Issued By cannot exceed 100 characters.")]
        public String IssuedBy { get; set; }

      
        [Column("itoh_issued_date")]
        public DateTime IssuedDate { get; set; }

        [Required]
        [Column("itoh_rcvd_by")]
        [MaxLength(100, ErrorMessage = "Received By cannot exceed 100 characters.")]
        public String ReceivedBy { get; set; }

        [Required]
        [Column("itoh_department")]
        [MaxLength(200, ErrorMessage = "Department cannot exceed 200 characters.")]
        public String Department { get; set; }

        [Required]
        [Column("itoh_ref_number")]
        [MaxLength(100, ErrorMessage = "ReferenceNo cannot exceed 100 characters.")]
        public String ReferenceNo { get; set; }

        [Column("itoh_remarks")]
        [MaxLength(100, ErrorMessage = "Remarks cannot exceed 100 characters.")]
        public String Remarks { get; set; }

        [Required]
        [Column("itoh_status")]
        [MaxLength(100, ErrorMessage = "Status cannot exceed 100 characters.")]
        public String Status { get; set; }

    }
}
