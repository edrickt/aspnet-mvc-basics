// Edrick Tamayo
// Thursday 3:30PM
// 12/18/20
namespace cis237_assignment6.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Beverage
    {
        [StringLength(10)]
        public string id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(20)]
        public string pack { get; set; }

        [Required(ErrorMessage = "Please enter a price decimal")]
        [Column(TypeName = "money")]
        public decimal price { get; set; }

        public bool active { get; set; }
    }
}
