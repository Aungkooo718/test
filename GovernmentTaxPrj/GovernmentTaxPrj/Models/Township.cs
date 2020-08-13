using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GovernmentTaxPrj.Models
{
    public class Township
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<TaxPayer> TaxPayers { get; set; }
        [ForeignKey("Region")]
        public long RegionId { get; set; }
        public Region Region { get; set; }
    }
}
