USE [HomNayAnGi]
GO
/****** Object:  Table [dbo].[Ingredient]    Script Date: 10/31/2024 11:06:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredient](
	[IngredientId] [int] IDENTITY(1,1) NOT NULL,
	[IngredientName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [Ingredient_pk] PRIMARY KEY CLUSTERED 
(
	[IngredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meal]    Script Date: 10/31/2024 11:06:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meal](
	[MealId] [int] IDENTITY(1,1) NOT NULL,
	[MealName] [nvarchar](100) NOT NULL,
 CONSTRAINT [Meal_pk] PRIMARY KEY CLUSTERED 
(
	[MealId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NutritionFact]    Script Date: 10/31/2024 11:06:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NutritionFact](
	[RecipeId] [int] NOT NULL,
	[Calories] [float] NULL,
	[Protein] [float] NULL,
	[Fat] [float] NULL,
	[Carbohydrates] [float] NULL,
	[Fiber] [float] NULL,
	[Sugar] [float] NULL,
 CONSTRAINT [NutritionFact_pk] PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipe]    Script Date: 10/31/2024 11:06:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipe](
	[RecipeId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NULL,
	[RecipeName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CookTime] [int] NULL,
	[PrepTime] [int] NULL,
	[Servings] [int] NULL,
	[DifficultyLevel] [nvarchar](10) NULL,
	[UserId] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedAt] [datetime] NULL,
	[Image] [nvarchar](max) NULL,
	[Video] [nvarchar](max) NULL,
	[IsPublic] [int] NOT NULL,
 CONSTRAINT [Recipe_pk] PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeCategory]    Script Date: 10/31/2024 11:06:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeCategory](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](255) NOT NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [RecipeCategory_pk] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeComment]    Script Date: 10/31/2024 11:06:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeComment](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NULL,
	[UserId] [int] NULL,
	[Comment] [nvarchar](max) NULL,
	[Rating] [int] NULL,
	[CreatedAt] [datetime] NULL,
 CONSTRAINT [RecipeComment_pk] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeIngredient]    Script Date: 10/31/2024 11:06:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeIngredient](
	[RecipeIngredientId] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NOT NULL,
	[IngredientId] [int] NOT NULL,
	[Quantity] [float] NULL,
	[Unit] [nvarchar](50) NULL,
 CONSTRAINT [RecipeIngredient_pk] PRIMARY KEY CLUSTERED 
(
	[RecipeIngredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeMeal]    Script Date: 10/31/2024 11:06:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeMeal](
	[RecipeMealId] [int] IDENTITY(1,1) NOT NULL,
	[MealId] [int] NOT NULL,
	[RecipeId] [int] NOT NULL,
 CONSTRAINT [RecipeMeal_pk] PRIMARY KEY CLUSTERED 
(
	[RecipeMealId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeStep]    Script Date: 10/31/2024 11:06:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeStep](
	[StepId] [int] IDENTITY(1,1) NOT NULL,
	[RecipeId] [int] NULL,
	[StepNumber] [int] NULL,
	[Instruction] [nvarchar](max) NULL,
 CONSTRAINT [RecipeStep_pk] PRIMARY KEY CLUSTERED 
(
	[StepId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SignupOTP]    Script Date: 10/31/2024 11:06:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SignupOTP](
	[SignupRequestId] [varchar](100) NOT NULL,
	[OTPString] [varchar](10) NOT NULL,
	[RequestAttemptsRemains] [int] NOT NULL,
	[ExpiresAt] [datetime] NOT NULL,
 CONSTRAINT [SignupOTP_pk] PRIMARY KEY CLUSTERED 
(
	[SignupRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StepImage]    Script Date: 10/31/2024 11:06:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StepImage](
	[StepImageId] [int] IDENTITY(1,1) NOT NULL,
	[StepId] [int] NOT NULL,
	[ImageLink] [nvarchar](max) NOT NULL,
 CONSTRAINT [StepImage_pk] PRIMARY KEY CLUSTERED 
(
	[StepImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/31/2024 11:06:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[Role] [varchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [User_pk] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserFavorite]    Script Date: 10/31/2024 11:06:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserFavorite](
	[UserFavoriteId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RecipeId] [int] NOT NULL,
 CONSTRAINT [UserFavorite_pk] PRIMARY KEY CLUSTERED 
(
	[UserFavoriteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRefreshToken]    Script Date: 10/31/2024 11:06:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRefreshToken](
	[UserRefreshTokenId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RefreshToken] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
	[ExpiresAt] [datetime] NULL,
	[DeviceID] [nvarchar](max) NULL,
 CONSTRAINT [UserRefreshToken_pk] PRIMARY KEY CLUSTERED 
(
	[UserRefreshTokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Ingredient] ON 

INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (1, N'Muối', N'Gia vị phổ biến, dùng để nêm nếm nhiều món ăn.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (2, N'Đường', N'Nguyên liệu tạo ngọt, thường dùng trong món tráng miệng và đồ uống.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (3, N'Nước mắm', N'Gia vị truyền thống của Việt Nam, được lên men từ cá và muối.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (4, N'Hành lá', N'Rau thơm thường dùng để trang trí và tăng hương vị cho món ăn.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (5, N'Tỏi', N'Nguyên liệu có hương vị mạnh, thường được phi thơm trong nhiều món xào.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (6, N'Gừng', N'Thường dùng để tạo hương thơm và vị cay nhẹ cho các món ăn.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (7, N'Sả', N'Nguyên liệu có mùi thơm đặc trưng, dùng nhiều trong món nướng và kho.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (8, N'Ớt', N'Gia vị cay, thường được dùng để tăng vị cho món ăn.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (9, N'Thịt heo', N'Nguồn protein phổ biến, thường được chế biến thành các món luộc, xào, kho.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (10, N'Thịt bò', N'Nguyên liệu chính cho nhiều món ăn như bò kho, phở.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (11, N'Rau muống', N'Loại rau xanh phổ biến, thường dùng trong món luộc hoặc xào.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (12, N'Bột ngọt', N'Chất điều vị, giúp tăng hương vị cho món ăn.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (13, N'Dầu ăn', N'Dầu thực vật, dùng để chiên, xào các món ăn.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (14, N'Cà chua', N'Nguyên liệu chính trong nhiều món canh và xào.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (15, N'Nấm rơm', N'Loại nấm thường dùng trong món lẩu, canh, và xào.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (16, N'Rau răm', N'Rau thơm có mùi nồng, thường dùng với các món hải sản và trứng vịt lộn.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (17, N'Đậu hũ', N'Nguồn protein thực vật, thường dùng trong món chay và canh.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (18, N'Trứng gà', N'Nguyên liệu phổ biến trong nhiều món ăn như chiên, luộc, và nấu canh.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (19, N'Hành tím', N'Thường được phi thơm, dùng để tăng hương vị cho các món ăn.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (20, N'Mắm tôm', N'Gia vị đặc trưng của ẩm thực miền Bắc Việt Nam, thường ăn kèm với bún đậu và nhiều món khác.', NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (22, N'Test', NULL, 1)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (24, N'Hiep', NULL, 1)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (25, N'Hiệp 2', NULL, 1)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (26, N'Cà ri', NULL, 1)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (27, N'hehehee', NULL, NULL)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (28, N'Xúc xích', NULL, 1)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (29, N'Lươn', NULL, 1)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (30, N'Cơm nguội', NULL, 1)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (31, N'Mỳ tôm', NULL, 8)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (32, N'Rau ngót', NULL, 10)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (33, N'Thịt heo băm', NULL, 10)
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (34, N'Hành khô', NULL, 10)
SET IDENTITY_INSERT [dbo].[Ingredient] OFF
GO
SET IDENTITY_INSERT [dbo].[Meal] ON 

INSERT [dbo].[Meal] ([MealId], [MealName]) VALUES (1, N'Bữa sáng')
INSERT [dbo].[Meal] ([MealId], [MealName]) VALUES (2, N'Bữa trưa')
INSERT [dbo].[Meal] ([MealId], [MealName]) VALUES (3, N'Bữa tối')
INSERT [dbo].[Meal] ([MealId], [MealName]) VALUES (4, N'Ăn vặt')
SET IDENTITY_INSERT [dbo].[Meal] OFF
GO
SET IDENTITY_INSERT [dbo].[Recipe] ON 

INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (1, 1, N'Phở bò', N'Món ph? truy?n th?ng v?i nu?c dùng bò d?m dà.', 60, 30, 4, N'Medium', 1, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), N'https://tiki.vn/blog/wp-content/uploads/2023/07/thumb-12.jpg', NULL, 1)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (2, 2, N'Bún Ch?', N'Bún th?t nu?ng ki?u Hà N?i, kèm nu?c m?m pha.', 45, 20, 3, N'Medium', 2, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), N'https://vcdn1-giadinh.vnecdn.net/2021/01/08/Anh-2-8146-1610099449.jpg?w=460&h=0&q=100&dpr=2&fit=crop&s=sQNPaScgfMKYM7nJJEMJUQ', NULL, 1)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (3, 3, N'G?i Cu?n', N'Món cu?n nh? nhàng, bao g?m tôm, th?t và rau s?ng.', 15, 10, 4, N'Easy', 3, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (4, 4, N'Bánh Xèo', N'Bánh xèo giòn v?i nhân tôm th?t, an kèm rau s?ng và nu?c m?m.', 30, 15, 4, N'Medium', 4, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (5, 5, N'Com T?m', N'Com t?m su?n bì ch?, an kèm nu?c m?m chua ng?t.', 40, 20, 1, N'Easy', 1, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (6, 6, N'Chả Giò', N'Món ch? giò giòn r?m, an kèm rau s?ng và nu?c ch?m.', 30, 20, 5, N'Medium', 2, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (7, 1, N'Canh Chua Cá', N'Canh chua d?m dà v?i cá và rau qu?, v? chua ng?t hài hòa.', 25, 10, 4, N'Medium', 3, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (8, 3, N'Bánh Cu?n', N'Bánh cu?n m?m m?n nhân th?t và m?c nhi, an kèm nu?c m?m.', 20, 15, 3, N'Easy', 4, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (9, 2, N'Bún Bò Hu?', N'Bún bò Hu? cay n?ng v?i nu?c dùng d?m dà.', 90, 45, 4, N'Hard', 1, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (10, 4, N'Mì Qu?ng', N'Mì Qu?ng v?i nu?c dùng s?t, an kèm tôm, th?t và bánh da.', 50, 20, 4, N'Medium', 2, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (12, 4, N'Cơm luộc', N'Cơm luộc nước', 20, 10, 4, N'Easy', 1, CAST(N'2024-10-29T15:36:04.050' AS DateTime), CAST(N'2024-10-29T15:39:02.333' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (14, NULL, N'Rau luoc', N'Rau luộc là một món ăn ngon', 10, 5, NULL, N'Khó', NULL, CAST(N'2024-10-29T17:27:47.313' AS DateTime), CAST(N'2024-10-29T17:27:47.313' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (15, NULL, N'Ga luộc', N'Gà luộc chín thái độ', 60, 15, NULL, N'Khó', 1, CAST(N'2024-10-29T17:58:04.110' AS DateTime), CAST(N'2024-10-29T17:58:04.110' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (19, NULL, N'gdegerte', N'trgwrgtre', 184, 64, NULL, N'Khó', 1, CAST(N'2024-10-29T18:06:54.773' AS DateTime), CAST(N'2024-10-29T18:06:54.773' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (20, NULL, N'r2345345', N'6456456', 9, 785, NULL, N'Khó', 1, CAST(N'2024-10-29T18:08:06.573' AS DateTime), CAST(N'2024-10-29T18:08:06.573' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (21, NULL, N'3245', N'36457', 20, 720, NULL, N'Khó', 1, CAST(N'2024-10-29T18:08:52.780' AS DateTime), CAST(N'2024-10-29T18:08:52.780' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (22, 21, N'Súp lươn', N'Ngon', 60, 30, NULL, N'Trung bình', 1, CAST(N'2024-10-29T19:09:08.180' AS DateTime), CAST(N'2024-10-29T19:09:08.180' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (23, 29, N'Kho lá sắn', N'Cực ngon', 60, 120, NULL, N'Khó', 1, CAST(N'2024-10-29T19:11:01.280' AS DateTime), CAST(N'2024-10-29T19:11:01.280' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (46, 21, N'Lươn nướng', N'Ngon', 60, 30, NULL, N'Trung bình', 1, CAST(N'2024-10-30T14:12:52.760' AS DateTime), CAST(N'2024-10-30T14:12:52.760' AS DateTime), N'https://gofood.vn//uploads/san-pham/Hai-san-nhat-ban/hai-san-moi/luon-nhat-unagi-4.jpg', NULL, 0)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (47, 21, N'Súp lươn', N'Ngon', 60, 30, NULL, N'Khó', 1, CAST(N'2024-10-30T14:16:08.987' AS DateTime), CAST(N'2024-10-30T14:16:08.987' AS DateTime), N'https://cdn.tgdd.vn/Files/2022/11/23/1489497/cach-nau-sup-luon-thom-ngon-bo-duong-chuan-vi-nghe-an-202211230740380484.jpg', NULL, 0)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (49, 6, N'Mỳ trộn', N'Mỳ trộn là món ăn nhanh gọn, kết hợp mỳ mềm dai với nước sốt đậm đà từ xì dầu, dầu hào, và tương ớt. Thêm rau thơm, hành phi, đậu phộng rang để tăng hương vị. Món này dễ biến tấu với các loại thịt như bò, gà, hoặc tôm, phù hợp cho bữa ăn tiện lợi và ngon miệng.', 15, 5, NULL, N'Dễ', 8, CAST(N'2024-10-30T17:10:27.273' AS DateTime), CAST(N'2024-10-30T17:10:27.273' AS DateTime), N'https://cdn.tgdd.vn/2021/07/CookProduct/mitrontrunglongdao-1200x676.jpg', NULL, 0)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (50, NULL, N'hehe', NULL, NULL, NULL, NULL, NULL, 8, CAST(N'2024-10-30T17:37:22.557' AS DateTime), CAST(N'2024-10-30T17:37:22.557' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (57, 3, N'Canh rau ngót', N'Canh ngon mùa hè', 30, 10, NULL, N'Dễ', 10, CAST(N'2024-10-31T02:02:28.540' AS DateTime), CAST(N'2024-10-31T02:02:28.540' AS DateTime), N'https://cdn2.fptshop.com.vn/unsafe/1920x0/filters:quality(100)/2023_10_10_638325371086947278_cach-nau-canh-rau-ngot.jpg', NULL, 0)
SET IDENTITY_INSERT [dbo].[Recipe] OFF
GO
SET IDENTITY_INSERT [dbo].[RecipeCategory] ON 

INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (1, N'Ẩm thực đường phố', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (2, N'Bánh', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (3, N'Canh', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (4, N'Cơm', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (5, N'Món chiên', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (6, N'Mì', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (7, N'Miến', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (8, N'Món hầm', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (9, N'Món khai vị', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (10, N'Món mặn', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (11, N'Món ngọt', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (12, N'Món nướng', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (13, N'Món tráng miệng', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (14, N'Nước xốt', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (15, N'Salad', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (16, N'Thức ăn chơi', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (17, N'Thức ăn dịp lễ hội', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (18, N'Thức ăn nhanh', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (19, N'Thức ăn nhẹ', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (20, N'Thức ăn từ thuỷ sản', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (21, N'Đặc sản Nghệ An', 1)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (22, N'test', 1)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (27, N'aaaa', 1)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (28, N'Món ăn của Hiệp', 1)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (29, N'Đặc sản Phú Yên', 1)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (31, N'Đặc sản Hà Nội', 1)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (32, N'Tuấn Anh', 1)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (33, N'Đồ ăn Hoà Lạc', 1)
SET IDENTITY_INSERT [dbo].[RecipeCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[RecipeComment] ON 

INSERT [dbo].[RecipeComment] ([CommentId], [RecipeId], [UserId], [Comment], [Rating], [CreatedAt]) VALUES (1, NULL, NULL, NULL, 0, CAST(N'2024-10-31T08:27:23.710' AS DateTime))
INSERT [dbo].[RecipeComment] ([CommentId], [RecipeId], [UserId], [Comment], [Rating], [CreatedAt]) VALUES (2, 1, 1, N'This is a sample comment.', 5, CAST(N'2024-10-31T08:53:43.893' AS DateTime))
INSERT [dbo].[RecipeComment] ([CommentId], [RecipeId], [UserId], [Comment], [Rating], [CreatedAt]) VALUES (9, 1, 1, N'string', 1, CAST(N'2024-10-31T09:20:36.180' AS DateTime))
INSERT [dbo].[RecipeComment] ([CommentId], [RecipeId], [UserId], [Comment], [Rating], [CreatedAt]) VALUES (10, 1, 3, N'string', 0, CAST(N'2024-10-31T09:28:10.080' AS DateTime))
SET IDENTITY_INSERT [dbo].[RecipeComment] OFF
GO
SET IDENTITY_INSERT [dbo].[RecipeIngredient] ON 

INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (15, 47, 2, 1.5, N'muỗng')
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (16, 47, 29, 5, N'con')
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (17, 47, 19, 0.5, N'củ')
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (21, 49, 31, 1, N'vắt')
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (22, 49, 28, 2, N'cái')
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (23, 49, 18, 2, N'cái')
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (25, 57, 32, 1, N'bó nhỏ')
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (26, 57, 33, 100, N'gram')
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (27, 57, 34, 1, N'củ')
SET IDENTITY_INSERT [dbo].[RecipeIngredient] OFF
GO
SET IDENTITY_INSERT [dbo].[RecipeStep] ON 

INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (1, 47, 1, N'Sơ chế lươn')
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (2, 47, 2, N'Cho hết nguyên liệu vào')
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (3, 47, 3, N'Let him cook')
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (8, 49, 1, N'Trụng mì với nước sôi rồi vớt ra')
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (9, 49, 2, N'Ốp trứng và rán xúc xích')
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (10, 49, 3, N'Để vào tô các nguyên liệu và trộn đều')
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (15, 57, 1, N'Rửa và vò nát rau ngót')
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (16, 57, 2, N'Cắt nhỏ hành khô')
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (17, 57, 3, N'Cho rau ngót và thịt băm vào xào với dầu trong 2 phút')
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (18, 57, 4, N'Cho 650ml nước vào')
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (19, 57, 5, N'Nêm nếm đủ dùng')
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (20, 57, 6, N'Nấu trong 10 phút')
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (21, 57, 7, N'Ăn')
SET IDENTITY_INSERT [dbo].[RecipeStep] OFF
GO
INSERT [dbo].[SignupOTP] ([SignupRequestId], [OTPString], [RequestAttemptsRemains], [ExpiresAt]) VALUES (N'0a285afd-a39b-4f39-9ca6-314975b98568', N'498771', 2, CAST(N'2024-10-29T10:08:22.297' AS DateTime))
INSERT [dbo].[SignupOTP] ([SignupRequestId], [OTPString], [RequestAttemptsRemains], [ExpiresAt]) VALUES (N'0f93b74b-3682-4103-bec0-f2928c650665', N'200242', 3, CAST(N'2024-10-29T10:50:09.990' AS DateTime))
INSERT [dbo].[SignupOTP] ([SignupRequestId], [OTPString], [RequestAttemptsRemains], [ExpiresAt]) VALUES (N'168253b5-1ead-46a5-b1c0-e6ef211d8dc9', N'496324', 3, CAST(N'2024-10-30T15:26:42.023' AS DateTime))
INSERT [dbo].[SignupOTP] ([SignupRequestId], [OTPString], [RequestAttemptsRemains], [ExpiresAt]) VALUES (N'750205ad-87dc-44ee-ac9c-6312ff29d029', N'642045', 3, CAST(N'2024-10-29T10:22:41.627' AS DateTime))
INSERT [dbo].[SignupOTP] ([SignupRequestId], [OTPString], [RequestAttemptsRemains], [ExpiresAt]) VALUES (N'febdcbcd-0252-411a-9dd3-b2ce6b768492', N'695555', 3, CAST(N'2024-10-29T10:47:37.160' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (1, N'tranhiep', N'hieptran.pa@gmail.com', N'bfddf8322176ac637616d198f6327f75dd4f8d0be91ae2bd51261c686bef2510', CAST(N'2024-10-28T16:14:04.223' AS DateTime), N'USER', 1)
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (2, N'fewfwef', N'sdf@gmail.com', N'bfddf8322176ac637616d198f6327f75dd4f8d0be91ae2bd51261c686bef2510', CAST(N'2024-10-28T16:18:38.987' AS DateTime), N'USER', 1)
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (3, N'tranhiep2', N'hiep@maol.cpl', N'bfddf8322176ac637616d198f6327f75dd4f8d0be91ae2bd51261c686bef2510', CAST(N'2024-10-28T17:00:04.213' AS DateTime), N'USER', 1)
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (4, N'tranhiep22', N'hieptran.pa2@gmail.com', N'2769112f253df4764a72cb026d5f3a11d237e4c4435b3f385a14054d6be903fe', CAST(N'2024-10-28T17:07:43.070' AS DateTime), N'USER', 1)
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (5, N'dskjf', N'hfwefe@gmail.com', N'bfddf8322176ac637616d198f6327f75dd4f8d0be91ae2bd51261c686bef2510', CAST(N'2024-10-28T17:18:23.133' AS DateTime), N'USER', 1)
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (6, N'hieptran', N'hieptvhe173252@gmail.com', N'bfddf8322176ac637616d198f6327f75dd4f8d0be91ae2bd51261c686bef2510', CAST(N'2024-10-28T17:36:38.853' AS DateTime), N'USER', 1)
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (7, N'hieptv', N'hieptv@gmail.com', N'bfddf8322176ac637616d198f6327f75dd4f8d0be91ae2bd51261c686bef2510', CAST(N'2024-10-29T10:28:18.260' AS DateTime), N'USER', 1)
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (8, N'trhiwp', N'hiwp@gmail.com', N'bfddf8322176ac637616d198f6327f75dd4f8d0be91ae2bd51261c686bef2510', CAST(N'2024-10-29T15:16:22.910' AS DateTime), N'USER', 1)
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (9, N'trunghieu', N'kininmontal2003@gmail.com', N'd601ea2d3603a0ca6f97b1a2205f25d0a2ce83e9f5fe9df371c250a52501198f', CAST(N'2024-10-30T17:30:02.383' AS DateTime), N'USER', 1)
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (10, N'hieptvhe173252', N'hieptvhe173252@fpt.edu.vn', N'bfddf8322176ac637616d198f6327f75dd4f8d0be91ae2bd51261c686bef2510', CAST(N'2024-10-31T01:53:04.050' AS DateTime), N'USER', 1)
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (11, N'hieptest', N'tranhiep.1326@gmail.com', N'8bf36f8f22946050ddc06204a3890c5de30ad7c057c1d104f9e032e25397a38a', CAST(N'2024-10-31T01:59:32.043' AS DateTime), N'USER', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserFavorite] ON 

INSERT [dbo].[UserFavorite] ([UserFavoriteId], [UserId], [RecipeId]) VALUES (4, 9, 57)
SET IDENTITY_INSERT [dbo].[UserFavorite] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRefreshToken] ON 

INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (2, 1, N'G/2fU5CKudYbnX3+UV1F+1aQy+TRwbLDXvdFuFY67vE=', CAST(N'2024-10-29T15:07:17.197' AS DateTime), CAST(N'2024-11-29T15:07:17.197' AS DateTime), N'2')
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (3, 1, N'bqMATQWZ2SPRxEMwHJtthaqgjYaEfbFCu108kiB+cX4=', CAST(N'2024-10-29T15:07:21.480' AS DateTime), CAST(N'2024-11-29T15:07:21.480' AS DateTime), N'3')
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (4, 1, N'TGWW1rdZIR0GRKVkP7UzukBzPZCQfxtvcFrnbU2fOEk=', CAST(N'2024-10-29T15:07:48.713' AS DateTime), CAST(N'2024-11-29T15:07:48.713' AS DateTime), N'1')
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (17, 9, N'Y7O62rEZ2215LHm1nwW8PP6cvKWxtnr6zJ1X3w13shI=', CAST(N'2024-10-31T00:30:10.170' AS DateTime), CAST(N'2024-11-30T00:30:10.170' AS DateTime), N'62ec91c4-955e-4692-87fe-cd13c62425a7')
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (25, 9, N'9m27IwWVox6hxLx1RP6CFrWOUF0JLAC/1Z0lWXsHePk=', CAST(N'2024-10-31T09:57:42.090' AS DateTime), CAST(N'2024-11-30T09:57:42.090' AS DateTime), N'729634c6-a4c3-42a3-8a1d-776807f8b04e')
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (26, 8, N'LPNDMDWbcWvk+hM0TYFHOWiq8eMQsouHQ7jv3Qw+LAw=', CAST(N'2024-10-31T10:42:01.630' AS DateTime), CAST(N'2024-11-30T10:42:01.630' AS DateTime), N'90eaa4bd-bede-4c3d-90d8-26a9526d99f5')
SET IDENTITY_INSERT [dbo].[UserRefreshToken] OFF
GO
ALTER TABLE [dbo].[Recipe] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Recipe] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[RecipeComment] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Ingredient]  WITH CHECK ADD  CONSTRAINT [Ingredient_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Ingredient] CHECK CONSTRAINT [Ingredient_User]
GO
ALTER TABLE [dbo].[NutritionFact]  WITH CHECK ADD  CONSTRAINT [FK_5] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([RecipeId])
GO
ALTER TABLE [dbo].[NutritionFact] CHECK CONSTRAINT [FK_5]
GO
ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD  CONSTRAINT [Recipe_RecipeCategory] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[RecipeCategory] ([CategoryId])
GO
ALTER TABLE [dbo].[Recipe] CHECK CONSTRAINT [Recipe_RecipeCategory]
GO
ALTER TABLE [dbo].[RecipeCategory]  WITH CHECK ADD  CONSTRAINT [RecipeCategory_User] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[RecipeCategory] CHECK CONSTRAINT [RecipeCategory_User]
GO
ALTER TABLE [dbo].[RecipeComment]  WITH CHECK ADD  CONSTRAINT [FK_8] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([RecipeId])
GO
ALTER TABLE [dbo].[RecipeComment] CHECK CONSTRAINT [FK_8]
GO
ALTER TABLE [dbo].[RecipeComment]  WITH CHECK ADD  CONSTRAINT [FK_9] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[RecipeComment] CHECK CONSTRAINT [FK_9]
GO
ALTER TABLE [dbo].[RecipeIngredient]  WITH CHECK ADD  CONSTRAINT [FK_2] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([RecipeId])
GO
ALTER TABLE [dbo].[RecipeIngredient] CHECK CONSTRAINT [FK_2]
GO
ALTER TABLE [dbo].[RecipeIngredient]  WITH CHECK ADD  CONSTRAINT [FK_3] FOREIGN KEY([IngredientId])
REFERENCES [dbo].[Ingredient] ([IngredientId])
GO
ALTER TABLE [dbo].[RecipeIngredient] CHECK CONSTRAINT [FK_3]
GO
ALTER TABLE [dbo].[RecipeMeal]  WITH CHECK ADD  CONSTRAINT [RecipeMeal_Meal] FOREIGN KEY([MealId])
REFERENCES [dbo].[Meal] ([MealId])
GO
ALTER TABLE [dbo].[RecipeMeal] CHECK CONSTRAINT [RecipeMeal_Meal]
GO
ALTER TABLE [dbo].[RecipeMeal]  WITH CHECK ADD  CONSTRAINT [RecipeMeal_Recipe] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([RecipeId])
GO
ALTER TABLE [dbo].[RecipeMeal] CHECK CONSTRAINT [RecipeMeal_Recipe]
GO
ALTER TABLE [dbo].[RecipeStep]  WITH CHECK ADD  CONSTRAINT [FK_4] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([RecipeId])
GO
ALTER TABLE [dbo].[RecipeStep] CHECK CONSTRAINT [FK_4]
GO
ALTER TABLE [dbo].[StepImage]  WITH CHECK ADD  CONSTRAINT [StepImage_RecipeStep] FOREIGN KEY([StepId])
REFERENCES [dbo].[RecipeStep] ([StepId])
GO
ALTER TABLE [dbo].[StepImage] CHECK CONSTRAINT [StepImage_RecipeStep]
GO
ALTER TABLE [dbo].[UserFavorite]  WITH CHECK ADD  CONSTRAINT [FK_6] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserFavorite] CHECK CONSTRAINT [FK_6]
GO
ALTER TABLE [dbo].[UserFavorite]  WITH CHECK ADD  CONSTRAINT [FK_7] FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([RecipeId])
GO
ALTER TABLE [dbo].[UserFavorite] CHECK CONSTRAINT [FK_7]
GO
ALTER TABLE [dbo].[UserRefreshToken]  WITH CHECK ADD  CONSTRAINT [UserRefreshToken_User_fk] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserRefreshToken] CHECK CONSTRAINT [UserRefreshToken_User_fk]
GO
