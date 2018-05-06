using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
namespace BikeStore.Models
{
    public class Vendor
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Name can't be blank")]
        public string Name { get; set; }
    }
}