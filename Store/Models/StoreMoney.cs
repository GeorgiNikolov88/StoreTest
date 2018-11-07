using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.Models
{
    class StoreMoney
    {
        [Key]
        public decimal StoreCashSupply { get; set; }
    }
}
