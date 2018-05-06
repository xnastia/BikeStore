using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BikeStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int VendorId { get; set; }       
        public Vendor Vendor { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } 

        public double AverageRating()
        {
            if (Comments == null || CommentsCount()==0) return 0;
            return Comments.Average(c => c.Rating);
        }
         public int CommentsCount() {
             if (Comments == null) return 0;
             return Comments.Count();
        }

    }
}