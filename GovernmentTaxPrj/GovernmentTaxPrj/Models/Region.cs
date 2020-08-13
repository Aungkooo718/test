using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GovernmentTaxPrj.Models
{
    public class Region
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Township> Townships { get; set; }
    }
}
