using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website_Giay.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        [StringLength(255)]
        public string ProductName { get; set; }

        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [StringLength(50)]
        public string TargetAudience { get; set; } = "Unisex"; // Nam, Nữ, Unisex

        [StringLength(100)]
        public string ProductType { get; set; }

        public decimal Price { get; set; }

        public decimal OldPrice { get; set; }

        public double Rating { get; set; } = 5.0;

        public int ReviewCount { get; set; } = 0;

        public int Stock { get; set; } = 10;

        [StringLength(50)]
        public string Badge { get; set; } // new, hot, sale, etc.

        [StringLength(255)]
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public bool IsFeatured { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public virtual ICollection<ProductColor> Colors { get; set; } = new List<ProductColor>();

        public virtual ICollection<ProductSize> Sizes { get; set; } = new List<ProductSize>();
    }
}
