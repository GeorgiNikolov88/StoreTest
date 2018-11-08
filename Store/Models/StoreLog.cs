using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.Models
{
    public class StoreLog
    {        
        [Key]
        public int Id { get; set; }
        public string SalesLog { get; set; }
    }
}
