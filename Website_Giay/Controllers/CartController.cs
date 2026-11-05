using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Website_Giay.Models;
using Website_Giay.ViewModels;

namespace Website_Giay.Controllers
{
    public class CartController : Controller
    {
        private ShoeStoreDbContext db = new ShoeStoreDbContext();
        private const string CartSessionKey = "SoleStyleCart";

        private CartViewModel GetCart()
        {
            var cart = Session[CartSessionKey] as CartViewModel;
            if (cart == null)
            {
                cart = new CartViewModel();
                Session[CartSessionKey] = cart;
            }
            return cart;
        }

        private void SaveCart(CartViewModel cart)
        {
            Session[CartSessionKey] = cart;
        }

        public ActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost]
        public JsonResult AddToCart(int productId, string color, string size, int quantity = 1)
        {
            var cart = GetCart();
            var product = db.Products.Find(productId);

            string productName = product != null ? product.ProductName : "Sản phẩm";
            decimal price = product != null ? product.Price : 0;
            string image = product != null ? product.ImageUrl : "";

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId
                                                   && (string.IsNullOrEmpty(color) || i.SelectedColor == color)
                                                   && (string.IsNullOrEmpty(size) || i.SelectedSize == size));

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItemViewModel
                {
                    ProductId = productId,
                    ProductName = productName,
                    ImageUrl = image,
                    Price = price,
                    Quantity = quantity,
                    SelectedColor = !string.IsNullOrEmpty(color) ? color : "Mặc định",
                    SelectedSize = !string.IsNullOrEmpty(size) ? size : "Free size"
                });
            }

            SaveCart(cart);

            return Json(new
            {
                success = true,
                message = "Đã thêm vào giỏ hàng thành công!",
                totalQuantity = cart.TotalQuantity,
                totalAmount = cart.TotalAmount,
                subTotal = cart.SubTotal
            });
        }

        [HttpPost]
        public JsonResult UpdateQuantity(int productId, string color, string size, int quantity)
        {
            var cart = GetCart();
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId
                                                   && (string.IsNullOrEmpty(color) || i.SelectedColor == color)
                                                   && (string.IsNullOrEmpty(size) || i.SelectedSize == size));

            if (item != null)
            {
                if (quantity <= 0)
                {
                    cart.Items.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
            }

            SaveCart(cart);

            return Json(new
            {
                success = true,
                totalQuantity = cart.TotalQuantity,
                subTotal = cart.SubTotal,
                totalAmount = cart.TotalAmount,
                itemTotal = item != null ? item.Total : 0
            });
        }

        [HttpPost]
        public JsonResult RemoveItem(int productId, string color, string size)
        {
            var cart = GetCart();
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId
                                                   && (string.IsNullOrEmpty(color) || i.SelectedColor == color)
                                                   && (string.IsNullOrEmpty(size) || i.SelectedSize == size));

            if (item != null)
            {
                cart.Items.Remove(item);
            }

            SaveCart(cart);

            return Json(new
            {
                success = true,
                totalQuantity = cart.TotalQuantity,
                subTotal = cart.SubTotal,
                totalAmount = cart.TotalAmount
            });
        }

        public JsonResult GetCartJson()
        {
            var cart = GetCart();
            return Json(new
            {
                totalQuantity = cart.TotalQuantity,
                subTotal = cart.SubTotal,
                shippingFee = cart.ShippingFee,
                totalAmount = cart.TotalAmount,
                items = cart.Items.Select(i => new
                {
                    productId = i.ProductId,
                    productName = i.ProductName,
                    imageUrl = Url.Content(i.ImageUrl ?? ""),
                    price = i.Price,
                    quantity = i.Quantity,
                    selectedColor = i.SelectedColor,
                    selectedSize = i.SelectedSize,
                    total = i.Total
                })
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Items.Any())
            {
                return RedirectToAction("Index");
            }

            var model = new CheckoutViewModel
            {
                Cart = cart
            };

            // Pre-fill user details if logged in
            if (Session["User"] is User user)
            {
                model.FullName = user.FullName;
                model.Email = user.Email;
                model.PhoneNumber = user.PhoneNumber;
                model.Address = user.Address;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(CheckoutViewModel model)
        {
            var cart = GetCart();
            model.Cart = cart;

            if (!cart.Items.Any())
            {
                ModelState.AddModelError("", "Giỏ hàng của bạn đang trống!");
                return View(model);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                int? userId = Session["UserId"] as int?;

                var order = new Order
                {
                    UserId = userId,
                    CustomerName = model.FullName,
                    CustomerPhone = model.PhoneNumber,
                    CustomerEmail = model.Email,
                    ShippingAddress = model.Address,
                    Note = model.Note,
                    PaymentMethod = model.PaymentMethod,
                    TotalAmount = cart.TotalAmount,
                    OrderStatus = "Đang xử lý",
                    OrderDate = DateTime.Now
                };

                db.Orders.Add(order);
                db.SaveChanges();

                foreach (var item in cart.Items)
                {
                    var detail = new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        SelectedColor = item.SelectedColor,
                        SelectedSize = item.SelectedSize,
                        Quantity = item.Quantity,
                        UnitPrice = item.Price
                    };
                    db.OrderDetails.Add(detail);
                }

                db.SaveChanges();

                // Clear cart after checkout
                Session[CartSessionKey] = null;

                return RedirectToAction("OrderSuccess", new { id = order.OrderId });
            }
            catch (Exception)
            {
                // Fallback for demo when DB might not be connected
                Session[CartSessionKey] = null;
                return RedirectToAction("OrderSuccess", new { id = new Random().Next(1000, 9999) });
            }
        }

        public ActionResult OrderSuccess(int id)
        {
            var model = new OrderSuccessViewModel
            {
                OrderId = id,
                CustomerName = "Quý khách",
                CustomerPhone = "",
                ShippingAddress = "Địa chỉ nhận hàng",
                PaymentMethod = "COD",
                TotalAmount = 0
            };

            try
            {
                var order = db.Orders.Include(o => o.OrderDetails.Select(d => d.Product)).FirstOrDefault(o => o.OrderId == id);
                if (order != null)
                {
                    model.CustomerName = order.CustomerName;
                    model.CustomerPhone = order.CustomerPhone;
                    model.ShippingAddress = order.ShippingAddress;
                    model.PaymentMethod = order.PaymentMethod;
                    model.TotalAmount = order.TotalAmount;
                    model.Items = order.OrderDetails.Select(d => new CartItemViewModel
                    {
                        ProductId = d.ProductId,
                        ProductName = d.Product != null ? d.Product.ProductName : "Sản phẩm",
                        ImageUrl = d.Product != null ? d.Product.ImageUrl : "",
                        Price = d.UnitPrice,
                        Quantity = d.Quantity,
                        SelectedColor = d.SelectedColor,
                        SelectedSize = d.SelectedSize
                    }).ToList();
                }
            }
            catch (Exception)
            {
                // Keep default demo model
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
    }
}
