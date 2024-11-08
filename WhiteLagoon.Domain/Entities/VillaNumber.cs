using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WhiteLagoon.Domain.Entities
{
    public class VillaNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]//Not create ID automatically
        //public int Id { get; set; }

        [Display(Name ="Villa Number")]
        public int Villa_Number { get; set; }

        public string? SpecialDetails { get; set; }

        #region ForeignKey to Villa
        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        [ValidateNever]  //using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
        public Villa Villa { get; set; }
        #endregion
        
    }
}
