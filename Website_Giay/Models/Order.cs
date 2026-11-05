using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website_Giay.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(20)]
        public string CustomerPhone { get; set; }

        [StringLength(150)]
        public string CustomerEmail { get; set; }

        [Required]
        [StringLength(255)]
        public string ShippingAddress { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public decimal TotalAmount { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; } = "COD"; // COD, BankTransfer, Momo

        [StringLength(50)]
        public string OrderStatus { get; set; } = "Đang xử lý"; // Đang xử lý, Đang giao, Đã giao, Đã hủy

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
