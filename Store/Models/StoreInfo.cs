using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.Models
{
    class StoreInfo
    {
        //public int StoreId { get; set; }
        [Key]
        public decimal StoreCash { get; set; }
        public string StoreLog { get; set; }
    }
}
