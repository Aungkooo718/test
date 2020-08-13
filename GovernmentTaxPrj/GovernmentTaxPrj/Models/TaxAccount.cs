using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GovernmentTaxPrj.Models
{
    public class TaxAccount
    {
        [Key]
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime OpenDate { get; set; }
    }
}
