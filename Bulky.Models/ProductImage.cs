using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [ForeignKey("ProductId")]   //relation between product and productimage is one to many, one product can have multiple images
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
