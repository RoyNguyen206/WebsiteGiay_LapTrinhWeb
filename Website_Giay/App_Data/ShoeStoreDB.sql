-- =============================================
-- DATABASE SCRIPT CHO DU AN WEBSITE BAN GIAY SNEAKER (SOLESYLE)
-- =============================================
USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'ShoeStoreDB')
BEGIN
    CREATE DATABASE ShoeStoreDB;
END
GO

USE ShoeStoreDB;
GO

-- 1. BANG THUONG HIEU (BRANDS)
IF OBJECT_ID('dbo.Brands', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Brands (
        BrandId INT IDENTITY(1,1) PRIMARY KEY,
        BrandName NVARCHAR(100) NOT NULL,
        LogoUrl NVARCHAR(255) NULL,
        Description NVARCHAR(500) NULL
    );
END
GO

-- 2. BANG DANH MUC (CATEGORIES)
IF OBJECT_ID('dbo.Categories', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Categories (
        CategoryId INT IDENTITY(1,1) PRIMARY KEY,
        CategoryName NVARCHAR(100) NOT NULL,
        Slug NVARCHAR(100) NULL,
        Description NVARCHAR(500) NULL
    );
END
GO

-- 3. BANG NGUOI DUNG (USERS)
IF OBJECT_ID('dbo.Users', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Users (
        UserId INT IDENTITY(1,1) PRIMARY KEY,
        FullName NVARCHAR(100) NOT NULL,
        Email NVARCHAR(150) NOT NULL UNIQUE,
        Password NVARCHAR(100) NOT NULL,
        PhoneNumber NVARCHAR(20) NULL,
        Address NVARCHAR(255) NULL,
        Role NVARCHAR(50) DEFAULT 'Customer',
        CreatedDate DATETIME DEFAULT GETDATE()
    );
END
GO

-- 4. BANG SAN PHAM (PRODUCTS)
IF OBJECT_ID('dbo.Products', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Products (
        ProductId INT IDENTITY(1,1) PRIMARY KEY,
        ProductName NVARCHAR(255) NOT NULL,
        BrandId INT NOT NULL FOREIGN KEY REFERENCES dbo.Brands(BrandId) ON DELETE CASCADE,
        CategoryId INT NOT NULL FOREIGN KEY REFERENCES dbo.Categories(CategoryId) ON DELETE CASCADE,
        TargetAudience NVARCHAR(50) DEFAULT 'Unisex',
        ProductType NVARCHAR(100) NULL,
        Price DECIMAL(18,0) NOT NULL DEFAULT 0,
        OldPrice DECIMAL(18,0) NOT NULL DEFAULT 0,
        Rating FLOAT DEFAULT 5.0,
        ReviewCount INT DEFAULT 0,
        Stock INT DEFAULT 10,
        Badge NVARCHAR(50) NULL,
        ImageUrl NVARCHAR(255) NULL,
        Description NVARCHAR(MAX) NULL,
        IsFeatured BIT DEFAULT 0,
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETDATE()
    );
END
GO

-- 5. BANG MAU SAC SAN PHAM (PRODUCT COLORS)
IF OBJECT_ID('dbo.ProductColors', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ProductColors (
        ColorId INT IDENTITY(1,1) PRIMARY KEY,
        ProductId INT NOT NULL FOREIGN KEY REFERENCES dbo.Products(ProductId) ON DELETE CASCADE,
        ColorName NVARCHAR(100) NOT NULL,
        HexCode NVARCHAR(50) NULL,
        ImageUrl NVARCHAR(255) NULL
    );
END
GO

-- 6. BANG KICH THUOC SAN PHAM (PRODUCT SIZES)
IF OBJECT_ID('dbo.ProductSizes', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ProductSizes (
        SizeId INT IDENTITY(1,1) PRIMARY KEY,
        ProductId INT NOT NULL FOREIGN KEY REFERENCES dbo.Products(ProductId) ON DELETE CASCADE,
        SizeValue NVARCHAR(50) NOT NULL
    );
END
GO

-- 7. BANG DON HANG (ORDERS)
IF OBJECT_ID('dbo.Orders', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Orders (
        OrderId INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NULL FOREIGN KEY REFERENCES dbo.Users(UserId),
        CustomerName NVARCHAR(100) NOT NULL,
        CustomerPhone NVARCHAR(20) NOT NULL,
        CustomerEmail NVARCHAR(150) NULL,
        ShippingAddress NVARCHAR(255) NOT NULL,
        Note NVARCHAR(500) NULL,
        TotalAmount DECIMAL(18,0) NOT NULL DEFAULT 0,
        PaymentMethod NVARCHAR(50) DEFAULT 'COD',
        OrderStatus NVARCHAR(50) DEFAULT N'Đang xử lý',
        OrderDate DATETIME DEFAULT GETDATE()
    );
END
GO

-- 8. BANG CHI TIET DON HANG (ORDER DETAILS)
IF OBJECT_ID('dbo.OrderDetails', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.OrderDetails (
        OrderDetailId INT IDENTITY(1,1) PRIMARY KEY,
        OrderId INT NOT NULL FOREIGN KEY REFERENCES dbo.Orders(OrderId) ON DELETE CASCADE,
        ProductId INT NOT NULL FOREIGN KEY REFERENCES dbo.Products(ProductId),
        SelectedColor NVARCHAR(50) NULL,
        SelectedSize NVARCHAR(50) NULL,
        Quantity INT NOT NULL DEFAULT 1,
        UnitPrice DECIMAL(18,0) NOT NULL DEFAULT 0
    );
END
GO

-- =============================================
-- INSERT DULIEU MAU
-- =============================================

-- Seed Brands
SET IDENTITY_INSERT dbo.Brands ON;
INSERT INTO dbo.Brands (BrandId, BrandName, LogoUrl, Description) VALUES
(1, N'Nike', '~/assets/img/logoNike.png', N'Thương hiệu thể thao hàng đầu thế giới'),
(2, N'Adidas', '~/assets/img/logoAdidas.png', N'Biểu tượng phong cách và thể thao từ Đức'),
(3, N'Converse', '~/assets/img/logoConverse.png', N'Huyền thoại giày bóng rổ và phong cách đường phố'),
(4, N'Puma', '~/assets/img/logoPuma.png', N'Tốc độ và phong cách năng động hiện đại'),
(5, N'New Balance', '~/assets/img/logoNewBalance.png', N'Chất lượng thủ công vượt trội và êm ái tối đa');
SET IDENTITY_INSERT dbo.Brands OFF;
GO

-- Seed Categories
SET IDENTITY_INSERT dbo.Categories ON;
INSERT INTO dbo.Categories (CategoryId, CategoryName, Slug, Description) VALUES
(1, N'Giày', 'giay', N'Các dòng giày thể thao, sneaker chính hãng'),
(2, N'Dép', 'dep', N'Dép quai ngang, dép xỏ ngón tiện lợi và thời trang'),
(3, N'Nón', 'non', N'Nón lưỡi trai, nón bucket phong cách'),
(4, N'Phụ kiện', 'phu-kien', N'Balo, vớ, túi thể thao chính hãng');
SET IDENTITY_INSERT dbo.Categories OFF;
GO

-- Seed Users
SET IDENTITY_INSERT dbo.Users ON;
INSERT INTO dbo.Users (UserId, FullName, Email, Password, PhoneNumber, Address, Role, CreatedDate) VALUES
(1, N'Quản Trị Viên', 'admin@solestyle.vn', 'admin', '0368068274', N'140 Lê Trọng Tấn, Tây Thạnh, Tân Phú, TP.HCM', 'Admin', GETDATE()),
(2, N'Nguyễn Văn An', 'an.nguyen@example.com', '123', '0901234567', N'123 CMT8, Quận 3, TP.HCM', 'Customer', GETDATE());
SET IDENTITY_INSERT dbo.Users OFF;
GO

SET IDENTITY_INSERT dbo.Products ON;

INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (1, N'Giày Nike Air Jordan 1 Retro High OG', 1, 1, N'Nam', N'Sneakers bóng rổ', 3990000, 4590000, 4.9, 18, 6, N'new', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (4) (16).png', N'Thiết kế cổ cao, đệm êm và phối màu dễ mặc cho cả đi học, đi chơi hoặc phối streetwear.', 1, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (2, N'Giày Adidas Ultraboost 2026 Running', 2, 1, N'Unisex', N'Giày chạy bộ', 4290000, 0, 4.8, 12, 9, N'new', '~/assets/img/image (5).png', N'Thân giày ôm chân, đế đàn hồi tốt, phù hợp tập luyện hằng ngày và di chuyển nhiều.', 1, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (3, N'Chuck Taylor All Star', 3, 1, N'Unisex', N'Giày cổ cao', 1700000, 0, 0, 0, 20, N'Kinh điển', '~/assets/img/image (4).png', N'Đôi giày biểu tượng nhất của Converse, Chuck Taylor All Star với thiết kế vải canvas bền bỉ và đế cao su đặc trưng.', 1, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (4, N'Nike P-6000', 1, 1, N'Unisex', N'Giày thể thao', 2929000, 0, 0, 0, 20, N'Được đánh giá cao', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (10).png', N'Giày Nike P-6000 với thiết kế lấy cảm hứng từ phong cách chạy bộ những năm 2000, kết hợp giữa sự thoải mái và vẻ ngoài thời thượng.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (5, N'Giày bóng đá ULTRA 6 MATCH TT Unisex', 4, 1, N'Unisex', N'Giày bóng đá', 1260000, 2100000, 0, 0, 20, N'Giảm giá 40%', '~/assets/img/Giày-bóng-đá-ULTRA-6-MATCH-TT-Unisex.avif', N'Giày bóng đá hiệu năng cao với thiết kế hiện đại, hỗ trợ tối đa cho các trận đấu trên sân cỏ nhân tạo.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (6, N'Dép Adidas Adilette Comfort', 2, 2, N'Unisex', N'Dép thể thao', 990000, 1290000, 4.5, 11, 10, N'-23%', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (17).png', N'Dép quai ngang êm, dễ mang sau khi tập luyện hoặc dùng hằng ngày.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (7, N'Giày Adidas Grand Court 3.0 Sneakers', 2, 1, N'Nam', N'Giày tennis lifestyle', 2000000, 0, 4.7, 15, 8, N'new', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (17)_ma2.png', N'Form thấp dễ mang, lấy cảm hứng từ giày tennis, phù hợp đi học, đi làm hoặc dạo phố.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (8, N'ADIZERO EVO SL SHOES', 2, 1, N'Nam', N'Giày chạy bộ', 4000000, 0, 1.0, 1, 20, N'mới', '~/assets/img/Adizero_EVO_SL_Shoes_Grey_KI9542_06_standard.avif', N'Giày chạy bộ Adizero Evo SL với thiết kế nhẹ, tối ưu hóa cho hiệu suất chạy bộ hàng ngày.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (9, N'Giày New Balance 530 Essential', 5, 1, N'Unisex', N'Giày lifestyle', 2390000, 2790000, 4.8, 14, 7, N'-14%', '~/assets/img/mr530asm_nb_03_i.webp', N'Thiết kế retro chạy bộ, nhẹ chân và dễ phối với quần jeans, cargo hoặc đồ thể thao.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (10, N'Air Jordan Mule Golf Shoes', 1, 1, N'Nam', N'Giày Golf', 2929000, 0, 0, 0, 20, N'Được đánh giá cao', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (15).png', N'Giày golf Air Jordan Mule với thiết kế không gót tiện lợi, mang lại sự thoải mái và phong cách đặc trưng trên sân golf.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (15, N'Adizero EVO SL Woven Shoes', 2, 1, N'Men', N'Performance', 3600000, 4000000, 4.8, 0, 20, N'-10%', '~/assets/img/Giay_Chay_Bo_Runfalcon_5_Mau_xanh_da_troi_JR5521_06_standard.avif', N'Giày chạy bộ Adizero EVO SL Woven dành cho nam với thiết kế siêu nhẹ, hỗ trợ tốc độ và hiệu suất tối đa trong luyện tập cũng như thi đấu.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (16, N'Nike Pegasus 1 Golf Shoes', 1, 1, N'Unisex', N'Golf Shoes', 4699000, 0, 4.9, 21, 15, N'new', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3).png', N'Nike Pegasus 1 Golf mang lại sự kết hợp giữa đệm ReactX êm ái và đế bám sân ổn định, phù hợp cho những vòng golf dài với cảm giác thoải mái và hiệu suất cao.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (17, N'Converse X 1908 Jogger', 3, 1, N'Unisex', N'Limited Edition', 2800000, 0, 4.9, 12, 8, N'LIMITED', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (2).png', N'Converse X 1908 Jogger là phiên bản giới hạn lấy cảm hứng từ dòng giày chạy bộ cổ điển, kết hợp phong cách retro với chất liệu cao cấp và thiết kế hiện đại dành cho cả nam và nữ.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (18, N'PUMA x HYROX Deviate NITRO™ Elite 4 Shoes Men', 4, 1, N'Men', N'Performance', 6300000, 0, 4.9, 0, 15, N'-10%', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (3).png', N'Giày chạy bộ PUMA x HYROX Deviate NITRO™ Elite 4 dành cho nam với công nghệ NITRO Elite siêu nhẹ, đế carbon, thiết kế tối ưu cho hiệu suất cao trong các cuộc thi chạy và tập luyện chuyên nghiệp.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (19, N'ADIZERO EVO SL EXO SHOES', 2, 1, N'Unisex', N'Performance', 4300000, 0, 0, 0, 25, N'NEW', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (4).png', N'Adizero EVO SL EXO Shoes - Giày chạy bộ hiệu suất cao với thiết kế siêu nhẹ, đế Lightstrike Pro và công nghệ khí động học, mang lại cảm giác chạy bền bỉ và tốc độ vượt trội cho mọi cung đường.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (20, N'Sabrina 3 EP Basketball Shoes', 1, 1, N'Women', N'Basketball', 3829000, 0, 4.7, 0, 30, N'NEW', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (5).png', N'Sabrina 3 EP - Giày bóng rổ Nike dành riêng cho nữ với thiết kế năng động, công nghệ đệm React và độ bám tối ưu trên mọi mặt sân, lấy cảm hứng từ phong cách chơi của Sabrina Ionescu.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (21, N'Converse Wave Motion Trainer', 3, 1, N'Unisex', N'Sneakers', 3100000, 0, 4.8, 16, 11, N'new', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (6).png', N'Converse Wave Motion Trainer sở hữu thiết kế chunky hiện đại với phần đế Wave nổi bật, mang lại cảm giác êm ái, ổn định và phù hợp cho cả đi chơi lẫn sử dụng hằng ngày.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (22, N'Puma Flyer Lite 3', 4, 1, N'Nữ', N'Giày chạy bộ', 1200000, 0, 4.7, 19, 14, N'new', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (7).png', N'Puma Flyer Lite 3 là mẫu giày chạy bộ nhẹ với thân giày lưới thoáng khí, đệm êm và đế ngoài bám tốt, phù hợp cho luyện tập hằng ngày hoặc di chuyển thường xuyên.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (23, N'ADIZERO TAKUMI SEN 11 SHOES', 2, 1, N'Men', N'Running', 5000000, 0, 4.9, 0, 20, N'-15%', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (8).png', N'ADIZERO TAKUMI SEN 11 - Giày chạy bộ hiệu suất cao dành cho nam với thiết kế siêu nhẹ, đệm Lightstrike Pro và công nghệ Energy Rods, tối ưu cho các cuộc đua tốc độ và chạy đường dài.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (24, N'Nike SB Dunk Low Pro Skate Shoes', 1, 1, N'Unisex', N'Skateboarding', 3239000, 0, 4.8, 0, 25, N'NEW', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (9).png', N'Nike SB Dunk Low Pro - Giày skateboard biểu tượng với thiết kế low-top linh hoạt, đế Zoom Air hỗ trợ giảm chấn và chất liệu da cao cấp, mang lại cảm giác êm ái và độ bám tốt cho mọi pha trượt.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (25, N'Adidas Lifestyle Sneakers', 2, 4, N'Unisex', N'Lifestyle', 480000, 800000, 4.5, 0, 20, N'-40%', '~/assets/img/Cap_adidas.avif', N'Giày Adidas Lifestyle với thiết kế thể thao phong cách, phù hợp cho mọi hoạt động hằng ngày. Sản phẩm thuộc chương trình End Of Season Sale 2026 với ưu đãi lên đến 50% và thêm 15% cho thành viên adiClub.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (26, N'Túi thể thao PUMA Buzz Small', 4, 4, N'Unisex', N'Túi & Balo', 800000, 1000000, 4.6, 0, 15, N'-20%', '~/assets/img/Tui_Puma.avif', N'Túi thể thao PUMA Buzz Small với thiết kế nhỏ gọn, phong cách thể thao năng động. Chất liệu bền bỉ, phù hợp mang theo khi tập gym, đi du lịch hoặc sử dụng hằng ngày. Có thể nhập mã EXTRA10 để được giảm thêm 10%.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (27, N'Converse Backpack', 3, 4, N'Unisex', N'Túi & Balo', 1250000, 0, 4.7, 0, 20, N'new', '~/assets/img/Tui_CV.webp', N'Balo Converse với thiết kế đơn giản, phong cách trẻ trung và năng động. Chất liệu bền bỉ, phù hợp cho học sinh, sinh viên hoặc sử dụng hằng ngày. Thiết kế một size phù hợp cho mọi đối tượng.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (28, N'Adidas Essentials Linear Duffel Bag Medium', 2, 4, N'Unisex', N'Túi & Balo', 645000, 950000, 4.8, 0, 25, N'-30%', '~/assets/img/tui_adidas1.avif', N'Túi duffel Adidas Essentials Linear Medium với thiết kế thể thao, năng động. Chất liệu bền bỉ, phù hợp mang theo khi tập gym, du lịch hoặc sử dụng hằng ngày. Sản phẩm thuộc chương trình End Of Season Sale 2026 với ưu đãi lên đến 50% và thêm 15% cho thành viên adiClub.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (29, N'Converse X Hello Kitty And Friends Go 2 Backpack', 3, 4, N'Unisex', N'Túi & Balo', 1550000, 0, 5.0, 0, 15, N'limited edition', '~/assets/img/0882-CONWA5874023005OSA-1.webp', N'Chiếc ba lô đa năng kết hợp phong cách siêu đáng yêu cùng các nhân vật Sanrio yêu thích. Thiết kế tiện lợi cho mọi nhu cầu di chuyển hằng ngày.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (30, N'Real Madrid Baseball Cap', 2, 3, N'Unisex', N'Mũ', 650000, 0, 4.8, 0, 30, N'new', '~/assets/img/REAL_MADRID_BASEBALL_CAP_Black_KQ9575_01_00_standard.avif', N'Chiếc mũ bóng chày Real Madrid phong cách với thiết kế ba sọc đặc trưng của Adidas và huy hiệu câu lạc bộ nổi bật. Chất liệu bền đẹp, phù hợp để đội hằng ngày hoặc cổ vũ cho đội bóng yêu thích.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (31, N'Mũ Lưỡi Trai Replica Đội Đua Scuderia Ferrari HP', 4, 3, N'Unisex', N'Mũ', 1100000, 0, 5.0, 0, 20, N'new', '~/assets/img/Mũ-Lưỡi-Trai-Replica-Đội-Đua-Scuderia-Ferrari-HP.avif', N'Mũ lưỡi trai phong cách thể thao từ bộ sưu tập Scuderia Ferrari của Puma. Thiết kế nổi bật với huy hiệu Ferrari đặc trưng, chất liệu thoáng khí, phù hợp cho người hâm mộ tốc độ và phong cách năng động.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (32, N'Adimule Slides', 2, 2, N'Unisex', N'Dép', 2700000, 0, 4.5, 129, 20, N'new', '~/assets/img/image (7).png', N'Dép Adimule với thiết kế tối giản, chất liệu da lộn cao cấp mang lại cảm giác thoải mái và sang trọng. Phong cách hiện đại phù hợp cho nhiều trang phục thường ngày.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (33, N'Dép Quai Ngang MCLAREN RACING Leadcat 2.0 Unisex', 4, 2, N'Unisex', N'Dép', 770000, 1100000, 4.8, 0, 25, N'-30%', '~/assets/img/Dép-Quai-Ngang-McLAREN-RACING-Leadcat-2.0-Unisex', N'Dép quai ngang Leadcat 2.0 hợp tác cùng McLaren Racing, mang đậm phong cách thể thao tốc độ. Thiết kế bền bỉ và thoải mái, là lựa chọn lý tưởng cho các hoạt động hằng ngày.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (34, N'Manchester United Baseball Cap', 2, 3, N'Unisex', N'Mũ', 650000, 0, 5.0, 2, 25, N'new', '~/assets/img/MANCHESTER_UNITED_BASEBALL_CAP_Red_KT0648_01_00_standard.avif', N'Nón bóng chày Manchester United với sắc đỏ nổi bật, kết hợp huy hiệu câu lạc bộ và ba sọc đặc trưng của Adidas. Phụ kiện lý tưởng dành cho người hâm mộ thể hiện niềm tự hào với đội bóng.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (35, N'Classic Cotton Bucket Hat', 2, 3, N'Unisex', N'Nón', 550000, 0, 4.7, 107, 20, N'trending', '~/assets/img/Classic_Cotton_Bucket_Hat_Black_HT2029_01_00_standard_3.avif', N'Nón bucket cổ điển từ Adidas, mang phong cách đơn giản và năng động. Chất liệu cotton thoáng mát, phù hợp để phối đồ hằng ngày.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (36, N'Day 1 Chuck Bucket Hat', 3, 3, N'Unisex', N'Nón', 550000, 0, 4.8, 0, 20, N'new', '~/assets/img/0882-CONMA5712023005O_S-1_1778955309_3.webp', N'Nón bucket Converse với thiết kế trẻ trung, nổi bật nhờ logo All Star đặc trưng. Chất liệu bền bỉ, phù hợp cho phong cách hằng ngày.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (37, N'Unwind NITROcat™ Unisex', 4, 2, N'Unisex', N'Dép', 1260000, 2100000, 5.0, 0, 15, N'new', '~/assets/img/Unwind-NITROcat™-Unisex.avif', N'Dép Unwind NITROcat™ mang đến sự êm ái tối đa với công nghệ đệm NITRO tiên tiến, thiết kế tối giản, phong cách năng động cho các hoạt động hằng ngày.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (38, N'ADIFOM IINFINITY MULES', 2, 2, N'Unisex', N'Dép', 1400000, 0, 4.5, 4, 20, N'End Of Season Sale 2026', '~/assets/img/ADIFOM_Iinfinity_Mules_Black_KJ9516_01_00_standard.png', N'Dép Adifom Iinfinity Mules với thiết kế hiện đại, độc đáo. Sản phẩm mang lại sự thoải mái tối đa cho việc sử dụng hằng ngày.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (39, N'Dép sandal quai hậu', 4, 2, N'Unisex', N'Dép', 420000, 799000, 5.0, 0, 20, N'-40%', '~/assets/img/Dép-sandal-quai-hậu.avif', N'Dép sandal quai hậu Puma với thiết kế năng động, tiện lợi. Chất liệu bền bỉ và quai dán điều chỉnh dễ dàng, phù hợp cho nhiều hoạt động ngoài trời.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (40, N'Giày 574 Core', 5, 1, N'unisex ', N'Giày Lifestyle', 1900000, 2500000, 4.2, 2277, 30, N'-25%', '~/assets/img/ml574evg_nb_03_i.jpg', N'Mẫu giày 574 Core mang tính biểu tượng của New Balance, kết hợp giữa phong cách cổ điển và sự thoải mái hiện đại, hoàn hảo cho việc sử dụng hằng ngày.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (41, N'FuelCell Rebel v5', 5, 1, N'Nam', N'Giày chạy bộ', 3700000, 0, 5.0, 0, 20, N'mới', '~/assets/img/mfcx2pp_nb_03_i.webp', N'Giày chạy bộ FuelCell Rebel v5 với công nghệ đệm cao cấp, mang lại khả năng hoàn trả năng lượng tối ưu và sự thoải mái cho các vận động viên.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (42, N'Fresh Foam X RCVRY', 5, 1, N'Unisex', N'Giày chạy bộ', 2500000, 0, 4.0, 127, 20, N'Giảm giá 25%', '~/assets/img/rcvryegy_nb_03_i_2.webp', N'Giày Fresh Foam X RCVRY với thiết kế thoải mái, hỗ trợ phục hồi sau khi vận động, chất liệu nhẹ và êm ái.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (43, N'''47 Classic Tri-logo', 5, 3, N'Unisex', N'Nón', 1000000, 0, 2.5, 6, 3, N'mới', '~/assets/img/ac40718bk_nb_03_i_2.webp', N'Nón ''47 Classic Tri-logo với thiết kế đơn giản, thời trang, phù hợp cho nhiều hoạt động thường ngày.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (44, N'Shohei Signature Hitch', 5, 3, N'Unisex', N'Nón', 950000, 0, 2.0, 2, 20, N'mới', '~/assets/img/ac0089wsnw_nb_03_i.webp', N'Nón Shohei Signature Hitch với thiết kế độc quyền, thể hiện phong cách năng động và tinh thần thể thao chuyên nghiệp.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (45, N'Túi du lịch Basketball Duffle Bag', 5, 4, N'Unisex', N'Túi & Balo', 1900000, 2700000, 5.0, 0, 20, N'Giảm giá 30%', '~/assets/img/ac4497ktre_nb_03_i.webp', N'Túi du lịch Basketball Duffle Bag tiện lợi với thiết kế bền bỉ, không gian chứa đồ rộng rãi, phù hợp cho việc mang theo dụng cụ thể thao hoặc các chuyến đi ngắn ngày.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (46, N'Balo RC Pack', 5, 4, N'Unisex', N'Túi & Balo', 2500000, 3600000, 5.0, 1, 20, N'Giảm giá 30%', '~/assets/img/ac8227ubk_nb_03_i.webp', N'Balo RC Pack với thiết kế tối giản, chất liệu bền bỉ và hiện đại, mang lại sự tiện lợi tối đa cho các hoạt động di chuyển hằng ngày.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (47, N'Nike ACG ''DAYMAX''', 1, 4, N'Unisex', N'Balo', 3569000, 0, 0, 0, 20, N'Vật liệu tái chế', '~/assets/img/ACG+DAYMAX+BKPK.avif', N'Balo Nike ACG ''DAYMAX'' dung tích 25L với ngăn chính lớn, các túi trước và bên hông dễ truy cập, cùng dây đai điều chỉnh để gắn thêm vật dụng, lý tưởng cho các chuyến phiêu lưu.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (48, N'Nike Brasilia Backpack (Medium, 24L)', 1, 4, N'Unisex', N'Balo', 1399000, 0, 0, 0, 20, N'Vật liệu tái chế', '~/assets/img/NK+BRSLA+M+BKPK+-+X+AOP+SU26.avif', N'Balo Nike Brasilia với ngăn chính rộng rãi, ngăn đựng laptop lên đến 15 inch, túi lưới bên hông đựng bình nước, cùng các ngăn khóa kéo phía trước và bên trong giúp sắp xếp đồ dùng ngăn nắp.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (49, N'Nike Dri-FIT Club Structured Swoosh Cap', 1, 3, N'Unisex', N'Nón', 559000, 0, 0, 0, 20, N'Vật liệu tái chế', '~/assets/img/U+NK+DF+CLUB+CAP+S+CB+P.avif', N'Nón Nike Dri-FIT Club với thiết kế structured và logo Swoosh đặc trưng. Sản phẩm được làm từ ít nhất 75% sợi polyester tái chế.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (50, N'Nike Apex Futura Washed Bucket Hat', 1, 3, N'Unisex', N'Nón', 659000, 0, 5.0, 0, 20, N'mới', '~/assets/img/U+NK+APEX+BUCKET+SQ+FUT+WSH+L.avif', N'Nón bucket Nike Apex với thiết kế mid-depth làm từ cotton mềm mại, nổi bật với logo Futura thêu tinh tế. Chất liệu đã qua xử lý wash mang lại cảm giác dễ chịu và vẻ ngoài cổ điển.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (51, N'200XL', 5, 2, N'Unisex', N'Dép', 1000000, 0, 4.8, 4, 20, N'mới', '~/assets/img/suf2xla1_nb_02_i.jpg', N'Dép 200XL với thiết kế hiện đại, thoải mái, phù hợp cho nhu cầu sử dụng hàng ngày.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (52, N'Nike ReactX Rejuven8', 1, 2, N'Nam', N'Dép', 2069000, 0, 0, 0, 20, N'Được đánh giá cao', '~/assets/img/NIKE+REACTX+REJUVEN8.avif', N'Giày Nike ReactX Rejuven8 mang lại sự thoải mái tối đa với công nghệ đệm ReactX tiên tiến, thiết kế hỗ trợ phục hồi và phong cách hiện đại.', 0, 1, GETDATE());
INSERT INTO dbo.Products (ProductId, ProductName, BrandId, CategoryId, TargetAudience, ProductType, Price, OldPrice, Rating, ReviewCount, Stock, Badge, ImageUrl, Description, IsFeatured, IsActive, CreatedDate) VALUES (53, N'Jordan Franchise', 1, 2, N'Unisex', N'Dép', 889000, 0, 0, 0, 20, N'Được đánh giá cao', '~/assets/img/JORDAN+FRANCHISE+SLIDE_3.avif', N'Dép Jordan Franchise được làm từ bọt bền và linh hoạt, hỗ trợ từng bước đi, có đường cong nhẹ ở lòng bàn chân giúp giữ vừa vặn an toàn và nổi bật với logo Jumpman phía trước.', 0, 1, GETDATE());

SET IDENTITY_INSERT dbo.Products OFF;
GO

INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (1, N'Hồng phấn', '#E6CFD1', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (4) (16).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (1, N'Olive Green', '#767B66', '~/assets/img/BLAZER+MID+''77+OD+NBY (3).avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (1, N'Xám', '#9ca3af', '~/assets/img/BLAZER+MID+''77+OD+NBY (2).avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (2, N'Trắng', '#f8fafc', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (17)_ma2.png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (2, N'Đen', '#111827', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (18)_ma2.png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (2, N'Xanh ngọc', '#72D2C9', '~/assets/img/image (5).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (3, N'Đen', '#000000', '~/assets/img/image (4).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (3, N'Trắng', '#FFFFFF', '~/assets/img/image (3).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (4, N'Be', '#D2B48C', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (10).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (4, N'Đen', '#000000', '~/assets/img/NIKE+P-6000 (1).avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (4, N'Trắng', '#FFFFFF', '~/assets/img/NIKE+P-6000 (2).avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (5, N'Vàng neon', '#CCFF00', '~/assets/img/Giày-bóng-đá-ULTRA-6-MATCH-TT-Unisex.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (5, N'Hồng', '#FF69B4', '~/assets/img/Giày-bóng-đá-ULTRA-6-MATCH-TT-Unisex_2.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (6, N'Đen', '#111827', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (18).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (6, N'Trắng', '#ffffff', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (17).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (7, N'Trắng', '#f8fafc', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (17)_ma2.png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (7, N'Đen', '#111827', '~/assets/img/Adizero_EVO_SL_Shoes_Grey_KJ1364_06_standard.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (7, N'Xám', '#9ca3af', '~/assets/img/Adizero_EVO_SL_Shoes_Grey_JS4489_06_standard.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (8, N'Trắng', '#F8F8FF', '~/assets/img/Adizero_EVO_SL_Shoes_Grey_KI9542_06_standard.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (9, N'Be', '#D2B48C', '~/assets/img/mr530asm_nb_03_i.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (9, N'Xám', '#9ca3af', '~/assets/img/mr530ema_nb_03_i.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (9, N'Đen', '#000000', '~/assets/img/u5309tn_nb_03_i.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (10, N'Trắng', '#FFFFFF', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (15).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (10, N'Nâu', '#8B4513', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (14).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (15, N'Xanh navy đậm', '#1A2346', '~/assets/img/Giay_Chay_Bo_Runfalcon_5_Mau_xanh_da_troi_JR5521_06_standard.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (16, N'Trắng', '#FFFFFF', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (17, N'Xanh lam', '#6BA6C8', '~/assets/img/0882-CONA21432C12W010-2.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (17, N'Xanh olive', '#7A8B3C', '~/assets/img/0882-CONA21431C00G010-2.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (17, N'Trắng', '#F5F5F5', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (2).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (17, N'Vàng kem', '#EFD87A', '~/assets/img/0882-CONA21430C00Y010-2.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (18, N'Xanh teal', '#008080', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (3).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (19, N'Trắng', '#F5F5F5', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (4).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (20, N'Trắng', '#FFFFFF', '~/assets/img/SABRINA+3+EP (1).avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (20, N'Hồng', '#FF6B8A', '~/assets/img/SABRINA+3+EP.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (20, N'Đen', '#1A1A1A', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (5).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (21, N'Trắng ', '#F5F5F0', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (6).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (21, N'Hồng phấn', '#F2D8E0', '~/assets/img/0882-CONA20009C00W06H-2.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (21, N'Đen', '#111827', '~/assets/img/0882-CONA19128C00509H-2.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (22, N'Trắng kem', '#F5F2EA', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (7).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (23, N'Xanh dương', '#1E3A8A', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (8).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (24, N'Trắng', '#FFFFFF', '~/assets/img/Untitled - June 20, 2026 at 02.06.14 (3) (9).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (25, N'Đen', '#1a2a3a', '~/assets/img/Cap_adidas.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (25, N'Đỏ', '#DC2626', '~/assets/img/Cap_adidas2.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (25, N'Tím', '#7C3AED', '~/assets/img/Cap_adidas3.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (26, N'Đen', '#111827', '~/assets/img/Tui_Puma.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (27, N'Đen', '#111827', '~/assets/img/Tui_CV.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (28, N'Đen', '#111827', '~/assets/img/tui_adidas2.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (28, N'Xanh dương', '#1E3A8A', '~/assets/img/tui_adidas1.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (28, N'Xám', '#6B7280', '~/assets/img/tui_adidas3.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (28, N'Tím', '#7C3AED', '~/assets/img/tui_adidas4.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (29, N'Đen', '#000000', '~/assets/img/0882-CONWA5874023005OSA-1.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (30, N'Đen', '#111827', '~/assets/img/REAL_MADRID_BASEBALL_CAP_Black_KQ9575_01_00_standard.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (31, N'Đỏ', '#EF4444', '~/assets/img/Mũ-Lưỡi-Trai-Replica-Đội-Đua-Scuderia-Ferrari-HP.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (32, N'Đen', '#111827', '~/assets/img/image (8).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (32, N'Cam', '#F97316', '~/assets/img/image (9).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (32, N'Be', '#D6C4B1', '~/assets/img/image (7).png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (33, N'Đen', '#111827', '~/assets/img/Dép-Quai-Ngang-McLAREN-RACING-Leadcat-2 (1).0-Unisex');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (33, N'Cam', '#FF8C00', '~/assets/img/Dép-Quai-Ngang-McLAREN-RACING-Leadcat-2.0-Unisex');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (34, N'Đỏ', '#DC2626', '~/assets/img/MANCHESTER_UNITED_BASEBALL_CAP_Red_KT0648_01_00_standard.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (35, N'Đen', '#111827', '~/assets/img/Classic_Cotton_Bucket_Hat_Black_HT2029_01_00_standard.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (35, N'Xám', '#9ca3af', '~/assets/img/Classic_Cotton_Bucket_Hat_Black_HT2029_01_00_standard_2.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (35, N'Be', '#D6C4B1', '~/assets/img/Classic_Cotton_Bucket_Hat_Black_HT2029_01_00_standard_3.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (36, N'Đen', '#000000', '~/assets/img/0882-CONMA5712023005O_S-1_1778955309.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (36, N'Kem', '#F5F5DC', '~/assets/img/0882-CONMA5712023005O_S-1_1778955309_2.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (36, N'Xanh navy', '#000080', '~/assets/img/0882-CONMA5712023005O_S-1_1778955309_3.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (37, N'Đen', '#000000', '~/assets/img/Unwind-NITROcat™-Unisex (1)_2.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (37, N'Be', '#D6C4B1', '~/assets/img/Unwind-NITROcat™-Unisex.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (38, N'Đen', '#111827', '~/assets/img/ADIFOM_Iinfinity_Mules_Black_KJ9516_01_00_standard.png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (38, N'Cam', '#F97316', '~/assets/img/ADIFOM_Iinfinity_Mules_Black_KJ9516_01_00_standard_2.png');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (39, N'Đen', '#111827', '~/assets/img/Dép-sandal-quai-hậu.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (40, N'Xám', '#71717A', '~/assets/img/ml574evg_nb_03_i.jpg');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (41, N'Cam', '#FF4500', '~/assets/img/mfcx2pp_nb_03_i.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (41, N'Xanh dương', '#007BFF', '~/assets/img/mfcx2pp_nb_03_i_2.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (41, N'Vàng nhạt', '#D0D0B5', '~/assets/img/mfcx2pp_nb_03_i_3.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (41, N'Trắng', '#FFFFFF', '~/assets/img/mfcx2pp_nb_03_i_4.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (42, N'Đen', '#000000', '~/assets/img/rcvryegy_nb_03_i_2.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (42, N'Xám', '#D3D3D3', '~/assets/img/rcvryegy_nb_03_i.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (42, N'Xanh lá', '#7FFF00', '~/assets/img/rcvryegy_nb_03_i_3.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (43, N'Nâu', '#6F5B4D', '~/assets/img/ac40718bk_nb_03_i_2.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (43, N'Đen', '#000000', '~/assets/img/ac40718bk_nb_03_i.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (44, N'Nâu', '#A0522D', '~/assets/img/ac0089wsnw_nb_03_i.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (44, N'Xanh nhạt', '#ADD8E6', '~/assets/img/ac0089wsnw_nb_03_i_2.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (45, N'Đỏ', '#DC2626', '~/assets/img/ac4497ktre_nb_03_i.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (46, N'Đen', '#000000', '~/assets/img/ac8227ubk_nb_03_i.webp');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (47, N'Đen', '#000000', '~/assets/img/ACG+DAYMAX+BKPK.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (48, N'Xanh dương', '#2B4B70', '~/assets/img/NK+BRSLA+M+BKPK+-+X+AOP+SU26.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (49, N'Đen', '#000000', '~/assets/img/U+NK+DF+CLUB+CAP+S+CB+P.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (49, N'Xanh navy', '#000080', '~/assets/img/U+NK+DF+CLUB+CAP+S+CB+P (1).avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (50, N'Đen', '#000000', '~/assets/img/U+NK+APEX+BUCKET+SQ+FUT+WSH+L.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (51, N'Đen', '#000000', '~/assets/img/suf2xla1_nb_02_i.jpg');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (52, N'Đen', '#000000', '~/assets/img/NIKE+REACTX+REJUVEN8.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (52, N'Xám', '#808080', '~/assets/img/NIKE+REACTX+REJUVEN8_3.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (52, N'Xanh lá', '#3cff00', '~/assets/img/NIKE+REACTX+REJUVEN8_2.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (53, N'Đen/Trắng', '#d8d4d4', '~/assets/img/JORDAN+FRANCHISE+SLIDE_3.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (53, N'Đen', '#000000', '~/assets/img/JORDAN+FRANCHISE+SLIDE.avif');
INSERT INTO dbo.ProductColors (ProductId, ColorName, HexCode, ImageUrl) VALUES (53, N'Trắng', '#FFFFFF', '~/assets/img/JORDAN+FRANCHISE+SLIDE_2.avif');

GO

INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (1, '39');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (1, '40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (1, '41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (1, '42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (1, '43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (2, '38');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (2, '39');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (2, '40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (2, '41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (2, '42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (2, '43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (2, '44');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '3.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '4');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '4.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '5.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '6');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '6.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '7');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '7.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '8');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '8.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '9');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '9.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '10');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '10.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '11');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (3, '12');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (4, 'EU 37.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (4, 'EU 38.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (4, 'EU 39');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (4, 'EU 40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (4, 'EU 40.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (4, 'EU 41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (4, 'EU 42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (4, 'EU 42.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (4, 'EU 43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (4, 'EU 44');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (4, 'EU 44.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (4, 'EU 45');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (4, 'EU 45.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (4, 'EU 46');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (4, 'EU 47');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (5, 'EUR 43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (6, '36');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (6, '37');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (6, '38');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (6, '39');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (6, '40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (6, '41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (6, '42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (7, '39');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (7, '40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (7, '41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (7, '42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (7, '43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (7, '44');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (8, '6.5 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (8, '7 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (8, '7.5 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (8, '8 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (8, '8.5 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (8, '9 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (8, '9.5 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (9, '38');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (9, '39');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (9, '40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (9, '41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (9, '42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (9, '43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (10, 'EU 40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (10, 'EU 40.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (10, 'EU 41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (10, 'EU 42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (10, 'EU 42.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (10, 'EU 43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (10, 'EU 44');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (10, 'EU 44.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (10, 'EU 45');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (10, 'EU 46');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (10, 'EU 47');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (10, 'EU 47.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (15, '39');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (15, '40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (15, '41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (15, '42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (15, '43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (15, '44');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (15, '45');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (16, '40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (16, '40.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (16, '41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (16, '42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (16, '42.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (16, '43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (16, '44');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (16, '44.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (16, '45');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (16, '46');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (16, '47');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (17, '36');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (17, '37');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (17, '38');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (17, '39');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (17, '40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (17, '41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (17, '42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (17, '43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (17, '44');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (18, '39');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (18, '40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (18, '40.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (18, '41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (18, '42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (18, '42.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (18, '43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (18, '44');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (18, '44.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (18, '45');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (19, '6');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (19, '6.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (19, '7');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (19, '7.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (19, '8');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (19, '8.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (19, '9');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (19, '9.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (19, '10');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (19, '10.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (19, '11');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '35.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '36');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '36.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '37.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '38');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '38.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '39');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '40.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '42.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '44');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '44.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '45');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '45.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (20, '47');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (21, '36');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (21, '37');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (21, '38');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (21, '39');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (21, '40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (21, '41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (21, '42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (21, '43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (21, '44');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (22, '35.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (22, '36');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (22, '37');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (22, '37.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (22, '38');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (22, '38.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (22, '39');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (22, '40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (23, '6');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (23, '6.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (23, '7');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (23, '7.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (23, '8');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (23, '8.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (23, '9');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (23, '9.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (23, '10');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (23, '10.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (24, '38.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (24, '39');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (24, '40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (24, '41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (24, '42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (24, '43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (24, '44');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (24, '45');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (24, '46');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (24, '47.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (25, '39');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (25, '40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (25, '41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (25, '42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (25, '43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (25, '44');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (26, 'OSFA');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (27, 'One Size');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (28, 'Medium');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (29, 'One Size');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (30, 'OSFW');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (30, 'OSFM');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (31, 'OSFA');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (32, '4 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (32, '5 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (32, '6 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (32, '7 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (32, '8 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (32, '9 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (32, '10 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (32, '11 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (33, '39');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (33, '42');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (33, '43');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (33, '44.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (34, 'OSFW');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (34, 'OSFM');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (35, 'OSFW');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (35, 'OSFM');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (36, 'One Size');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (37, '35.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (37, '37');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (37, '38');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (38, '4 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (38, '5 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (38, '6 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (38, '7 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (38, '8 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (38, '9 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (38, '10 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (38, '11 UK');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (39, 'EUR 46');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (39, 'EUR 47');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (40, '4');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (40, '4.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (40, '6');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (40, '7.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (40, '8');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (40, '8.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (40, '9');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (40, '9.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (40, '10');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (40, '10.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (40, '11');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (40, '11.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (40, '12');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (41, '7');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (41, '7.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (41, '8');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (41, '8.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (41, '9');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (41, '9.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (41, '10');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (41, '10.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (41, '11');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (41, '11.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (41, '12');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (41, '12.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (41, '13');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (41, '14');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (41, '15');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (42, 'M4/W5.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (42, 'M5/W6.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (42, 'M6/W7.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (42, 'M7/W8.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (42, 'M8/W9.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (42, 'M9/W10.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (42, 'M10/W11.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (42, 'M11/W12.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (42, 'M12/W13.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (42, 'M13/W14.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (42, 'M14/W15.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (42, 'M15/W16.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (43, 'Một kích cỡ');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (44, 'Một kích cỡ');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (45, 'Một kích cỡ');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (46, 'Một kích cỡ');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (47, 'Một kích cỡ');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (48, 'Một kích cỡ');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (49, 'S/M');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (49, 'M/L');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (49, 'L/XL');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (50, 'S');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (50, 'M');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (50, 'L');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (51, 'M4/W5.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (51, 'M5/W6.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (51, 'M6/W7.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (51, 'M7/W8.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (51, 'M10/W11.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (51, 'M11/W12.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (51, 'M12/W13.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (52, 'EU 38.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (52, 'EU 40');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (52, 'EU 41');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (52, 'EU 42.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (52, 'EU 44');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (52, 'EU 45');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (52, 'EU 46');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (52, 'EU 47.5');
INSERT INTO dbo.ProductSizes (ProductId, SizeValue) VALUES (53, 'Một kích cỡ');

GO


SET IDENTITY_INSERT dbo.Orders ON;
INSERT INTO dbo.Orders (OrderId, UserId, CustomerName, CustomerPhone, CustomerEmail, ShippingAddress, Note, TotalAmount, PaymentMethod, OrderStatus, OrderDate) VALUES
(1001, 2, N'Nguyễn Văn An', '0901234567', 'an.nguyen@example.com', N'123 CMT8, Quận 3, TP.HCM', N'Giao giờ hành chính', 3990000, 'COD', N'Đang giao', DATEADD(day, -2, GETDATE()));
SET IDENTITY_INSERT dbo.Orders OFF;
GO

INSERT INTO dbo.OrderDetails (OrderId, ProductId, SelectedColor, SelectedSize, Quantity, UnitPrice) VALUES
(1001, 1, N'Hồng phấn', '41', 1, 3990000);
GO

PRINT N'Cơ sở dữ liệu ShoeStoreDB đã được khởi tạo và chèn dữ liệu mẫu thành công!';
