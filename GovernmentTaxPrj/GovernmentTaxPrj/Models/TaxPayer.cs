using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GovernmentTaxPrj.Models
{
    [Table("TaxPayer")]
    public class TaxPayer
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string TinNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Nrc { get; set; }
        [Required]
        public string TaxType { get; set; }
        [ForeignKey("Township")]
        public long TownshipId { get; set; }
        public Township Township { get; set; }
    }
}
