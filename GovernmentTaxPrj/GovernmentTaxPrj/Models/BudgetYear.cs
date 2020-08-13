using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GovernmentTaxPrj.Models
{
    [Table("BudgetYear")]
    public class BudgetYear
    {
        [Key]
        public long Id { get; set; }
        [Display(Name ="Budget Year")]
        public string Name { get; set; }

        public string Code { get; set; }
        
        [Display(Name ="Budget StartDate")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name ="Budget EndDate")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }
    }
}
