using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.Models
{
    class ProductTypes
    {
        [Key]
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }       
    }
}
