using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website_Giay.Models
{
    [Table("ProductColors")]
    public class ProductColor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColorId { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Required]
        [StringLength(100)]
        public string ColorName { get; set; }

        [StringLength(50)]
        public string HexCode { get; set; }

        [StringLength(255)]
        public string ImageUrl { get; set; }
    }
}
