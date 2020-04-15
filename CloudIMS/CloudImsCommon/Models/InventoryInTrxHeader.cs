using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Models
{
    [Table("inventoryin_trx_header")]
    public class InventoryInTrxHeader
    {
        [Key]
        [Required]
        [Column("trxno")]
        [MaxLength(100, ErrorMessage = "TransactionNo cannot exceed 100 characters.")]
        public String TransactionNo { get; set; }

       
        [Column("trx_date")]
        public DateTime TransactionDate { get; set; }

      
        [Column("rcvd_date")]
        public DateTime ReceivedDate { get; set; }


        [Column("rcvd_by")]
        [MaxLength(100, ErrorMessage = "ReceivedBy cannot exceed 100 characters.")]
        public String ReceivedBy { get; set; }

      
        [Column("po_number")]
        [MaxLength(100, ErrorMessage = "PONumber cannot exceed 100 characters.")]
        public String PONumber { get; set; }

     
        [Column("invoice_number")]
        [MaxLength(100, ErrorMessage = "InvoiceNo cannot exceed 100 characters.")]
        public String InvoiceNo { get; set; }

       
        [Column("ref_number")]
        [MaxLength(100, ErrorMessage = "ReferenceNo cannot exceed 100 characters.")]
        public String ReferenceNo { get; set; }

        
        [Column("doc_number")]
        [MaxLength(100, ErrorMessage = "DocumnetNo cannot exceed 100 characters.")]
        public String DocumnetNo { get; set; }

       
        [Column("supplier")]
        [MaxLength(100, ErrorMessage = "Supplier cannot exceed 100 characters.")]
        public String Supplier { get; set; }
            
     
        [Column("remarks")]
        [MaxLength(100, ErrorMessage = "Remarks cannot exceed 100 characters.")]
        public String Remarks { get; set; }



    }
}
