using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.Models
{
    class StoreLog
    {
        //public int StoreId { get; set; }
        [Key]
        public int Id { get; set; }
        public string SalesLog { get; set; }
    }
}
