using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ForestCore.Models
{
    [Table("Product")]

    public class Product
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }


        public string MainPhoto { get; set; }

        public decimal Price { get; set; }

        [Required]
        public bool Top { get; set; }

        public IEnumerable<ProductPhotos> Images { get; set; }   

        public int? CategoryId { get; set; } 
        public virtual Category Category { get; set; }
    }
}
