﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WhiteLagoon.Domain.Entities.WhiteLagoon
{
    public class Villa
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }
        public string? Description { get; set; }

        [Display(Name="Price per night")]
        //https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-8.0
        [Range(10, 10000)]
        public double Price { get; set; }
        
        public int Sqft { get; set; }

        [Range(1, 10)]
        public int Occupancy { get; set; }

        [NotMapped] //Don't add to database
        public IFormFile ? Image { get; set; }

        [Display(Name ="Image URL")]
        public string? ImageUrl { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Updated_Date { get; set; }

        [ValidateNever] //One Villa can have many Amenity
        public IEnumerable<Amenity> VillaAmenities { get; set; }

        [ValidateNever] //One Villa can have many VillaNumber
        public IEnumerable<VillaNumber> VillaNumber { get; set; }
    }
}
