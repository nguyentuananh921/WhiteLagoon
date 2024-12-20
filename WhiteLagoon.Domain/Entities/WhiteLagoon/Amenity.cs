﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteLagoon.Domain.Entities.WhiteLagoon
{
    public class Amenity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Amenity Name")]
        public string Name { get; set; }
        public string? Description { get; set; }

        #region ForeignKey to Villa
        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        [ValidateNever]  //using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
        public Villa Villa { get; set; }
        #endregion
    }
}
