using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForestCore.Models
{
    public class IndexViewModelPage
    {
        public IEnumerable<Product> Products { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
