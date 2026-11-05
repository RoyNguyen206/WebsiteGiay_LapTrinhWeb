using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Website_Giay.ViewModels
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1;
        public string SelectedColor { get; set; }
        public string SelectedSize { get; set; }
        public decimal Total => Price * Quantity;
    }

    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
        public decimal SubTotal => Items.Sum(i => i.Total);
        public decimal ShippingFee => SubTotal > 0 && SubTotal < 2000000 ? 30000m : 0m;
        public decimal TotalAmount => SubTotal + ShippingFee;
        public int TotalQuantity => Items.Sum(i => i.Quantity);
    }

    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ nhận hàng")]
        [Display(Name = "Địa chỉ nhận hàng")]
        public string Address { get; set; }

        [Display(Name = "Ghi chú giao hàng")]
        public string Note { get; set; }

        [Display(Name = "Phương thức thanh toán")]
        public string PaymentMethod { get; set; } = "COD";

        public CartViewModel Cart { get; set; } = new CartViewModel();
    }

    public class OrderSuccessViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
    }
}
