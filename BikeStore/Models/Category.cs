using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace BikeStore.Models
{
    public class Category
    {
        public int Id { get; set; }
         [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public int ProductsCount()
        {
            if (Products == null) return 0;
            return Products.Count();
        }
    }
}