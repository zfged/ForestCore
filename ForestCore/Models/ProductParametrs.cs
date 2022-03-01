using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForestCore.Models
{
    public class ProductParametrs
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
