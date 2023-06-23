using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;
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
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; } 

        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")] //Next, we need the navigation property.We will have to add the foreign key. And there we have to say that company ID is the navigation property for this company that we have.
        [ValidateNever]
        public Company? Company { get; set; }

        [NotMapped] //it is not pushed to database
        public string Role { get; set; }
    }
}
