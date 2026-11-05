using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Website_Giay.Models
{
    public class ShoeStoreDbInitializer : CreateDatabaseIfNotExists<ShoeStoreDbContext>
    {
        protected override void Seed(ShoeStoreDbContext context)
        {
            // Seed Brands
            var brands = new Dictionary<string, Brand>
            {
                { "Nike", new Brand { BrandName = "Nike", LogoUrl = "~/assets/img/logoNike.png", Description = "Thương hiệu thể thao hàng đầu thế giới" } },
                { "Adidas", new Brand { BrandName = "Adidas", LogoUrl = "~/assets/img/logoAdidas.png", Description = "Biểu tượng phong cách và thể thao từ Đức" } },
                { "Converse", new Brand { BrandName = "Converse", LogoUrl = "~/assets/img/logoConverse.png", Description = "Huyền thoại giày bóng rổ và phong cách đường phố" } },
                { "Puma", new Brand { BrandName = "Puma", LogoUrl = "~/assets/img/logoPuma.png", Description = "Tốc độ và phong cách năng động hiện đại" } },
                { "New Balance", new Brand { BrandName = "New Balance", LogoUrl = "~/assets/img/logoNewBalance.png", Description = "Chất lượng thủ công vượt trội và êm ái tối đa" } }
            };
            foreach (var b in brands.Values) context.Brands.Add(b);
            context.SaveChanges();

            // Seed Categories
            var categories = new Dictionary<string, Category>
            {
                { "Giày", new Category { CategoryName = "Giày", Slug = "giay", Description = "Các dòng giày thể thao, sneaker chính hãng" } },
                { "Dép", new Category { CategoryName = "Dép", Slug = "dep", Description = "Dép quai ngang, dép xỏ ngón tiện lợi và thời trang" } },
                { "Nón", new Category { CategoryName = "Nón", Slug = "non", Description = "Nón lưỡi trai, nón bucket phong cách" } },
                { "Phụ kiện", new Category { CategoryName = "Phụ kiện", Slug = "phu-kien", Description = "Balo, vớ, túi thể thao chính hãng" } }
            };
            foreach (var c in categories.Values) context.Categories.Add(c);
            context.SaveChanges();

            // Seed Default Users
            var adminUser = new User
            {
                FullName = "Quản Trị Viên",
                Email = "admin@solestyle.vn",
                Password = "admin",
                PhoneNumber = "0368068274",
                Address = "140 Lê Trọng Tấn, Tây Thạnh, Tân Phú, TP.HCM",
                Role = "Admin",
                CreatedDate = DateTime.Now
            };
            var customerUser = new User
            {
                FullName = "Nguyễn Văn An",
                Email = "an.nguyen@example.com",
                Password = "123",
                PhoneNumber = "0901234567",
                Address = "123 CMT8, Quận 3, TP.HCM",
                Role = "Customer",
                CreatedDate = DateTime.Now
            };
            context.Users.Add(adminUser);
            context.Users.Add(customerUser);
            context.SaveChanges();

            // Seed Products

            var p1 = new Product
            {
                ProductName = @"Giày Nike Air Jordan 1 Retro High OG",
                BrandId = brands.ContainsKey(@"Nike") ? brands[@"Nike"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Nam",
                ProductType = @"Sneakers bóng rổ",
                Price = 3990000m,
                OldPrice = 4590000m,
                Rating = 4.9,
                ReviewCount = 18,
                Stock = 6,
                Badge = @"new",
                ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (4) (16).png",
                Description = @"Thiết kế cổ cao, đệm êm và phối màu dễ mặc cho cả đi học, đi chơi hoặc phối streetwear.",
                IsFeatured = true,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p1);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p1.ProductId, ColorName = @"Hồng phấn", HexCode = @"#E6CFD1", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (4) (16).png" });
            context.ProductColors.Add(new ProductColor { ProductId = p1.ProductId, ColorName = @"Olive Green", HexCode = @"#767B66", ImageUrl = @"~/assets/img/BLAZER+MID+'77+OD+NBY (3).avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p1.ProductId, ColorName = @"Xám", HexCode = @"#9ca3af", ImageUrl = @"~/assets/img/BLAZER+MID+'77+OD+NBY (2).avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p1.ProductId, SizeValue = @"39" });
            context.ProductSizes.Add(new ProductSize { ProductId = p1.ProductId, SizeValue = @"40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p1.ProductId, SizeValue = @"41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p1.ProductId, SizeValue = @"42" });
            context.ProductSizes.Add(new ProductSize { ProductId = p1.ProductId, SizeValue = @"43" });
            context.SaveChanges();

            var p2 = new Product
            {
                ProductName = @"Giày Adidas Ultraboost 2026 Running",
                BrandId = brands.ContainsKey(@"Adidas") ? brands[@"Adidas"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Giày chạy bộ",
                Price = 4290000m,
                OldPrice = 0m,
                Rating = 4.8,
                ReviewCount = 12,
                Stock = 9,
                Badge = @"new",
                ImageUrl = @"~/assets/img/image (5).png",
                Description = @"Thân giày ôm chân, đế đàn hồi tốt, phù hợp tập luyện hằng ngày và di chuyển nhiều.",
                IsFeatured = true,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p2);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p2.ProductId, ColorName = @"Trắng", HexCode = @"#f8fafc", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (17)_ma2.png" });
            context.ProductColors.Add(new ProductColor { ProductId = p2.ProductId, ColorName = @"Đen", HexCode = @"#111827", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (18)_ma2.png" });
            context.ProductColors.Add(new ProductColor { ProductId = p2.ProductId, ColorName = @"Xanh ngọc", HexCode = @"#72D2C9", ImageUrl = @"~/assets/img/image (5).png" });
            context.ProductSizes.Add(new ProductSize { ProductId = p2.ProductId, SizeValue = @"38" });
            context.ProductSizes.Add(new ProductSize { ProductId = p2.ProductId, SizeValue = @"39" });
            context.ProductSizes.Add(new ProductSize { ProductId = p2.ProductId, SizeValue = @"40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p2.ProductId, SizeValue = @"41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p2.ProductId, SizeValue = @"42" });
            context.ProductSizes.Add(new ProductSize { ProductId = p2.ProductId, SizeValue = @"43" });
            context.ProductSizes.Add(new ProductSize { ProductId = p2.ProductId, SizeValue = @"44" });
            context.SaveChanges();

            var p3 = new Product
            {
                ProductName = @"Chuck Taylor All Star",
                BrandId = brands.ContainsKey(@"Converse") ? brands[@"Converse"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Giày cổ cao",
                Price = 1700000m,
                OldPrice = 0m,
                Rating = 0,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"Kinh điển",
                ImageUrl = @"~/assets/img/image (4).png",
                Description = @"Đôi giày biểu tượng nhất của Converse, Chuck Taylor All Star với thiết kế vải canvas bền bỉ và đế cao su đặc trưng.",
                IsFeatured = true,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p3);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p3.ProductId, ColorName = @"Đen", HexCode = @"#000000", ImageUrl = @"~/assets/img/image (4).png" });
            context.ProductColors.Add(new ProductColor { ProductId = p3.ProductId, ColorName = @"Trắng", HexCode = @"#FFFFFF", ImageUrl = @"~/assets/img/image (3).png" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"3.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"4" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"4.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"5.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"6" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"6.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"7" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"7.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"8" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"8.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"9" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"9.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"10" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"10.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"11" });
            context.ProductSizes.Add(new ProductSize { ProductId = p3.ProductId, SizeValue = @"12" });
            context.SaveChanges();

            var p4 = new Product
            {
                ProductName = @"Nike P-6000",
                BrandId = brands.ContainsKey(@"Nike") ? brands[@"Nike"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Giày thể thao",
                Price = 2929000m,
                OldPrice = 0m,
                Rating = 0,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"Được đánh giá cao",
                ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (10).png",
                Description = @"Giày Nike P-6000 với thiết kế lấy cảm hứng từ phong cách chạy bộ những năm 2000, kết hợp giữa sự thoải mái và vẻ ngoài thời thượng.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p4);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p4.ProductId, ColorName = @"Be", HexCode = @"#D2B48C", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (10).png" });
            context.ProductColors.Add(new ProductColor { ProductId = p4.ProductId, ColorName = @"Đen", HexCode = @"#000000", ImageUrl = @"~/assets/img/NIKE+P-6000 (1).avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p4.ProductId, ColorName = @"Trắng", HexCode = @"#FFFFFF", ImageUrl = @"~/assets/img/NIKE+P-6000 (2).avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p4.ProductId, SizeValue = @"EU 37.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p4.ProductId, SizeValue = @"EU 38.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p4.ProductId, SizeValue = @"EU 39" });
            context.ProductSizes.Add(new ProductSize { ProductId = p4.ProductId, SizeValue = @"EU 40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p4.ProductId, SizeValue = @"EU 40.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p4.ProductId, SizeValue = @"EU 41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p4.ProductId, SizeValue = @"EU 42" });
            context.ProductSizes.Add(new ProductSize { ProductId = p4.ProductId, SizeValue = @"EU 42.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p4.ProductId, SizeValue = @"EU 43" });
            context.ProductSizes.Add(new ProductSize { ProductId = p4.ProductId, SizeValue = @"EU 44" });
            context.ProductSizes.Add(new ProductSize { ProductId = p4.ProductId, SizeValue = @"EU 44.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p4.ProductId, SizeValue = @"EU 45" });
            context.ProductSizes.Add(new ProductSize { ProductId = p4.ProductId, SizeValue = @"EU 45.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p4.ProductId, SizeValue = @"EU 46" });
            context.ProductSizes.Add(new ProductSize { ProductId = p4.ProductId, SizeValue = @"EU 47" });
            context.SaveChanges();

            var p5 = new Product
            {
                ProductName = @"Giày bóng đá ULTRA 6 MATCH TT Unisex",
                BrandId = brands.ContainsKey(@"Puma") ? brands[@"Puma"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Giày bóng đá",
                Price = 1260000m,
                OldPrice = 2100000m,
                Rating = 0,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"Giảm giá 40%",
                ImageUrl = @"~/assets/img/Giày-bóng-đá-ULTRA-6-MATCH-TT-Unisex.avif",
                Description = @"Giày bóng đá hiệu năng cao với thiết kế hiện đại, hỗ trợ tối đa cho các trận đấu trên sân cỏ nhân tạo.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p5);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p5.ProductId, ColorName = @"Vàng neon", HexCode = @"#CCFF00", ImageUrl = @"~/assets/img/Giày-bóng-đá-ULTRA-6-MATCH-TT-Unisex.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p5.ProductId, ColorName = @"Hồng", HexCode = @"#FF69B4", ImageUrl = @"~/assets/img/Giày-bóng-đá-ULTRA-6-MATCH-TT-Unisex_2.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p5.ProductId, SizeValue = @"EUR 43" });
            context.SaveChanges();

            var p6 = new Product
            {
                ProductName = @"Dép Adidas Adilette Comfort",
                BrandId = brands.ContainsKey(@"Adidas") ? brands[@"Adidas"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Dép") ? categories[@"Dép"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Dép thể thao",
                Price = 990000m,
                OldPrice = 1290000m,
                Rating = 4.5,
                ReviewCount = 11,
                Stock = 10,
                Badge = @"-23%",
                ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (17).png",
                Description = @"Dép quai ngang êm, dễ mang sau khi tập luyện hoặc dùng hằng ngày.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p6);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p6.ProductId, ColorName = @"Đen", HexCode = @"#111827", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (18).png" });
            context.ProductColors.Add(new ProductColor { ProductId = p6.ProductId, ColorName = @"Trắng", HexCode = @"#ffffff", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (17).png" });
            context.ProductSizes.Add(new ProductSize { ProductId = p6.ProductId, SizeValue = @"36" });
            context.ProductSizes.Add(new ProductSize { ProductId = p6.ProductId, SizeValue = @"37" });
            context.ProductSizes.Add(new ProductSize { ProductId = p6.ProductId, SizeValue = @"38" });
            context.ProductSizes.Add(new ProductSize { ProductId = p6.ProductId, SizeValue = @"39" });
            context.ProductSizes.Add(new ProductSize { ProductId = p6.ProductId, SizeValue = @"40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p6.ProductId, SizeValue = @"41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p6.ProductId, SizeValue = @"42" });
            context.SaveChanges();

            var p7 = new Product
            {
                ProductName = @"Giày Adidas Grand Court 3.0 Sneakers",
                BrandId = brands.ContainsKey(@"Adidas") ? brands[@"Adidas"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Nam",
                ProductType = @"Giày tennis lifestyle",
                Price = 2000000m,
                OldPrice = 0m,
                Rating = 4.7,
                ReviewCount = 15,
                Stock = 8,
                Badge = @"new",
                ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (17)_ma2.png",
                Description = @"Form thấp dễ mang, lấy cảm hứng từ giày tennis, phù hợp đi học, đi làm hoặc dạo phố.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p7);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p7.ProductId, ColorName = @"Trắng", HexCode = @"#f8fafc", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (17)_ma2.png" });
            context.ProductColors.Add(new ProductColor { ProductId = p7.ProductId, ColorName = @"Đen", HexCode = @"#111827", ImageUrl = @"~/assets/img/Adizero_EVO_SL_Shoes_Grey_KJ1364_06_standard.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p7.ProductId, ColorName = @"Xám", HexCode = @"#9ca3af", ImageUrl = @"~/assets/img/Adizero_EVO_SL_Shoes_Grey_JS4489_06_standard.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p7.ProductId, SizeValue = @"39" });
            context.ProductSizes.Add(new ProductSize { ProductId = p7.ProductId, SizeValue = @"40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p7.ProductId, SizeValue = @"41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p7.ProductId, SizeValue = @"42" });
            context.ProductSizes.Add(new ProductSize { ProductId = p7.ProductId, SizeValue = @"43" });
            context.ProductSizes.Add(new ProductSize { ProductId = p7.ProductId, SizeValue = @"44" });
            context.SaveChanges();

            var p8 = new Product
            {
                ProductName = @"ADIZERO EVO SL SHOES",
                BrandId = brands.ContainsKey(@"Adidas") ? brands[@"Adidas"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Nam",
                ProductType = @"Giày chạy bộ",
                Price = 4000000m,
                OldPrice = 0m,
                Rating = 1.0,
                ReviewCount = 1,
                Stock = 20,
                Badge = @"mới",
                ImageUrl = @"~/assets/img/Adizero_EVO_SL_Shoes_Grey_KI9542_06_standard.avif",
                Description = @"Giày chạy bộ Adizero Evo SL với thiết kế nhẹ, tối ưu hóa cho hiệu suất chạy bộ hàng ngày.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p8);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p8.ProductId, ColorName = @"Trắng", HexCode = @"#F8F8FF", ImageUrl = @"~/assets/img/Adizero_EVO_SL_Shoes_Grey_KI9542_06_standard.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p8.ProductId, SizeValue = @"6.5 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p8.ProductId, SizeValue = @"7 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p8.ProductId, SizeValue = @"7.5 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p8.ProductId, SizeValue = @"8 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p8.ProductId, SizeValue = @"8.5 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p8.ProductId, SizeValue = @"9 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p8.ProductId, SizeValue = @"9.5 UK" });
            context.SaveChanges();

            var p9 = new Product
            {
                ProductName = @"Giày New Balance 530 Essential",
                BrandId = brands.ContainsKey(@"New Balance") ? brands[@"New Balance"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Giày lifestyle",
                Price = 2390000m,
                OldPrice = 2790000m,
                Rating = 4.8,
                ReviewCount = 14,
                Stock = 7,
                Badge = @"-14%",
                ImageUrl = @"~/assets/img/mr530asm_nb_03_i.webp",
                Description = @"Thiết kế retro chạy bộ, nhẹ chân và dễ phối với quần jeans, cargo hoặc đồ thể thao.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p9);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p9.ProductId, ColorName = @"Be", HexCode = @"#D2B48C", ImageUrl = @"~/assets/img/mr530asm_nb_03_i.webp" });
            context.ProductColors.Add(new ProductColor { ProductId = p9.ProductId, ColorName = @"Xám", HexCode = @"#9ca3af", ImageUrl = @"~/assets/img/mr530ema_nb_03_i.webp" });
            context.ProductColors.Add(new ProductColor { ProductId = p9.ProductId, ColorName = @"Đen", HexCode = @"#000000", ImageUrl = @"~/assets/img/u5309tn_nb_03_i.webp" });
            context.ProductSizes.Add(new ProductSize { ProductId = p9.ProductId, SizeValue = @"38" });
            context.ProductSizes.Add(new ProductSize { ProductId = p9.ProductId, SizeValue = @"39" });
            context.ProductSizes.Add(new ProductSize { ProductId = p9.ProductId, SizeValue = @"40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p9.ProductId, SizeValue = @"41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p9.ProductId, SizeValue = @"42" });
            context.ProductSizes.Add(new ProductSize { ProductId = p9.ProductId, SizeValue = @"43" });
            context.SaveChanges();

            var p10 = new Product
            {
                ProductName = @"Air Jordan Mule Golf Shoes",
                BrandId = brands.ContainsKey(@"Nike") ? brands[@"Nike"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Nam",
                ProductType = @"Giày Golf",
                Price = 2929000m,
                OldPrice = 0m,
                Rating = 0,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"Được đánh giá cao",
                ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (15).png",
                Description = @"Giày golf Air Jordan Mule với thiết kế không gót tiện lợi, mang lại sự thoải mái và phong cách đặc trưng trên sân golf.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p10);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p10.ProductId, ColorName = @"Trắng", HexCode = @"#FFFFFF", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (15).png" });
            context.ProductColors.Add(new ProductColor { ProductId = p10.ProductId, ColorName = @"Nâu", HexCode = @"#8B4513", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (14).png" });
            context.ProductSizes.Add(new ProductSize { ProductId = p10.ProductId, SizeValue = @"EU 40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p10.ProductId, SizeValue = @"EU 40.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p10.ProductId, SizeValue = @"EU 41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p10.ProductId, SizeValue = @"EU 42" });
            context.ProductSizes.Add(new ProductSize { ProductId = p10.ProductId, SizeValue = @"EU 42.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p10.ProductId, SizeValue = @"EU 43" });
            context.ProductSizes.Add(new ProductSize { ProductId = p10.ProductId, SizeValue = @"EU 44" });
            context.ProductSizes.Add(new ProductSize { ProductId = p10.ProductId, SizeValue = @"EU 44.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p10.ProductId, SizeValue = @"EU 45" });
            context.ProductSizes.Add(new ProductSize { ProductId = p10.ProductId, SizeValue = @"EU 46" });
            context.ProductSizes.Add(new ProductSize { ProductId = p10.ProductId, SizeValue = @"EU 47" });
            context.ProductSizes.Add(new ProductSize { ProductId = p10.ProductId, SizeValue = @"EU 47.5" });
            context.SaveChanges();

            var p15 = new Product
            {
                ProductName = @"Adizero EVO SL Woven Shoes",
                BrandId = brands.ContainsKey(@"Adidas") ? brands[@"Adidas"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Men",
                ProductType = @"Performance",
                Price = 3600000m,
                OldPrice = 4000000m,
                Rating = 4.8,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"-10%",
                ImageUrl = @"~/assets/img/Giay_Chay_Bo_Runfalcon_5_Mau_xanh_da_troi_JR5521_06_standard.avif",
                Description = @"Giày chạy bộ Adizero EVO SL Woven dành cho nam với thiết kế siêu nhẹ, hỗ trợ tốc độ và hiệu suất tối đa trong luyện tập cũng như thi đấu.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p15);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p15.ProductId, ColorName = @"Xanh navy đậm", HexCode = @"#1A2346", ImageUrl = @"~/assets/img/Giay_Chay_Bo_Runfalcon_5_Mau_xanh_da_troi_JR5521_06_standard.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p15.ProductId, SizeValue = @"39" });
            context.ProductSizes.Add(new ProductSize { ProductId = p15.ProductId, SizeValue = @"40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p15.ProductId, SizeValue = @"41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p15.ProductId, SizeValue = @"42" });
            context.ProductSizes.Add(new ProductSize { ProductId = p15.ProductId, SizeValue = @"43" });
            context.ProductSizes.Add(new ProductSize { ProductId = p15.ProductId, SizeValue = @"44" });
            context.ProductSizes.Add(new ProductSize { ProductId = p15.ProductId, SizeValue = @"45" });
            context.SaveChanges();

            var p16 = new Product
            {
                ProductName = @"Nike Pegasus 1 Golf Shoes",
                BrandId = brands.ContainsKey(@"Nike") ? brands[@"Nike"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Golf Shoes",
                Price = 4699000m,
                OldPrice = 0m,
                Rating = 4.9,
                ReviewCount = 21,
                Stock = 15,
                Badge = @"new",
                ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3).png",
                Description = @"Nike Pegasus 1 Golf mang lại sự kết hợp giữa đệm ReactX êm ái và đế bám sân ổn định, phù hợp cho những vòng golf dài với cảm giác thoải mái và hiệu suất cao.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p16);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p16.ProductId, ColorName = @"Trắng", HexCode = @"#FFFFFF", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3).png" });
            context.ProductSizes.Add(new ProductSize { ProductId = p16.ProductId, SizeValue = @"40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p16.ProductId, SizeValue = @"40.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p16.ProductId, SizeValue = @"41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p16.ProductId, SizeValue = @"42" });
            context.ProductSizes.Add(new ProductSize { ProductId = p16.ProductId, SizeValue = @"42.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p16.ProductId, SizeValue = @"43" });
            context.ProductSizes.Add(new ProductSize { ProductId = p16.ProductId, SizeValue = @"44" });
            context.ProductSizes.Add(new ProductSize { ProductId = p16.ProductId, SizeValue = @"44.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p16.ProductId, SizeValue = @"45" });
            context.ProductSizes.Add(new ProductSize { ProductId = p16.ProductId, SizeValue = @"46" });
            context.ProductSizes.Add(new ProductSize { ProductId = p16.ProductId, SizeValue = @"47" });
            context.SaveChanges();

            var p17 = new Product
            {
                ProductName = @"Converse X 1908 Jogger",
                BrandId = brands.ContainsKey(@"Converse") ? brands[@"Converse"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Limited Edition",
                Price = 2800000m,
                OldPrice = 0m,
                Rating = 4.9,
                ReviewCount = 12,
                Stock = 8,
                Badge = @"LIMITED",
                ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (2).png",
                Description = @"Converse X 1908 Jogger là phiên bản giới hạn lấy cảm hứng từ dòng giày chạy bộ cổ điển, kết hợp phong cách retro với chất liệu cao cấp và thiết kế hiện đại dành cho cả nam và nữ.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p17);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p17.ProductId, ColorName = @"Xanh lam", HexCode = @"#6BA6C8", ImageUrl = @"~/assets/img/0882-CONA21432C12W010-2.webp" });
            context.ProductColors.Add(new ProductColor { ProductId = p17.ProductId, ColorName = @"Xanh olive", HexCode = @"#7A8B3C", ImageUrl = @"~/assets/img/0882-CONA21431C00G010-2.webp" });
            context.ProductColors.Add(new ProductColor { ProductId = p17.ProductId, ColorName = @"Trắng", HexCode = @"#F5F5F5", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (2).png" });
            context.ProductColors.Add(new ProductColor { ProductId = p17.ProductId, ColorName = @"Vàng kem", HexCode = @"#EFD87A", ImageUrl = @"~/assets/img/0882-CONA21430C00Y010-2.webp" });
            context.ProductSizes.Add(new ProductSize { ProductId = p17.ProductId, SizeValue = @"36" });
            context.ProductSizes.Add(new ProductSize { ProductId = p17.ProductId, SizeValue = @"37" });
            context.ProductSizes.Add(new ProductSize { ProductId = p17.ProductId, SizeValue = @"38" });
            context.ProductSizes.Add(new ProductSize { ProductId = p17.ProductId, SizeValue = @"39" });
            context.ProductSizes.Add(new ProductSize { ProductId = p17.ProductId, SizeValue = @"40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p17.ProductId, SizeValue = @"41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p17.ProductId, SizeValue = @"42" });
            context.ProductSizes.Add(new ProductSize { ProductId = p17.ProductId, SizeValue = @"43" });
            context.ProductSizes.Add(new ProductSize { ProductId = p17.ProductId, SizeValue = @"44" });
            context.SaveChanges();

            var p18 = new Product
            {
                ProductName = @"PUMA x HYROX Deviate NITRO™ Elite 4 Shoes Men",
                BrandId = brands.ContainsKey(@"Puma") ? brands[@"Puma"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Men",
                ProductType = @"Performance",
                Price = 6300000m,
                OldPrice = 0m,
                Rating = 4.9,
                ReviewCount = 0,
                Stock = 15,
                Badge = @"-10%",
                ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (3).png",
                Description = @"Giày chạy bộ PUMA x HYROX Deviate NITRO™ Elite 4 dành cho nam với công nghệ NITRO Elite siêu nhẹ, đế carbon, thiết kế tối ưu cho hiệu suất cao trong các cuộc thi chạy và tập luyện chuyên nghiệp.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p18);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p18.ProductId, ColorName = @"Xanh teal", HexCode = @"#008080", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (3).png" });
            context.ProductSizes.Add(new ProductSize { ProductId = p18.ProductId, SizeValue = @"39" });
            context.ProductSizes.Add(new ProductSize { ProductId = p18.ProductId, SizeValue = @"40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p18.ProductId, SizeValue = @"40.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p18.ProductId, SizeValue = @"41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p18.ProductId, SizeValue = @"42" });
            context.ProductSizes.Add(new ProductSize { ProductId = p18.ProductId, SizeValue = @"42.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p18.ProductId, SizeValue = @"43" });
            context.ProductSizes.Add(new ProductSize { ProductId = p18.ProductId, SizeValue = @"44" });
            context.ProductSizes.Add(new ProductSize { ProductId = p18.ProductId, SizeValue = @"44.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p18.ProductId, SizeValue = @"45" });
            context.SaveChanges();

            var p19 = new Product
            {
                ProductName = @"ADIZERO EVO SL EXO SHOES",
                BrandId = brands.ContainsKey(@"Adidas") ? brands[@"Adidas"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Performance",
                Price = 4300000m,
                OldPrice = 0m,
                Rating = 0,
                ReviewCount = 0,
                Stock = 25,
                Badge = @"NEW",
                ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (4).png",
                Description = @"Adizero EVO SL EXO Shoes - Giày chạy bộ hiệu suất cao với thiết kế siêu nhẹ, đế Lightstrike Pro và công nghệ khí động học, mang lại cảm giác chạy bền bỉ và tốc độ vượt trội cho mọi cung đường.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p19);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p19.ProductId, ColorName = @"Trắng", HexCode = @"#F5F5F5", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (4).png" });
            context.ProductSizes.Add(new ProductSize { ProductId = p19.ProductId, SizeValue = @"6" });
            context.ProductSizes.Add(new ProductSize { ProductId = p19.ProductId, SizeValue = @"6.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p19.ProductId, SizeValue = @"7" });
            context.ProductSizes.Add(new ProductSize { ProductId = p19.ProductId, SizeValue = @"7.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p19.ProductId, SizeValue = @"8" });
            context.ProductSizes.Add(new ProductSize { ProductId = p19.ProductId, SizeValue = @"8.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p19.ProductId, SizeValue = @"9" });
            context.ProductSizes.Add(new ProductSize { ProductId = p19.ProductId, SizeValue = @"9.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p19.ProductId, SizeValue = @"10" });
            context.ProductSizes.Add(new ProductSize { ProductId = p19.ProductId, SizeValue = @"10.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p19.ProductId, SizeValue = @"11" });
            context.SaveChanges();

            var p20 = new Product
            {
                ProductName = @"Sabrina 3 EP Basketball Shoes",
                BrandId = brands.ContainsKey(@"Nike") ? brands[@"Nike"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Women",
                ProductType = @"Basketball",
                Price = 3829000m,
                OldPrice = 0m,
                Rating = 4.7,
                ReviewCount = 0,
                Stock = 30,
                Badge = @"NEW",
                ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (5).png",
                Description = @"Sabrina 3 EP - Giày bóng rổ Nike dành riêng cho nữ với thiết kế năng động, công nghệ đệm React và độ bám tối ưu trên mọi mặt sân, lấy cảm hứng từ phong cách chơi của Sabrina Ionescu.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p20);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p20.ProductId, ColorName = @"Trắng", HexCode = @"#FFFFFF", ImageUrl = @"~/assets/img/SABRINA+3+EP (1).avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p20.ProductId, ColorName = @"Hồng", HexCode = @"#FF6B8A", ImageUrl = @"~/assets/img/SABRINA+3+EP.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p20.ProductId, ColorName = @"Đen", HexCode = @"#1A1A1A", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (5).png" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"35.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"36" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"36.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"37.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"38" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"38.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"39" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"40.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"42" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"42.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"43" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"44" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"44.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"45" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"45.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p20.ProductId, SizeValue = @"47" });
            context.SaveChanges();

            var p21 = new Product
            {
                ProductName = @"Converse Wave Motion Trainer",
                BrandId = brands.ContainsKey(@"Converse") ? brands[@"Converse"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Sneakers",
                Price = 3100000m,
                OldPrice = 0m,
                Rating = 4.8,
                ReviewCount = 16,
                Stock = 11,
                Badge = @"new",
                ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (6).png",
                Description = @"Converse Wave Motion Trainer sở hữu thiết kế chunky hiện đại với phần đế Wave nổi bật, mang lại cảm giác êm ái, ổn định và phù hợp cho cả đi chơi lẫn sử dụng hằng ngày.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p21);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p21.ProductId, ColorName = @"Trắng ", HexCode = @"#F5F5F0", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (6).png" });
            context.ProductColors.Add(new ProductColor { ProductId = p21.ProductId, ColorName = @"Hồng phấn", HexCode = @"#F2D8E0", ImageUrl = @"~/assets/img/0882-CONA20009C00W06H-2.webp" });
            context.ProductColors.Add(new ProductColor { ProductId = p21.ProductId, ColorName = @"Đen", HexCode = @"#111827", ImageUrl = @"~/assets/img/0882-CONA19128C00509H-2.webp" });
            context.ProductSizes.Add(new ProductSize { ProductId = p21.ProductId, SizeValue = @"36" });
            context.ProductSizes.Add(new ProductSize { ProductId = p21.ProductId, SizeValue = @"37" });
            context.ProductSizes.Add(new ProductSize { ProductId = p21.ProductId, SizeValue = @"38" });
            context.ProductSizes.Add(new ProductSize { ProductId = p21.ProductId, SizeValue = @"39" });
            context.ProductSizes.Add(new ProductSize { ProductId = p21.ProductId, SizeValue = @"40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p21.ProductId, SizeValue = @"41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p21.ProductId, SizeValue = @"42" });
            context.ProductSizes.Add(new ProductSize { ProductId = p21.ProductId, SizeValue = @"43" });
            context.ProductSizes.Add(new ProductSize { ProductId = p21.ProductId, SizeValue = @"44" });
            context.SaveChanges();

            var p22 = new Product
            {
                ProductName = @"Puma Flyer Lite 3",
                BrandId = brands.ContainsKey(@"Puma") ? brands[@"Puma"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Nữ",
                ProductType = @"Giày chạy bộ",
                Price = 1200000m,
                OldPrice = 0m,
                Rating = 4.7,
                ReviewCount = 19,
                Stock = 14,
                Badge = @"new",
                ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (7).png",
                Description = @"Puma Flyer Lite 3 là mẫu giày chạy bộ nhẹ với thân giày lưới thoáng khí, đệm êm và đế ngoài bám tốt, phù hợp cho luyện tập hằng ngày hoặc di chuyển thường xuyên.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p22);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p22.ProductId, ColorName = @"Trắng kem", HexCode = @"#F5F2EA", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (7).png" });
            context.ProductSizes.Add(new ProductSize { ProductId = p22.ProductId, SizeValue = @"35.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p22.ProductId, SizeValue = @"36" });
            context.ProductSizes.Add(new ProductSize { ProductId = p22.ProductId, SizeValue = @"37" });
            context.ProductSizes.Add(new ProductSize { ProductId = p22.ProductId, SizeValue = @"37.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p22.ProductId, SizeValue = @"38" });
            context.ProductSizes.Add(new ProductSize { ProductId = p22.ProductId, SizeValue = @"38.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p22.ProductId, SizeValue = @"39" });
            context.ProductSizes.Add(new ProductSize { ProductId = p22.ProductId, SizeValue = @"40" });
            context.SaveChanges();

            var p23 = new Product
            {
                ProductName = @"ADIZERO TAKUMI SEN 11 SHOES",
                BrandId = brands.ContainsKey(@"Adidas") ? brands[@"Adidas"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Men",
                ProductType = @"Running",
                Price = 5000000m,
                OldPrice = 0m,
                Rating = 4.9,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"-15%",
                ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (8).png",
                Description = @"ADIZERO TAKUMI SEN 11 - Giày chạy bộ hiệu suất cao dành cho nam với thiết kế siêu nhẹ, đệm Lightstrike Pro và công nghệ Energy Rods, tối ưu cho các cuộc đua tốc độ và chạy đường dài.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p23);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p23.ProductId, ColorName = @"Xanh dương", HexCode = @"#1E3A8A", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (8).png" });
            context.ProductSizes.Add(new ProductSize { ProductId = p23.ProductId, SizeValue = @"6" });
            context.ProductSizes.Add(new ProductSize { ProductId = p23.ProductId, SizeValue = @"6.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p23.ProductId, SizeValue = @"7" });
            context.ProductSizes.Add(new ProductSize { ProductId = p23.ProductId, SizeValue = @"7.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p23.ProductId, SizeValue = @"8" });
            context.ProductSizes.Add(new ProductSize { ProductId = p23.ProductId, SizeValue = @"8.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p23.ProductId, SizeValue = @"9" });
            context.ProductSizes.Add(new ProductSize { ProductId = p23.ProductId, SizeValue = @"9.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p23.ProductId, SizeValue = @"10" });
            context.ProductSizes.Add(new ProductSize { ProductId = p23.ProductId, SizeValue = @"10.5" });
            context.SaveChanges();

            var p24 = new Product
            {
                ProductName = @"Nike SB Dunk Low Pro Skate Shoes",
                BrandId = brands.ContainsKey(@"Nike") ? brands[@"Nike"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Skateboarding",
                Price = 3239000m,
                OldPrice = 0m,
                Rating = 4.8,
                ReviewCount = 0,
                Stock = 25,
                Badge = @"NEW",
                ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (9).png",
                Description = @"Nike SB Dunk Low Pro - Giày skateboard biểu tượng với thiết kế low-top linh hoạt, đế Zoom Air hỗ trợ giảm chấn và chất liệu da cao cấp, mang lại cảm giác êm ái và độ bám tốt cho mọi pha trượt.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p24);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p24.ProductId, ColorName = @"Trắng", HexCode = @"#FFFFFF", ImageUrl = @"~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (9).png" });
            context.ProductSizes.Add(new ProductSize { ProductId = p24.ProductId, SizeValue = @"38.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p24.ProductId, SizeValue = @"39" });
            context.ProductSizes.Add(new ProductSize { ProductId = p24.ProductId, SizeValue = @"40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p24.ProductId, SizeValue = @"41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p24.ProductId, SizeValue = @"42" });
            context.ProductSizes.Add(new ProductSize { ProductId = p24.ProductId, SizeValue = @"43" });
            context.ProductSizes.Add(new ProductSize { ProductId = p24.ProductId, SizeValue = @"44" });
            context.ProductSizes.Add(new ProductSize { ProductId = p24.ProductId, SizeValue = @"45" });
            context.ProductSizes.Add(new ProductSize { ProductId = p24.ProductId, SizeValue = @"46" });
            context.ProductSizes.Add(new ProductSize { ProductId = p24.ProductId, SizeValue = @"47.5" });
            context.SaveChanges();

            var p25 = new Product
            {
                ProductName = @"Adidas Lifestyle Sneakers",
                BrandId = brands.ContainsKey(@"Adidas") ? brands[@"Adidas"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Phụ kiện") ? categories[@"Phụ kiện"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Lifestyle",
                Price = 480000m,
                OldPrice = 800000m,
                Rating = 4.5,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"-40%",
                ImageUrl = @"~/assets/img/Cap_adidas.avif",
                Description = @"Giày Adidas Lifestyle với thiết kế thể thao phong cách, phù hợp cho mọi hoạt động hằng ngày. Sản phẩm thuộc chương trình End Of Season Sale 2026 với ưu đãi lên đến 50% và thêm 15% cho thành viên adiClub.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p25);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p25.ProductId, ColorName = @"Đen", HexCode = @"#1a2a3a", ImageUrl = @"~/assets/img/Cap_adidas.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p25.ProductId, ColorName = @"Đỏ", HexCode = @"#DC2626", ImageUrl = @"~/assets/img/Cap_adidas2.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p25.ProductId, ColorName = @"Tím", HexCode = @"#7C3AED", ImageUrl = @"~/assets/img/Cap_adidas3.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p25.ProductId, SizeValue = @"39" });
            context.ProductSizes.Add(new ProductSize { ProductId = p25.ProductId, SizeValue = @"40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p25.ProductId, SizeValue = @"41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p25.ProductId, SizeValue = @"42" });
            context.ProductSizes.Add(new ProductSize { ProductId = p25.ProductId, SizeValue = @"43" });
            context.ProductSizes.Add(new ProductSize { ProductId = p25.ProductId, SizeValue = @"44" });
            context.SaveChanges();

            var p26 = new Product
            {
                ProductName = @"Túi thể thao PUMA Buzz Small",
                BrandId = brands.ContainsKey(@"Puma") ? brands[@"Puma"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Phụ kiện") ? categories[@"Phụ kiện"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Túi & Balo",
                Price = 800000m,
                OldPrice = 1000000m,
                Rating = 4.6,
                ReviewCount = 0,
                Stock = 15,
                Badge = @"-20%",
                ImageUrl = @"~/assets/img/Tui_Puma.avif",
                Description = @"Túi thể thao PUMA Buzz Small với thiết kế nhỏ gọn, phong cách thể thao năng động. Chất liệu bền bỉ, phù hợp mang theo khi tập gym, đi du lịch hoặc sử dụng hằng ngày. Có thể nhập mã EXTRA10 để được giảm thêm 10%.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p26);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p26.ProductId, ColorName = @"Đen", HexCode = @"#111827", ImageUrl = @"~/assets/img/Tui_Puma.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p26.ProductId, SizeValue = @"OSFA" });
            context.SaveChanges();

            var p27 = new Product
            {
                ProductName = @"Converse Backpack",
                BrandId = brands.ContainsKey(@"Converse") ? brands[@"Converse"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Phụ kiện") ? categories[@"Phụ kiện"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Túi & Balo",
                Price = 1250000m,
                OldPrice = 0m,
                Rating = 4.7,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"new",
                ImageUrl = @"~/assets/img/Tui_CV.webp",
                Description = @"Balo Converse với thiết kế đơn giản, phong cách trẻ trung và năng động. Chất liệu bền bỉ, phù hợp cho học sinh, sinh viên hoặc sử dụng hằng ngày. Thiết kế một size phù hợp cho mọi đối tượng.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p27);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p27.ProductId, ColorName = @"Đen", HexCode = @"#111827", ImageUrl = @"~/assets/img/Tui_CV.webp" });
            context.ProductSizes.Add(new ProductSize { ProductId = p27.ProductId, SizeValue = @"One Size" });
            context.SaveChanges();

            var p28 = new Product
            {
                ProductName = @"Adidas Essentials Linear Duffel Bag Medium",
                BrandId = brands.ContainsKey(@"Adidas") ? brands[@"Adidas"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Phụ kiện") ? categories[@"Phụ kiện"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Túi & Balo",
                Price = 645000m,
                OldPrice = 950000m,
                Rating = 4.8,
                ReviewCount = 0,
                Stock = 25,
                Badge = @"-30%",
                ImageUrl = @"~/assets/img/tui_adidas1.avif",
                Description = @"Túi duffel Adidas Essentials Linear Medium với thiết kế thể thao, năng động. Chất liệu bền bỉ, phù hợp mang theo khi tập gym, du lịch hoặc sử dụng hằng ngày. Sản phẩm thuộc chương trình End Of Season Sale 2026 với ưu đãi lên đến 50% và thêm 15% cho thành viên adiClub.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p28);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p28.ProductId, ColorName = @"Đen", HexCode = @"#111827", ImageUrl = @"~/assets/img/tui_adidas2.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p28.ProductId, ColorName = @"Xanh dương", HexCode = @"#1E3A8A", ImageUrl = @"~/assets/img/tui_adidas1.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p28.ProductId, ColorName = @"Xám", HexCode = @"#6B7280", ImageUrl = @"~/assets/img/tui_adidas3.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p28.ProductId, ColorName = @"Tím", HexCode = @"#7C3AED", ImageUrl = @"~/assets/img/tui_adidas4.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p28.ProductId, SizeValue = @"Medium" });
            context.SaveChanges();

            var p29 = new Product
            {
                ProductName = @"Converse X Hello Kitty And Friends Go 2 Backpack",
                BrandId = brands.ContainsKey(@"Converse") ? brands[@"Converse"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Phụ kiện") ? categories[@"Phụ kiện"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Túi & Balo",
                Price = 1550000m,
                OldPrice = 0m,
                Rating = 5.0,
                ReviewCount = 0,
                Stock = 15,
                Badge = @"limited edition",
                ImageUrl = @"~/assets/img/0882-CONWA5874023005OSA-1.webp",
                Description = @"Chiếc ba lô đa năng kết hợp phong cách siêu đáng yêu cùng các nhân vật Sanrio yêu thích. Thiết kế tiện lợi cho mọi nhu cầu di chuyển hằng ngày.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p29);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p29.ProductId, ColorName = @"Đen", HexCode = @"#000000", ImageUrl = @"~/assets/img/0882-CONWA5874023005OSA-1.webp" });
            context.ProductSizes.Add(new ProductSize { ProductId = p29.ProductId, SizeValue = @"One Size" });
            context.SaveChanges();

            var p30 = new Product
            {
                ProductName = @"Real Madrid Baseball Cap",
                BrandId = brands.ContainsKey(@"Adidas") ? brands[@"Adidas"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Nón") ? categories[@"Nón"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Mũ",
                Price = 650000m,
                OldPrice = 0m,
                Rating = 4.8,
                ReviewCount = 0,
                Stock = 30,
                Badge = @"new",
                ImageUrl = @"~/assets/img/REAL_MADRID_BASEBALL_CAP_Black_KQ9575_01_00_standard.avif",
                Description = @"Chiếc mũ bóng chày Real Madrid phong cách với thiết kế ba sọc đặc trưng của Adidas và huy hiệu câu lạc bộ nổi bật. Chất liệu bền đẹp, phù hợp để đội hằng ngày hoặc cổ vũ cho đội bóng yêu thích.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p30);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p30.ProductId, ColorName = @"Đen", HexCode = @"#111827", ImageUrl = @"~/assets/img/REAL_MADRID_BASEBALL_CAP_Black_KQ9575_01_00_standard.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p30.ProductId, SizeValue = @"OSFW" });
            context.ProductSizes.Add(new ProductSize { ProductId = p30.ProductId, SizeValue = @"OSFM" });
            context.SaveChanges();

            var p31 = new Product
            {
                ProductName = @"Mũ Lưỡi Trai Replica Đội Đua Scuderia Ferrari HP",
                BrandId = brands.ContainsKey(@"Puma") ? brands[@"Puma"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Nón") ? categories[@"Nón"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Mũ",
                Price = 1100000m,
                OldPrice = 0m,
                Rating = 5.0,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"new",
                ImageUrl = @"~/assets/img/Mũ-Lưỡi-Trai-Replica-Đội-Đua-Scuderia-Ferrari-HP.avif",
                Description = @"Mũ lưỡi trai phong cách thể thao từ bộ sưu tập Scuderia Ferrari của Puma. Thiết kế nổi bật với huy hiệu Ferrari đặc trưng, chất liệu thoáng khí, phù hợp cho người hâm mộ tốc độ và phong cách năng động.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p31);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p31.ProductId, ColorName = @"Đỏ", HexCode = @"#EF4444", ImageUrl = @"~/assets/img/Mũ-Lưỡi-Trai-Replica-Đội-Đua-Scuderia-Ferrari-HP.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p31.ProductId, SizeValue = @"OSFA" });
            context.SaveChanges();

            var p32 = new Product
            {
                ProductName = @"Adimule Slides",
                BrandId = brands.ContainsKey(@"Adidas") ? brands[@"Adidas"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Dép") ? categories[@"Dép"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Dép",
                Price = 2700000m,
                OldPrice = 0m,
                Rating = 4.5,
                ReviewCount = 129,
                Stock = 20,
                Badge = @"new",
                ImageUrl = @"~/assets/img/image (7).png",
                Description = @"Dép Adimule với thiết kế tối giản, chất liệu da lộn cao cấp mang lại cảm giác thoải mái và sang trọng. Phong cách hiện đại phù hợp cho nhiều trang phục thường ngày.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p32);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p32.ProductId, ColorName = @"Đen", HexCode = @"#111827", ImageUrl = @"~/assets/img/image (8).png" });
            context.ProductColors.Add(new ProductColor { ProductId = p32.ProductId, ColorName = @"Cam", HexCode = @"#F97316", ImageUrl = @"~/assets/img/image (9).png" });
            context.ProductColors.Add(new ProductColor { ProductId = p32.ProductId, ColorName = @"Be", HexCode = @"#D6C4B1", ImageUrl = @"~/assets/img/image (7).png" });
            context.ProductSizes.Add(new ProductSize { ProductId = p32.ProductId, SizeValue = @"4 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p32.ProductId, SizeValue = @"5 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p32.ProductId, SizeValue = @"6 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p32.ProductId, SizeValue = @"7 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p32.ProductId, SizeValue = @"8 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p32.ProductId, SizeValue = @"9 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p32.ProductId, SizeValue = @"10 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p32.ProductId, SizeValue = @"11 UK" });
            context.SaveChanges();

            var p33 = new Product
            {
                ProductName = @"Dép Quai Ngang MCLAREN RACING Leadcat 2.0 Unisex",
                BrandId = brands.ContainsKey(@"Puma") ? brands[@"Puma"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Dép") ? categories[@"Dép"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Dép",
                Price = 770000m,
                OldPrice = 1100000m,
                Rating = 4.8,
                ReviewCount = 0,
                Stock = 25,
                Badge = @"-30%",
                ImageUrl = @"~/assets/img/Dép-Quai-Ngang-McLAREN-RACING-Leadcat-2.0-Unisex",
                Description = @"Dép quai ngang Leadcat 2.0 hợp tác cùng McLaren Racing, mang đậm phong cách thể thao tốc độ. Thiết kế bền bỉ và thoải mái, là lựa chọn lý tưởng cho các hoạt động hằng ngày.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p33);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p33.ProductId, ColorName = @"Đen", HexCode = @"#111827", ImageUrl = @"~/assets/img/Dép-Quai-Ngang-McLAREN-RACING-Leadcat-2 (1).0-Unisex" });
            context.ProductColors.Add(new ProductColor { ProductId = p33.ProductId, ColorName = @"Cam", HexCode = @"#FF8C00", ImageUrl = @"~/assets/img/Dép-Quai-Ngang-McLAREN-RACING-Leadcat-2.0-Unisex" });
            context.ProductSizes.Add(new ProductSize { ProductId = p33.ProductId, SizeValue = @"39" });
            context.ProductSizes.Add(new ProductSize { ProductId = p33.ProductId, SizeValue = @"42" });
            context.ProductSizes.Add(new ProductSize { ProductId = p33.ProductId, SizeValue = @"43" });
            context.ProductSizes.Add(new ProductSize { ProductId = p33.ProductId, SizeValue = @"44.5" });
            context.SaveChanges();

            var p34 = new Product
            {
                ProductName = @"Manchester United Baseball Cap",
                BrandId = brands.ContainsKey(@"Adidas") ? brands[@"Adidas"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Nón") ? categories[@"Nón"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Mũ",
                Price = 650000m,
                OldPrice = 0m,
                Rating = 5.0,
                ReviewCount = 2,
                Stock = 25,
                Badge = @"new",
                ImageUrl = @"~/assets/img/MANCHESTER_UNITED_BASEBALL_CAP_Red_KT0648_01_00_standard.avif",
                Description = @"Nón bóng chày Manchester United với sắc đỏ nổi bật, kết hợp huy hiệu câu lạc bộ và ba sọc đặc trưng của Adidas. Phụ kiện lý tưởng dành cho người hâm mộ thể hiện niềm tự hào với đội bóng.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p34);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p34.ProductId, ColorName = @"Đỏ", HexCode = @"#DC2626", ImageUrl = @"~/assets/img/MANCHESTER_UNITED_BASEBALL_CAP_Red_KT0648_01_00_standard.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p34.ProductId, SizeValue = @"OSFW" });
            context.ProductSizes.Add(new ProductSize { ProductId = p34.ProductId, SizeValue = @"OSFM" });
            context.SaveChanges();

            var p35 = new Product
            {
                ProductName = @"Classic Cotton Bucket Hat",
                BrandId = brands.ContainsKey(@"Adidas") ? brands[@"Adidas"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Nón") ? categories[@"Nón"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Nón",
                Price = 550000m,
                OldPrice = 0m,
                Rating = 4.7,
                ReviewCount = 107,
                Stock = 20,
                Badge = @"trending",
                ImageUrl = @"~/assets/img/Classic_Cotton_Bucket_Hat_Black_HT2029_01_00_standard_3.avif",
                Description = @"Nón bucket cổ điển từ Adidas, mang phong cách đơn giản và năng động. Chất liệu cotton thoáng mát, phù hợp để phối đồ hằng ngày.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p35);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p35.ProductId, ColorName = @"Đen", HexCode = @"#111827", ImageUrl = @"~/assets/img/Classic_Cotton_Bucket_Hat_Black_HT2029_01_00_standard.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p35.ProductId, ColorName = @"Xám", HexCode = @"#9ca3af", ImageUrl = @"~/assets/img/Classic_Cotton_Bucket_Hat_Black_HT2029_01_00_standard_2.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p35.ProductId, ColorName = @"Be", HexCode = @"#D6C4B1", ImageUrl = @"~/assets/img/Classic_Cotton_Bucket_Hat_Black_HT2029_01_00_standard_3.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p35.ProductId, SizeValue = @"OSFW" });
            context.ProductSizes.Add(new ProductSize { ProductId = p35.ProductId, SizeValue = @"OSFM" });
            context.SaveChanges();

            var p36 = new Product
            {
                ProductName = @"Day 1 Chuck Bucket Hat",
                BrandId = brands.ContainsKey(@"Converse") ? brands[@"Converse"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Nón") ? categories[@"Nón"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Nón",
                Price = 550000m,
                OldPrice = 0m,
                Rating = 4.8,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"new",
                ImageUrl = @"~/assets/img/0882-CONMA5712023005O_S-1_1778955309_3.webp",
                Description = @"Nón bucket Converse với thiết kế trẻ trung, nổi bật nhờ logo All Star đặc trưng. Chất liệu bền bỉ, phù hợp cho phong cách hằng ngày.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p36);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p36.ProductId, ColorName = @"Đen", HexCode = @"#000000", ImageUrl = @"~/assets/img/0882-CONMA5712023005O_S-1_1778955309.webp" });
            context.ProductColors.Add(new ProductColor { ProductId = p36.ProductId, ColorName = @"Kem", HexCode = @"#F5F5DC", ImageUrl = @"~/assets/img/0882-CONMA5712023005O_S-1_1778955309_2.webp" });
            context.ProductColors.Add(new ProductColor { ProductId = p36.ProductId, ColorName = @"Xanh navy", HexCode = @"#000080", ImageUrl = @"~/assets/img/0882-CONMA5712023005O_S-1_1778955309_3.webp" });
            context.ProductSizes.Add(new ProductSize { ProductId = p36.ProductId, SizeValue = @"One Size" });
            context.SaveChanges();

            var p37 = new Product
            {
                ProductName = @"Unwind NITROcat™ Unisex",
                BrandId = brands.ContainsKey(@"Puma") ? brands[@"Puma"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Dép") ? categories[@"Dép"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Dép",
                Price = 1260000m,
                OldPrice = 2100000m,
                Rating = 5.0,
                ReviewCount = 0,
                Stock = 15,
                Badge = @"new",
                ImageUrl = @"~/assets/img/Unwind-NITROcat™-Unisex.avif",
                Description = @"Dép Unwind NITROcat™ mang đến sự êm ái tối đa với công nghệ đệm NITRO tiên tiến, thiết kế tối giản, phong cách năng động cho các hoạt động hằng ngày.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p37);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p37.ProductId, ColorName = @"Đen", HexCode = @"#000000", ImageUrl = @"~/assets/img/Unwind-NITROcat™-Unisex (1)_2.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p37.ProductId, ColorName = @"Be", HexCode = @"#D6C4B1", ImageUrl = @"~/assets/img/Unwind-NITROcat™-Unisex.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p37.ProductId, SizeValue = @"35.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p37.ProductId, SizeValue = @"37" });
            context.ProductSizes.Add(new ProductSize { ProductId = p37.ProductId, SizeValue = @"38" });
            context.SaveChanges();

            var p38 = new Product
            {
                ProductName = @"ADIFOM IINFINITY MULES",
                BrandId = brands.ContainsKey(@"Adidas") ? brands[@"Adidas"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Dép") ? categories[@"Dép"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Dép",
                Price = 1400000m,
                OldPrice = 0m,
                Rating = 4.5,
                ReviewCount = 4,
                Stock = 20,
                Badge = @"End Of Season Sale 2026",
                ImageUrl = @"~/assets/img/ADIFOM_Iinfinity_Mules_Black_KJ9516_01_00_standard.png",
                Description = @"Dép Adifom Iinfinity Mules với thiết kế hiện đại, độc đáo. Sản phẩm mang lại sự thoải mái tối đa cho việc sử dụng hằng ngày.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p38);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p38.ProductId, ColorName = @"Đen", HexCode = @"#111827", ImageUrl = @"~/assets/img/ADIFOM_Iinfinity_Mules_Black_KJ9516_01_00_standard.png" });
            context.ProductColors.Add(new ProductColor { ProductId = p38.ProductId, ColorName = @"Cam", HexCode = @"#F97316", ImageUrl = @"~/assets/img/ADIFOM_Iinfinity_Mules_Black_KJ9516_01_00_standard_2.png" });
            context.ProductSizes.Add(new ProductSize { ProductId = p38.ProductId, SizeValue = @"4 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p38.ProductId, SizeValue = @"5 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p38.ProductId, SizeValue = @"6 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p38.ProductId, SizeValue = @"7 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p38.ProductId, SizeValue = @"8 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p38.ProductId, SizeValue = @"9 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p38.ProductId, SizeValue = @"10 UK" });
            context.ProductSizes.Add(new ProductSize { ProductId = p38.ProductId, SizeValue = @"11 UK" });
            context.SaveChanges();

            var p39 = new Product
            {
                ProductName = @"Dép sandal quai hậu",
                BrandId = brands.ContainsKey(@"Puma") ? brands[@"Puma"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Dép") ? categories[@"Dép"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Dép",
                Price = 420000m,
                OldPrice = 799000m,
                Rating = 5.0,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"-40%",
                ImageUrl = @"~/assets/img/Dép-sandal-quai-hậu.avif",
                Description = @"Dép sandal quai hậu Puma với thiết kế năng động, tiện lợi. Chất liệu bền bỉ và quai dán điều chỉnh dễ dàng, phù hợp cho nhiều hoạt động ngoài trời.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p39);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p39.ProductId, ColorName = @"Đen", HexCode = @"#111827", ImageUrl = @"~/assets/img/Dép-sandal-quai-hậu.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p39.ProductId, SizeValue = @"EUR 46" });
            context.ProductSizes.Add(new ProductSize { ProductId = p39.ProductId, SizeValue = @"EUR 47" });
            context.SaveChanges();

            var p40 = new Product
            {
                ProductName = @"Giày 574 Core",
                BrandId = brands.ContainsKey(@"New Balance") ? brands[@"New Balance"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"unisex ",
                ProductType = @"Giày Lifestyle",
                Price = 1900000m,
                OldPrice = 2500000m,
                Rating = 4.2,
                ReviewCount = 2277,
                Stock = 30,
                Badge = @"-25%",
                ImageUrl = @"~/assets/img/ml574evg_nb_03_i.jpg",
                Description = @"Mẫu giày 574 Core mang tính biểu tượng của New Balance, kết hợp giữa phong cách cổ điển và sự thoải mái hiện đại, hoàn hảo cho việc sử dụng hằng ngày.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p40);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p40.ProductId, ColorName = @"Xám", HexCode = @"#71717A", ImageUrl = @"~/assets/img/ml574evg_nb_03_i.jpg" });
            context.ProductSizes.Add(new ProductSize { ProductId = p40.ProductId, SizeValue = @"4" });
            context.ProductSizes.Add(new ProductSize { ProductId = p40.ProductId, SizeValue = @"4.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p40.ProductId, SizeValue = @"6" });
            context.ProductSizes.Add(new ProductSize { ProductId = p40.ProductId, SizeValue = @"7.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p40.ProductId, SizeValue = @"8" });
            context.ProductSizes.Add(new ProductSize { ProductId = p40.ProductId, SizeValue = @"8.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p40.ProductId, SizeValue = @"9" });
            context.ProductSizes.Add(new ProductSize { ProductId = p40.ProductId, SizeValue = @"9.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p40.ProductId, SizeValue = @"10" });
            context.ProductSizes.Add(new ProductSize { ProductId = p40.ProductId, SizeValue = @"10.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p40.ProductId, SizeValue = @"11" });
            context.ProductSizes.Add(new ProductSize { ProductId = p40.ProductId, SizeValue = @"11.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p40.ProductId, SizeValue = @"12" });
            context.SaveChanges();

            var p41 = new Product
            {
                ProductName = @"FuelCell Rebel v5",
                BrandId = brands.ContainsKey(@"New Balance") ? brands[@"New Balance"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Nam",
                ProductType = @"Giày chạy bộ",
                Price = 3700000m,
                OldPrice = 0m,
                Rating = 5.0,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"mới",
                ImageUrl = @"~/assets/img/mfcx2pp_nb_03_i.webp",
                Description = @"Giày chạy bộ FuelCell Rebel v5 với công nghệ đệm cao cấp, mang lại khả năng hoàn trả năng lượng tối ưu và sự thoải mái cho các vận động viên.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p41);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p41.ProductId, ColorName = @"Cam", HexCode = @"#FF4500", ImageUrl = @"~/assets/img/mfcx2pp_nb_03_i.webp" });
            context.ProductColors.Add(new ProductColor { ProductId = p41.ProductId, ColorName = @"Xanh dương", HexCode = @"#007BFF", ImageUrl = @"~/assets/img/mfcx2pp_nb_03_i_2.webp" });
            context.ProductColors.Add(new ProductColor { ProductId = p41.ProductId, ColorName = @"Vàng nhạt", HexCode = @"#D0D0B5", ImageUrl = @"~/assets/img/mfcx2pp_nb_03_i_3.webp" });
            context.ProductColors.Add(new ProductColor { ProductId = p41.ProductId, ColorName = @"Trắng", HexCode = @"#FFFFFF", ImageUrl = @"~/assets/img/mfcx2pp_nb_03_i_4.webp" });
            context.ProductSizes.Add(new ProductSize { ProductId = p41.ProductId, SizeValue = @"7" });
            context.ProductSizes.Add(new ProductSize { ProductId = p41.ProductId, SizeValue = @"7.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p41.ProductId, SizeValue = @"8" });
            context.ProductSizes.Add(new ProductSize { ProductId = p41.ProductId, SizeValue = @"8.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p41.ProductId, SizeValue = @"9" });
            context.ProductSizes.Add(new ProductSize { ProductId = p41.ProductId, SizeValue = @"9.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p41.ProductId, SizeValue = @"10" });
            context.ProductSizes.Add(new ProductSize { ProductId = p41.ProductId, SizeValue = @"10.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p41.ProductId, SizeValue = @"11" });
            context.ProductSizes.Add(new ProductSize { ProductId = p41.ProductId, SizeValue = @"11.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p41.ProductId, SizeValue = @"12" });
            context.ProductSizes.Add(new ProductSize { ProductId = p41.ProductId, SizeValue = @"12.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p41.ProductId, SizeValue = @"13" });
            context.ProductSizes.Add(new ProductSize { ProductId = p41.ProductId, SizeValue = @"14" });
            context.ProductSizes.Add(new ProductSize { ProductId = p41.ProductId, SizeValue = @"15" });
            context.SaveChanges();

            var p42 = new Product
            {
                ProductName = @"Fresh Foam X RCVRY",
                BrandId = brands.ContainsKey(@"New Balance") ? brands[@"New Balance"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Giày") ? categories[@"Giày"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Giày chạy bộ",
                Price = 2500000m,
                OldPrice = 0m,
                Rating = 4.0,
                ReviewCount = 127,
                Stock = 20,
                Badge = @"Giảm giá 25%",
                ImageUrl = @"~/assets/img/rcvryegy_nb_03_i_2.webp",
                Description = @"Giày Fresh Foam X RCVRY với thiết kế thoải mái, hỗ trợ phục hồi sau khi vận động, chất liệu nhẹ và êm ái.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p42);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p42.ProductId, ColorName = @"Đen", HexCode = @"#000000", ImageUrl = @"~/assets/img/rcvryegy_nb_03_i_2.webp" });
            context.ProductColors.Add(new ProductColor { ProductId = p42.ProductId, ColorName = @"Xám", HexCode = @"#D3D3D3", ImageUrl = @"~/assets/img/rcvryegy_nb_03_i.webp" });
            context.ProductColors.Add(new ProductColor { ProductId = p42.ProductId, ColorName = @"Xanh lá", HexCode = @"#7FFF00", ImageUrl = @"~/assets/img/rcvryegy_nb_03_i_3.webp" });
            context.ProductSizes.Add(new ProductSize { ProductId = p42.ProductId, SizeValue = @"M4/W5.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p42.ProductId, SizeValue = @"M5/W6.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p42.ProductId, SizeValue = @"M6/W7.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p42.ProductId, SizeValue = @"M7/W8.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p42.ProductId, SizeValue = @"M8/W9.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p42.ProductId, SizeValue = @"M9/W10.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p42.ProductId, SizeValue = @"M10/W11.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p42.ProductId, SizeValue = @"M11/W12.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p42.ProductId, SizeValue = @"M12/W13.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p42.ProductId, SizeValue = @"M13/W14.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p42.ProductId, SizeValue = @"M14/W15.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p42.ProductId, SizeValue = @"M15/W16.5" });
            context.SaveChanges();

            var p43 = new Product
            {
                ProductName = @"'47 Classic Tri-logo",
                BrandId = brands.ContainsKey(@"New Balance") ? brands[@"New Balance"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Nón") ? categories[@"Nón"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Nón",
                Price = 1000000m,
                OldPrice = 0m,
                Rating = 2.5,
                ReviewCount = 6,
                Stock = 3,
                Badge = @"mới",
                ImageUrl = @"~/assets/img/ac40718bk_nb_03_i_2.webp",
                Description = @"Nón '47 Classic Tri-logo với thiết kế đơn giản, thời trang, phù hợp cho nhiều hoạt động thường ngày.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p43);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p43.ProductId, ColorName = @"Nâu", HexCode = @"#6F5B4D", ImageUrl = @"~/assets/img/ac40718bk_nb_03_i_2.webp" });
            context.ProductColors.Add(new ProductColor { ProductId = p43.ProductId, ColorName = @"Đen", HexCode = @"#000000", ImageUrl = @"~/assets/img/ac40718bk_nb_03_i.webp" });
            context.ProductSizes.Add(new ProductSize { ProductId = p43.ProductId, SizeValue = @"Một kích cỡ" });
            context.SaveChanges();

            var p44 = new Product
            {
                ProductName = @"Shohei Signature Hitch",
                BrandId = brands.ContainsKey(@"New Balance") ? brands[@"New Balance"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Nón") ? categories[@"Nón"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Nón",
                Price = 950000m,
                OldPrice = 0m,
                Rating = 2.0,
                ReviewCount = 2,
                Stock = 20,
                Badge = @"mới",
                ImageUrl = @"~/assets/img/ac0089wsnw_nb_03_i.webp",
                Description = @"Nón Shohei Signature Hitch với thiết kế độc quyền, thể hiện phong cách năng động và tinh thần thể thao chuyên nghiệp.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p44);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p44.ProductId, ColorName = @"Nâu", HexCode = @"#A0522D", ImageUrl = @"~/assets/img/ac0089wsnw_nb_03_i.webp" });
            context.ProductColors.Add(new ProductColor { ProductId = p44.ProductId, ColorName = @"Xanh nhạt", HexCode = @"#ADD8E6", ImageUrl = @"~/assets/img/ac0089wsnw_nb_03_i_2.webp" });
            context.ProductSizes.Add(new ProductSize { ProductId = p44.ProductId, SizeValue = @"Một kích cỡ" });
            context.SaveChanges();

            var p45 = new Product
            {
                ProductName = @"Túi du lịch Basketball Duffle Bag",
                BrandId = brands.ContainsKey(@"New Balance") ? brands[@"New Balance"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Phụ kiện") ? categories[@"Phụ kiện"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Túi & Balo",
                Price = 1900000m,
                OldPrice = 2700000m,
                Rating = 5.0,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"Giảm giá 30%",
                ImageUrl = @"~/assets/img/ac4497ktre_nb_03_i.webp",
                Description = @"Túi du lịch Basketball Duffle Bag tiện lợi với thiết kế bền bỉ, không gian chứa đồ rộng rãi, phù hợp cho việc mang theo dụng cụ thể thao hoặc các chuyến đi ngắn ngày.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p45);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p45.ProductId, ColorName = @"Đỏ", HexCode = @"#DC2626", ImageUrl = @"~/assets/img/ac4497ktre_nb_03_i.webp" });
            context.ProductSizes.Add(new ProductSize { ProductId = p45.ProductId, SizeValue = @"Một kích cỡ" });
            context.SaveChanges();

            var p46 = new Product
            {
                ProductName = @"Balo RC Pack",
                BrandId = brands.ContainsKey(@"New Balance") ? brands[@"New Balance"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Phụ kiện") ? categories[@"Phụ kiện"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Túi & Balo",
                Price = 2500000m,
                OldPrice = 3600000m,
                Rating = 5.0,
                ReviewCount = 1,
                Stock = 20,
                Badge = @"Giảm giá 30%",
                ImageUrl = @"~/assets/img/ac8227ubk_nb_03_i.webp",
                Description = @"Balo RC Pack với thiết kế tối giản, chất liệu bền bỉ và hiện đại, mang lại sự tiện lợi tối đa cho các hoạt động di chuyển hằng ngày.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p46);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p46.ProductId, ColorName = @"Đen", HexCode = @"#000000", ImageUrl = @"~/assets/img/ac8227ubk_nb_03_i.webp" });
            context.ProductSizes.Add(new ProductSize { ProductId = p46.ProductId, SizeValue = @"Một kích cỡ" });
            context.SaveChanges();

            var p47 = new Product
            {
                ProductName = @"Nike ACG 'DAYMAX'",
                BrandId = brands.ContainsKey(@"Nike") ? brands[@"Nike"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Phụ kiện") ? categories[@"Phụ kiện"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Balo",
                Price = 3569000m,
                OldPrice = 0m,
                Rating = 0,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"Vật liệu tái chế",
                ImageUrl = @"~/assets/img/ACG+DAYMAX+BKPK.avif",
                Description = @"Balo Nike ACG 'DAYMAX' dung tích 25L với ngăn chính lớn, các túi trước và bên hông dễ truy cập, cùng dây đai điều chỉnh để gắn thêm vật dụng, lý tưởng cho các chuyến phiêu lưu.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p47);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p47.ProductId, ColorName = @"Đen", HexCode = @"#000000", ImageUrl = @"~/assets/img/ACG+DAYMAX+BKPK.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p47.ProductId, SizeValue = @"Một kích cỡ" });
            context.SaveChanges();

            var p48 = new Product
            {
                ProductName = @"Nike Brasilia Backpack (Medium, 24L)",
                BrandId = brands.ContainsKey(@"Nike") ? brands[@"Nike"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Phụ kiện") ? categories[@"Phụ kiện"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Balo",
                Price = 1399000m,
                OldPrice = 0m,
                Rating = 0,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"Vật liệu tái chế",
                ImageUrl = @"~/assets/img/NK+BRSLA+M+BKPK+-+X+AOP+SU26.avif",
                Description = @"Balo Nike Brasilia với ngăn chính rộng rãi, ngăn đựng laptop lên đến 15 inch, túi lưới bên hông đựng bình nước, cùng các ngăn khóa kéo phía trước và bên trong giúp sắp xếp đồ dùng ngăn nắp.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p48);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p48.ProductId, ColorName = @"Xanh dương", HexCode = @"#2B4B70", ImageUrl = @"~/assets/img/NK+BRSLA+M+BKPK+-+X+AOP+SU26.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p48.ProductId, SizeValue = @"Một kích cỡ" });
            context.SaveChanges();

            var p49 = new Product
            {
                ProductName = @"Nike Dri-FIT Club Structured Swoosh Cap",
                BrandId = brands.ContainsKey(@"Nike") ? brands[@"Nike"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Nón") ? categories[@"Nón"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Nón",
                Price = 559000m,
                OldPrice = 0m,
                Rating = 0,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"Vật liệu tái chế",
                ImageUrl = @"~/assets/img/U+NK+DF+CLUB+CAP+S+CB+P.avif",
                Description = @"Nón Nike Dri-FIT Club với thiết kế structured và logo Swoosh đặc trưng. Sản phẩm được làm từ ít nhất 75% sợi polyester tái chế.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p49);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p49.ProductId, ColorName = @"Đen", HexCode = @"#000000", ImageUrl = @"~/assets/img/U+NK+DF+CLUB+CAP+S+CB+P.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p49.ProductId, ColorName = @"Xanh navy", HexCode = @"#000080", ImageUrl = @"~/assets/img/U+NK+DF+CLUB+CAP+S+CB+P (1).avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p49.ProductId, SizeValue = @"S/M" });
            context.ProductSizes.Add(new ProductSize { ProductId = p49.ProductId, SizeValue = @"M/L" });
            context.ProductSizes.Add(new ProductSize { ProductId = p49.ProductId, SizeValue = @"L/XL" });
            context.SaveChanges();

            var p50 = new Product
            {
                ProductName = @"Nike Apex Futura Washed Bucket Hat",
                BrandId = brands.ContainsKey(@"Nike") ? brands[@"Nike"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Nón") ? categories[@"Nón"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Nón",
                Price = 659000m,
                OldPrice = 0m,
                Rating = 5.0,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"mới",
                ImageUrl = @"~/assets/img/U+NK+APEX+BUCKET+SQ+FUT+WSH+L.avif",
                Description = @"Nón bucket Nike Apex với thiết kế mid-depth làm từ cotton mềm mại, nổi bật với logo Futura thêu tinh tế. Chất liệu đã qua xử lý wash mang lại cảm giác dễ chịu và vẻ ngoài cổ điển.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p50);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p50.ProductId, ColorName = @"Đen", HexCode = @"#000000", ImageUrl = @"~/assets/img/U+NK+APEX+BUCKET+SQ+FUT+WSH+L.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p50.ProductId, SizeValue = @"S" });
            context.ProductSizes.Add(new ProductSize { ProductId = p50.ProductId, SizeValue = @"M" });
            context.ProductSizes.Add(new ProductSize { ProductId = p50.ProductId, SizeValue = @"L" });
            context.SaveChanges();

            var p51 = new Product
            {
                ProductName = @"200XL",
                BrandId = brands.ContainsKey(@"New Balance") ? brands[@"New Balance"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Dép") ? categories[@"Dép"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Dép",
                Price = 1000000m,
                OldPrice = 0m,
                Rating = 4.8,
                ReviewCount = 4,
                Stock = 20,
                Badge = @"mới",
                ImageUrl = @"~/assets/img/suf2xla1_nb_02_i.jpg",
                Description = @"Dép 200XL với thiết kế hiện đại, thoải mái, phù hợp cho nhu cầu sử dụng hàng ngày.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p51);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p51.ProductId, ColorName = @"Đen", HexCode = @"#000000", ImageUrl = @"~/assets/img/suf2xla1_nb_02_i.jpg" });
            context.ProductSizes.Add(new ProductSize { ProductId = p51.ProductId, SizeValue = @"M4/W5.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p51.ProductId, SizeValue = @"M5/W6.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p51.ProductId, SizeValue = @"M6/W7.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p51.ProductId, SizeValue = @"M7/W8.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p51.ProductId, SizeValue = @"M10/W11.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p51.ProductId, SizeValue = @"M11/W12.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p51.ProductId, SizeValue = @"M12/W13.5" });
            context.SaveChanges();

            var p52 = new Product
            {
                ProductName = @"Nike ReactX Rejuven8",
                BrandId = brands.ContainsKey(@"Nike") ? brands[@"Nike"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Dép") ? categories[@"Dép"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Nam",
                ProductType = @"Dép",
                Price = 2069000m,
                OldPrice = 0m,
                Rating = 0,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"Được đánh giá cao",
                ImageUrl = @"~/assets/img/NIKE+REACTX+REJUVEN8.avif",
                Description = @"Giày Nike ReactX Rejuven8 mang lại sự thoải mái tối đa với công nghệ đệm ReactX tiên tiến, thiết kế hỗ trợ phục hồi và phong cách hiện đại.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p52);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p52.ProductId, ColorName = @"Đen", HexCode = @"#000000", ImageUrl = @"~/assets/img/NIKE+REACTX+REJUVEN8.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p52.ProductId, ColorName = @"Xám", HexCode = @"#808080", ImageUrl = @"~/assets/img/NIKE+REACTX+REJUVEN8_3.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p52.ProductId, ColorName = @"Xanh lá", HexCode = @"#3cff00", ImageUrl = @"~/assets/img/NIKE+REACTX+REJUVEN8_2.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p52.ProductId, SizeValue = @"EU 38.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p52.ProductId, SizeValue = @"EU 40" });
            context.ProductSizes.Add(new ProductSize { ProductId = p52.ProductId, SizeValue = @"EU 41" });
            context.ProductSizes.Add(new ProductSize { ProductId = p52.ProductId, SizeValue = @"EU 42.5" });
            context.ProductSizes.Add(new ProductSize { ProductId = p52.ProductId, SizeValue = @"EU 44" });
            context.ProductSizes.Add(new ProductSize { ProductId = p52.ProductId, SizeValue = @"EU 45" });
            context.ProductSizes.Add(new ProductSize { ProductId = p52.ProductId, SizeValue = @"EU 46" });
            context.ProductSizes.Add(new ProductSize { ProductId = p52.ProductId, SizeValue = @"EU 47.5" });
            context.SaveChanges();

            var p53 = new Product
            {
                ProductName = @"Jordan Franchise",
                BrandId = brands.ContainsKey(@"Nike") ? brands[@"Nike"].BrandId : brands["Nike"].BrandId,
                CategoryId = categories.ContainsKey(@"Dép") ? categories[@"Dép"].CategoryId : categories["Giày"].CategoryId,
                TargetAudience = @"Unisex",
                ProductType = @"Dép",
                Price = 889000m,
                OldPrice = 0m,
                Rating = 0,
                ReviewCount = 0,
                Stock = 20,
                Badge = @"Được đánh giá cao",
                ImageUrl = @"~/assets/img/JORDAN+FRANCHISE+SLIDE_3.avif",
                Description = @"Dép Jordan Franchise được làm từ bọt bền và linh hoạt, hỗ trợ từng bước đi, có đường cong nhẹ ở lòng bàn chân giúp giữ vừa vặn an toàn và nổi bật với logo Jumpman phía trước.",
                IsFeatured = false,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            context.Products.Add(p53);
            context.SaveChanges();

            context.ProductColors.Add(new ProductColor { ProductId = p53.ProductId, ColorName = @"Đen/Trắng", HexCode = @"#d8d4d4", ImageUrl = @"~/assets/img/JORDAN+FRANCHISE+SLIDE_3.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p53.ProductId, ColorName = @"Đen", HexCode = @"#000000", ImageUrl = @"~/assets/img/JORDAN+FRANCHISE+SLIDE.avif" });
            context.ProductColors.Add(new ProductColor { ProductId = p53.ProductId, ColorName = @"Trắng", HexCode = @"#FFFFFF", ImageUrl = @"~/assets/img/JORDAN+FRANCHISE+SLIDE_2.avif" });
            context.ProductSizes.Add(new ProductSize { ProductId = p53.ProductId, SizeValue = @"Một kích cỡ" });
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
