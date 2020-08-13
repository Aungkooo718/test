using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GovernmentTaxPrj.Models
{
    public class TaxTransaction
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Region")]
        public long IncomeTypeId { get; set; }
        public IncomeType IncomeType { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TaxAmount { get; set; }


    }
}
