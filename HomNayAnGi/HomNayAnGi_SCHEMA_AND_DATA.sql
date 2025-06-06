USE [HomNayAnGi]
GO
/****** Object:  Table [dbo].[Ingredient]    Script Date: 11/7/2024 11:11:23 AM ******/
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
/****** Object:  Table [dbo].[Meal]    Script Date: 11/7/2024 11:11:23 AM ******/
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
/****** Object:  Table [dbo].[NutritionFact]    Script Date: 11/7/2024 11:11:23 AM ******/
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
/****** Object:  Table [dbo].[Recipe]    Script Date: 11/7/2024 11:11:23 AM ******/
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
/****** Object:  Table [dbo].[RecipeCategory]    Script Date: 11/7/2024 11:11:23 AM ******/
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
/****** Object:  Table [dbo].[RecipeComment]    Script Date: 11/7/2024 11:11:23 AM ******/
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
	[ParentCommentId] [int] NULL,
 CONSTRAINT [RecipeComment_pk] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeIngredient]    Script Date: 11/7/2024 11:11:23 AM ******/
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
/****** Object:  Table [dbo].[RecipeMeal]    Script Date: 11/7/2024 11:11:23 AM ******/
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
/****** Object:  Table [dbo].[RecipeStep]    Script Date: 11/7/2024 11:11:23 AM ******/
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
/****** Object:  Table [dbo].[SignupOTP]    Script Date: 11/7/2024 11:11:23 AM ******/
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
/****** Object:  Table [dbo].[StepImage]    Script Date: 11/7/2024 11:11:23 AM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 11/7/2024 11:11:23 AM ******/
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
/****** Object:  Table [dbo].[UserFavorite]    Script Date: 11/7/2024 11:11:23 AM ******/
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
/****** Object:  Table [dbo].[UserRefreshToken]    Script Date: 11/7/2024 11:11:23 AM ******/
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
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (1, N'Muối', N'Gia vị phổ biến, dùng để nêm nếm nhiều món ăn.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (2, N'Đường', N'Nguyên liệu tạo ngọt, thường dùng trong món tráng miệng và đồ uống.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (3, N'Nước mắm', N'Gia vị truyền thống của Việt Nam, được lên men từ cá và muối.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (4, N'Hành lá', N'Rau thơm thường dùng để trang trí và tăng hương vị cho món ăn.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (5, N'Tỏi', N'Nguyên liệu có hương vị mạnh, thường được phi thơm trong nhiều món xào.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (6, N'Gừng', N'Thường dùng để tạo hương thơm và vị cay nhẹ cho các món ăn.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (7, N'Sả', N'Nguyên liệu có mùi thơm đặc trưng, dùng nhiều trong món nướng và kho.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (8, N'Ớt', N'Gia vị cay, thường được dùng để tăng vị cho món ăn.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (9, N'Thịt heo', N'Nguồn protein phổ biến, thường được chế biến thành các món luộc, xào, kho.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (10, N'Thịt bò', N'Nguyên liệu chính cho nhiều món ăn như bò kho, phở.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (11, N'Rau muống', N'Loại rau xanh phổ biến, thường dùng trong món luộc hoặc xào.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (12, N'Bột ngọt', N'Chất điều vị, giúp tăng hương vị cho món ăn.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (13, N'Dầu ăn', N'Dầu thực vật, dùng để chiên, xào các món ăn.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (14, N'Cà chua', N'Nguyên liệu chính trong nhiều món canh và xào.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (15, N'Nấm rơm', N'Loại nấm thường dùng trong món lẩu, canh, và xào.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (16, N'Rau răm', N'Rau thơm có mùi nồng, thường dùng với các món hải sản và trứng vịt lộn.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (17, N'Đậu hũ', N'Nguồn protein thực vật, thường dùng trong món chay và canh.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (18, N'Trứng gà', N'Nguyên liệu phổ biến trong nhiều món ăn như chiên, luộc, và nấu canh.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (19, N'Hành tím', N'Thường được phi thơm, dùng để tăng hương vị cho các món ăn.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (20, N'Mắm tôm', N'Gia vị đặc trưng của ẩm thực miền Bắc Việt Nam, thường ăn kèm với bún đậu và nhiều món khác.', NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (22, N'Test', NULL, 1)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (24, N'Hiep', NULL, 1)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (25, N'Hiệp 2', NULL, 1)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (26, N'Cà ri', NULL, 1)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (27, N'hehehee', NULL, NULL)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (28, N'Xúc xích', NULL, 1)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (29, N'Lươn', NULL, 1)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (30, N'Cơm nguội', NULL, 1)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (31, N'Mỳ tôm', NULL, 8)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (32, N'Rau ngót', NULL, 10)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (33, N'Thịt heo băm', NULL, 10)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (34, N'Hành khô', NULL, 10)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (35, N'Cơm', NULL, 9)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (36, N'Gạo', NULL, 9)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (37, N'Nước', NULL, 9)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (38, N'Gạo', NULL, 1)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (39, N'Nước', NULL, 1)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (40, N'Gạo', NULL, 1)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (41, N'tài', NULL, 14)
GO
INSERT [dbo].[Ingredient] ([IngredientId], [IngredientName], [Description], [CreatedBy]) VALUES (42, N'tài', NULL, 14)
GO
SET IDENTITY_INSERT [dbo].[Ingredient] OFF
GO
SET IDENTITY_INSERT [dbo].[Meal] ON 
GO
INSERT [dbo].[Meal] ([MealId], [MealName]) VALUES (1, N'Bữa sáng')
GO
INSERT [dbo].[Meal] ([MealId], [MealName]) VALUES (2, N'Bữa trưa')
GO
INSERT [dbo].[Meal] ([MealId], [MealName]) VALUES (3, N'Bữa tối')
GO
INSERT [dbo].[Meal] ([MealId], [MealName]) VALUES (4, N'Ăn vặt')
GO
SET IDENTITY_INSERT [dbo].[Meal] OFF
GO
INSERT [dbo].[NutritionFact] ([RecipeId], [Calories], [Protein], [Fat], [Carbohydrates], [Fiber], [Sugar]) VALUES (47, 4, 2, 3, NULL, 4, 3)
GO
INSERT [dbo].[NutritionFact] ([RecipeId], [Calories], [Protein], [Fat], [Carbohydrates], [Fiber], [Sugar]) VALUES (57, 20, 2, 4, 5, 8, 1)
GO
SET IDENTITY_INSERT [dbo].[Recipe] ON 
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (1, 1, N'Phở bò', N'Món phở truyền thống với nuớc dùng bò đậm dà.', 60, 30, 4, N'Medium', 1, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), N'https://tiki.vn/blog/wp-content/uploads/2023/07/thumb-12.jpg', NULL, 1)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (2, 2, N'Bún Chả', N'Bún thịt nướng kiểu Hà Nội, kèm nước mắm pha.






', 45, 20, 3, N'Medium', 2, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), N'https://vcdn1-giadinh.vnecdn.net/2021/01/08/Anh-2-8146-1610099449.jpg?w=460&h=0&q=100&dpr=2&fit=crop&s=sQNPaScgfMKYM7nJJEMJUQ', NULL, 1)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (3, 3, N'Gỏi Cuốn', N'Món cuốn nhẹ nhàng, bao gồm tôm, thịt và rau sống.', 15, 10, 4, N'Easy', 3, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), N'https://upload.wikimedia.org/wikipedia/commons/0/01/Goi_Cuon_VN.jpg', NULL, 1)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (4, 4, N'Bánh Xèo', N'Bánh xèo giòn với nhân tôm thịt, ăn kèm rau sống và nước mắm.


', 30, 15, 4, N'Medium', 4, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), N'https://ik.imagekit.io/tvlk/blog/2022/08/banh-xeo-dac-san-o-dau-3.jpeg?tr=c-at_max?tr=c-at_max', NULL, 1)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (5, 5, N'Com Tấm', N'Cơm tấm sườn bì chả, ăn kèm nước mắm chua ngọt.






', 40, 20, 1, N'Easy', 1, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), N'https://upload.wikimedia.org/wikipedia/commons/thumb/b/b0/C%C6%A1m_T%E1%BA%A5m%2C_Da_Nang%2C_Vietnam.jpg/1200px-C%C6%A1m_T%E1%BA%A5m%2C_Da_Nang%2C_Vietnam.jpg', NULL, 1)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (6, 6, N'Chả Giò', N'Món chả giò giòn rụm, ăn kèm rau sống và nước chấm.






', 30, 20, 5, N'Medium', 2, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), N'https://assets.unileversolutions.com/recipes-v2/157768.jpg', NULL, 1)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (7, 1, N'Canh Chua Cá', N'Canh chua đậm đà với cá và rau quả, vị chua ngọt hài hòa.






', 25, 10, 4, N'Medium', 3, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), N'https://i-giadinh.vnecdn.net/2021/03/19/ca2-1616122035-2163-1616122469.jpg', NULL, 1)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (8, 3, N'Bánh Cuốn', N'Canh chua đậm đà với cá và rau quả, vị chua ngọt hài hòa.






', 20, 15, 3, N'Easy', 4, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), N'https://upload.wikimedia.org/wikipedia/commons/thumb/8/8c/Banh_Cuon_VN.jpg/640px-Banh_Cuon_VN.jpg', NULL, 1)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (9, 2, N'Bún Bò Huế', N'Bún bò Huế cay nồng với nước dùng đậm đà.






', 90, 45, 4, N'Hard', 1, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), N'https://t2.ex-cdn.com/crystalbay.com/resize/1860x570/files/news/2024/08/15/bun-bo-huemon-an-dac-san-noi-tieng-cua-vung-co-do-093837.jpg', NULL, 1)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (10, 4, N'Mì Quảng', N'Mì Quảng với nước dùng sệt, ăn kèm tôm, thịt và bánh đa.






', 50, 20, 4, N'Medium', 2, CAST(N'2024-10-29T15:08:54.483' AS DateTime), CAST(N'2024-10-29T15:08:54.483' AS DateTime), N'https://cdn.tgdd.vn/2021/02/CookProduct/1200-1200x676-16.jpg', NULL, 1)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (12, 4, N'Cơm luộc', N'Cơm luộc nước', 20, 10, 4, N'Easy', 1, CAST(N'2024-10-29T15:36:04.050' AS DateTime), CAST(N'2024-10-29T15:39:02.333' AS DateTime), N'https://nethue.com.vn/temp/-uploaded-san%20pham_com%20thit%20luoc_cr_521x521.jpg', NULL, 1)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (14, 4, N'Rau luoc', N'Rau luộc là một món ăn ngon', 10, 5, 2, N'Khó', 1, CAST(N'2024-10-29T17:27:47.313' AS DateTime), CAST(N'2024-10-29T17:27:47.313' AS DateTime), N'https://media-cdn-v2.laodong.vn/Storage/NewsPortal/2021/2/24/883194/1562561623-844-Thumb.jpg', NULL, 0)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (15, 2, N'Gà luộc', N'Gà luộc chín thái độ', 60, 15, 2, N'Khó', 1, CAST(N'2024-10-29T17:58:04.110' AS DateTime), CAST(N'2024-10-29T17:58:04.110' AS DateTime), N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ6DVxEfOs4voFwDAiC1W3c5Te8xepSYR3BLg&s', NULL, 0)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (20, 1, N'Bánh Khọt Vũng Tàu', N'Bánh khọt giòn, nhân tôm ngọt, ăn kèm rau và nước mắm.






', 9, 785, 2, N'Khó', 1, CAST(N'2024-10-29T18:08:06.573' AS DateTime), CAST(N'2024-10-29T18:08:06.573' AS DateTime), N'https://bazantravel.com/cdn/medias/uploads/82/82520-banh-khot-vung-tau-700x700.jpg', NULL, 0)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (21, 3, N'Nem Nướng Nha Trang', N'Nem nướng thơm lừng, chấm cùng nước chấm đặc biệt từ đậu phộng xay nhuyễn, ăn kèm rau sống và bánh tráng giòn.', 20, 720, 3, N'Khó', 1, CAST(N'2024-10-29T18:08:52.780' AS DateTime), CAST(N'2024-10-29T18:08:52.780' AS DateTime), N'https://cdn.tgdd.vn/2021/09/CookDish/cach-lam-nem-nuong-nha-trang-bang-noi-chien-khong-dau-thom-avt-1200x676.jpg', NULL, 0)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (22, 21, N'Lẩu Mắm Miền Tây', N'Lẩu mắm đậm đà, thơm hương rau đồng quê, điên điển, bông súng, cá và tôm tươi.







', 60, 30, 2, N'Trung bình', 1, CAST(N'2024-10-29T19:09:08.180' AS DateTime), CAST(N'2024-10-29T19:09:08.180' AS DateTime), N'https://i.ytimg.com/vi/3goLsgW6Nfk/maxresdefault.jpg', NULL, 0)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (23, 29, N'Hủ Tiếu Nam Vang Đặc Biệt', N'Hủ tiếu dai ngon, thịt tôm mực trứng cút, nước dùng thanh.






', 60, 120, 5, N'Khó', 1, CAST(N'2024-10-29T19:11:01.280' AS DateTime), CAST(N'2024-10-29T19:11:01.280' AS DateTime), N'https://barona.vn/storage/cong-thuc/hu-tieu-nam-vang.jpeg', NULL, 0)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (46, 21, N'Bánh Canh Cua Sài Gòn', N'Bánh canh cua thơm ngon với nước dùng sánh mịn, hương vị đậm đà từ cua tươi, kết hợp với chả và trứng cút hấp dẫn.', 60, 30, 3, N'Trung bình', 1, CAST(N'2024-10-30T14:12:52.760' AS DateTime), CAST(N'2024-10-30T14:12:52.760' AS DateTime), N'https://gofood.vn//uploads/san-pham/Hai-san-nhat-ban/hai-san-moi/luon-nhat-unagi-4.jpg', NULL, 0)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (47, 21, N'Bún Riêu Cua Thanh Mát', N'Nước dùng chua nhẹ, thơm cua đồng, kết hợp cà chua, đậu hũ, hành phi, mắm tôm, đậm đà khó quên.







', 60, 30, 5, N'Khó', 1, CAST(N'2024-10-30T14:16:08.987' AS DateTime), CAST(N'2024-10-30T14:16:08.987' AS DateTime), N'https://cdn.tgdd.vn/Files/2022/11/23/1489497/cach-nau-sup-luon-thom-ngon-bo-duong-chuan-vi-nghe-an-202211230740380484.jpg', NULL, 0)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (49, 6, N'Mỳ trộn', N'Mỳ trộn là món ăn nhanh gọn, kết hợp mỳ mềm dai với nước sốt đậm đà từ xì dầu, dầu hào, và tương ớt. Thêm rau thơm, hành phi, đậu phộng rang để tăng hương vị. Món này dễ biến tấu với các loại thịt như bò, gà, hoặc tôm, phù hợp cho bữa ăn tiện lợi và ngon miệng.', 15, 5, 3, N'Dễ', 8, CAST(N'2024-10-30T17:10:27.273' AS DateTime), CAST(N'2024-10-30T17:10:27.273' AS DateTime), N'https://cdn.tgdd.vn/2021/07/CookProduct/mitrontrunglongdao-1200x676.jpg', NULL, 0)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (50, NULL, N'Gà Hấp Muối Sả', N'Gà hấp muối sả, da giòn, thịt mềm, ăn kèm muối tiêu chanh.






', 50, 60, 5, N'Dễ', 8, CAST(N'2024-10-30T17:37:22.557' AS DateTime), CAST(N'2024-10-30T17:37:22.557' AS DateTime), N'https://img-global.cpcdn.com/recipes/55f5201150f9bcc6/680x482cq70/ga-h%E1%BA%A5p-mu%E1%BB%91i-s%E1%BA%A3-la-chanh-phien-b%E1%BA%A3n-m%E1%BB%9Bi-recipe-main-photo.jpg', NULL, 0)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (57, 3, N'Canh rau ngót', N'Canh ngon mùa hè', 30, 10, NULL, N'Dễ', 10, CAST(N'2024-10-31T02:02:28.540' AS DateTime), CAST(N'2024-10-31T02:02:28.540' AS DateTime), N'https://cdn2.fptshop.com.vn/unsafe/1920x0/filters:quality(100)/2023_10_10_638325371086947278_cach-nau-canh-rau-ngot.jpg', NULL, 0)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (70, 1, N'Banh', N'Banh', 20, 15, NULL, N'Trung bình', 1, CAST(N'2024-11-05T16:48:39.683' AS DateTime), CAST(N'2024-11-05T16:48:39.683' AS DateTime), N'https://canhdep.net/images/thumbs/2016/04/mon-an-ngon.jpg', NULL, 0)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (71, 2, N'Banh bao', N'banh bao', 20, 20, NULL, N'Khó', 1, CAST(N'2024-11-05T16:51:05.163' AS DateTime), CAST(N'2024-11-05T16:51:05.163' AS DateTime), N'https://canhdep.net/images/thumbs/2016/04/mon-an-ngon.jpg', NULL, 0)
GO
INSERT [dbo].[Recipe] ([RecipeId], [CategoryId], [RecipeName], [Description], [CookTime], [PrepTime], [Servings], [DifficultyLevel], [UserId], [CreatedAt], [UpdatedAt], [Image], [Video], [IsPublic]) VALUES (72, 1, N'bà', N'thịt tươi', 10, 15, NULL, N'Dễ', 14, CAST(N'2024-11-05T17:20:31.973' AS DateTime), CAST(N'2024-11-05T17:20:31.973' AS DateTime), NULL, NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[Recipe] OFF
GO
SET IDENTITY_INSERT [dbo].[RecipeCategory] ON 
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (1, N'Ẩm thực đường phố', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (2, N'Bánh', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (3, N'Canh', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (4, N'Cơm', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (5, N'Món chiên', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (6, N'Mì', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (7, N'Miến', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (8, N'Món hầm', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (9, N'Món khai vị', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (10, N'Món mặn', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (11, N'Món ngọt', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (12, N'Món nướng', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (13, N'Món tráng miệng', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (14, N'Nước xốt', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (15, N'Salad', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (16, N'Thức ăn chơi', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (17, N'Thức ăn dịp lễ hội', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (18, N'Thức ăn nhanh', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (19, N'Thức ăn nhẹ', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (20, N'Thức ăn từ thuỷ sản', NULL)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (21, N'Đặc sản Nghệ An', 1)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (22, N'test', 1)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (27, N'aaaa', 1)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (28, N'Món ăn của Hiệp', 1)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (29, N'Đặc sản Phú Yên', 1)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (31, N'Đặc sản Hà Nội', 1)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (32, N'Tuấn Anh', 1)
GO
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [CreatedBy]) VALUES (33, N'Đồ ăn Hoà Lạc', 1)
GO
SET IDENTITY_INSERT [dbo].[RecipeCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[RecipeComment] ON 
GO
INSERT [dbo].[RecipeComment] ([CommentId], [RecipeId], [UserId], [Comment], [Rating], [CreatedAt], [ParentCommentId]) VALUES (21, 2, 1, N'hay ngon', 0, CAST(N'2024-11-05T23:52:05.770' AS DateTime), NULL)
GO
INSERT [dbo].[RecipeComment] ([CommentId], [RecipeId], [UserId], [Comment], [Rating], [CreatedAt], [ParentCommentId]) VALUES (22, 1, 14, N'hey Hoang gay', 0, CAST(N'2024-11-06T00:13:45.067' AS DateTime), NULL)
GO
INSERT [dbo].[RecipeComment] ([CommentId], [RecipeId], [UserId], [Comment], [Rating], [CreatedAt], [ParentCommentId]) VALUES (23, 72, 14, N'tài nghé ngon chim', 0, CAST(N'2024-11-06T00:25:05.907' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[RecipeComment] OFF
GO
SET IDENTITY_INSERT [dbo].[RecipeIngredient] ON 
GO
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (15, 47, 2, 1.5, N'muỗng')
GO
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (16, 47, 29, 5, N'con')
GO
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (17, 47, 19, 0.5, N'củ')
GO
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (21, 49, 31, 1, N'vắt')
GO
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (22, 49, 28, 2, N'cái')
GO
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (23, 49, 18, 2, N'cái')
GO
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (25, 57, 32, 1, N'bó nhỏ')
GO
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (26, 57, 33, 100, N'gram')
GO
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (27, 57, 34, 1, N'củ')
GO
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (28, 1, 1, 2, N'muỗng')
GO
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (29, 1, 2, 3, N'muỗng')
GO
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (30, 1, 4, 5, N'cọng')
GO
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (31, 1, 5, 4, N'ánh')
GO
INSERT [dbo].[RecipeIngredient] ([RecipeIngredientId], [RecipeId], [IngredientId], [Quantity], [Unit]) VALUES (40, 72, 13, 1, N'thìa')
GO
SET IDENTITY_INSERT [dbo].[RecipeIngredient] OFF
GO
SET IDENTITY_INSERT [dbo].[RecipeMeal] ON 
GO
INSERT [dbo].[RecipeMeal] ([RecipeMealId], [MealId], [RecipeId]) VALUES (2, 1, 47)
GO
INSERT [dbo].[RecipeMeal] ([RecipeMealId], [MealId], [RecipeId]) VALUES (3, 2, 47)
GO
SET IDENTITY_INSERT [dbo].[RecipeMeal] OFF
GO
SET IDENTITY_INSERT [dbo].[RecipeStep] ON 
GO
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (1, 47, 1, N'Sơ chế lươn')
GO
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (2, 47, 2, N'Cho hết nguyên liệu vào')
GO
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (3, 47, 3, N'Let him cook')
GO
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (8, 49, 1, N'Trụng mì với nước sôi rồi vớt ra')
GO
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (9, 49, 2, N'Ốp trứng và rán xúc xích')
GO
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (10, 49, 3, N'Để vào tô các nguyên liệu và trộn đều')
GO
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (15, 57, 1, N'Rửa và vò nát rau ngót')
GO
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (16, 57, 2, N'Cắt nhỏ hành khô')
GO
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (17, 57, 3, N'Cho rau ngót và thịt băm vào xào với dầu trong 2 phút')
GO
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (18, 57, 4, N'Cho 650ml nước vào')
GO
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (19, 57, 5, N'Nêm nếm đủ dùng')
GO
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (20, 57, 6, N'Nấu trong 10 phút')
GO
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (21, 57, 7, N'Ăn')
GO
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (35, 72, 1, N'bỏ bà vào nồi')
GO
INSERT [dbo].[RecipeStep] ([StepId], [RecipeId], [StepNumber], [Instruction]) VALUES (36, 72, 2, N'bảo tài vớt bà ra ra chậu')
GO
SET IDENTITY_INSERT [dbo].[RecipeStep] OFF
GO
INSERT [dbo].[SignupOTP] ([SignupRequestId], [OTPString], [RequestAttemptsRemains], [ExpiresAt]) VALUES (N'b5f883b1-6a98-46fb-b901-5d396bc0b789', N'829772', 3, CAST(N'2024-11-05T21:10:02.533' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[StepImage] ON 
GO
INSERT [dbo].[StepImage] ([StepImageId], [StepId], [ImageLink]) VALUES (1, 15, N'https://cdn.eva.vn/upload/3-2014/images/2014-07-23/1406080071-sam_2270.jpg')
GO
INSERT [dbo].[StepImage] ([StepImageId], [StepId], [ImageLink]) VALUES (2, 15, N'https://s1.media.ngoisao.vn/resize_580/news/2024/04/17/vo-rau-ngot-768x1024-ngoisaovn-w768-h1024.jpg')
GO
INSERT [dbo].[StepImage] ([StepImageId], [StepId], [ImageLink]) VALUES (3, 16, N'https://afamilycdn.com/150157425591193600/2022/8/13/hanh1-1660399153146840690533.jpeg')
GO
INSERT [dbo].[StepImage] ([StepImageId], [StepId], [ImageLink]) VALUES (4, 17, N'https://bepxua.vn/wp-content/uploads/2021/08/canh-rau-ngot-thi-bam-3-1024x768.jpg')
GO
INSERT [dbo].[StepImage] ([StepImageId], [StepId], [ImageLink]) VALUES (5, 17, N'https://danviet.mediacdn.vn/zoom/700_438/296231569849192448/2023/5/15/-45-lan-rau-thuong-nau-voi-trung-rat-ngon-quet-sach-rac-trong-rau-ngot-11-1683878742-917-width780height520-1684095820893-168409-0-0-488-780-crop-168411488666372866146.jpg')
GO
INSERT [dbo].[StepImage] ([StepImageId], [StepId], [ImageLink]) VALUES (6, 18, N'https://cdn.tgdd.vn/Files/2019/09/14/1198179/canh-rau-ngot-nau-thit-thom-ngot-giup-be-an-ngon-201909142037216485.jpg')
GO
INSERT [dbo].[StepImage] ([StepImageId], [StepId], [ImageLink]) VALUES (7, 21, N'https://cdn.tgdd.vn/Files/2019/04/22/1162440/cach-nau-canh-cua-rau-ngot-xoa-tan-cai-nong-mua-he-202202231436267621.jpg')
GO
SET IDENTITY_INSERT [dbo].[StepImage] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (1, N'tranhiep', N'hieptran.pa@gmail.com', N'8bf36f8f22946050ddc06204a3890c5de30ad7c057c1d104f9e032e25397a38a', CAST(N'2024-10-28T16:14:04.223' AS DateTime), N'USER', 1)
GO
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (2, N'fewfwef', N'sdf@gmail.com', N'bfddf8322176ac637616d198f6327f75dd4f8d0be91ae2bd51261c686bef2510', CAST(N'2024-10-28T16:18:38.987' AS DateTime), N'USER', 1)
GO
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (3, N'tranhiep2', N'hiep@maol.cpl', N'bfddf8322176ac637616d198f6327f75dd4f8d0be91ae2bd51261c686bef2510', CAST(N'2024-10-28T17:00:04.213' AS DateTime), N'USER', 1)
GO
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (4, N'tranhiep22', N'hieptran.pa2@gmail.com', N'2769112f253df4764a72cb026d5f3a11d237e4c4435b3f385a14054d6be903fe', CAST(N'2024-10-28T17:07:43.070' AS DateTime), N'USER', 1)
GO
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (5, N'dskjf', N'hfwefe@gmail.com', N'bfddf8322176ac637616d198f6327f75dd4f8d0be91ae2bd51261c686bef2510', CAST(N'2024-10-28T17:18:23.133' AS DateTime), N'USER', 1)
GO
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (6, N'hieptran', N'hieptvhe173252@gmail.com', N'bfddf8322176ac637616d198f6327f75dd4f8d0be91ae2bd51261c686bef2510', CAST(N'2024-10-28T17:36:38.853' AS DateTime), N'USER', 1)
GO
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (7, N'hieptv', N'hieptv@gmail.com', N'bfddf8322176ac637616d198f6327f75dd4f8d0be91ae2bd51261c686bef2510', CAST(N'2024-10-29T10:28:18.260' AS DateTime), N'USER', 1)
GO
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (8, N'trhiwp', N'hiwp@gmail.com', N'8bf36f8f22946050ddc06204a3890c5de30ad7c057c1d104f9e032e25397a38a', CAST(N'2024-10-29T15:16:22.910' AS DateTime), N'USER', 1)
GO
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (9, N'trunghieu', N'kininmontal2003@gmail.com', N'd601ea2d3603a0ca6f97b1a2205f25d0a2ce83e9f5fe9df371c250a52501198f', CAST(N'2024-10-30T17:30:02.383' AS DateTime), N'USER', 1)
GO
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (10, N'hieptvhe173252', N'hieptvhe173252@fpt.edu.vn', N'8bf36f8f22946050ddc06204a3890c5de30ad7c057c1d104f9e032e25397a38a', CAST(N'2024-10-31T01:53:04.050' AS DateTime), N'USER', 1)
GO
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (11, N'hieptest', N'tranhiep.1326@gmail.com', N'8bf36f8f22946050ddc06204a3890c5de30ad7c057c1d104f9e032e25397a38a', CAST(N'2024-10-31T01:59:32.043' AS DateTime), N'USER', 1)
GO
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (12, N'hiwphiwp', N'hiep@maol.com', N'bfddf8322176ac637616d198f6327f75dd4f8d0be91ae2bd51261c686bef2510', CAST(N'2024-11-04T18:22:08.700' AS DateTime), N'USER', 1)
GO
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (13, N'hieptv2', N'hieptv2@gmail.com', N'bfddf8322176ac637616d198f6327f75dd4f8d0be91ae2bd51261c686bef2510', CAST(N'2024-11-05T14:12:00.810' AS DateTime), N'USER', 1)
GO
INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (14, N'huyhoang', N'peterparker.spiderman0701@gmail.com', N'8bf36f8f22946050ddc06204a3890c5de30ad7c057c1d104f9e032e25397a38a', CAST(N'2024-11-05T17:11:11.520' AS DateTime), N'USER', 1)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserFavorite] ON 
GO
INSERT [dbo].[UserFavorite] ([UserFavoriteId], [UserId], [RecipeId]) VALUES (4, 9, 57)
GO
INSERT [dbo].[UserFavorite] ([UserFavoriteId], [UserId], [RecipeId]) VALUES (6, 9, 47)
GO
INSERT [dbo].[UserFavorite] ([UserFavoriteId], [UserId], [RecipeId]) VALUES (7, 9, 49)
GO
INSERT [dbo].[UserFavorite] ([UserFavoriteId], [UserId], [RecipeId]) VALUES (8, 9, 4)
GO
INSERT [dbo].[UserFavorite] ([UserFavoriteId], [UserId], [RecipeId]) VALUES (9, 9, 8)
GO
INSERT [dbo].[UserFavorite] ([UserFavoriteId], [UserId], [RecipeId]) VALUES (10, 9, 20)
GO
INSERT [dbo].[UserFavorite] ([UserFavoriteId], [UserId], [RecipeId]) VALUES (12, 9, 47)
GO
INSERT [dbo].[UserFavorite] ([UserFavoriteId], [UserId], [RecipeId]) VALUES (13, 9, 49)
GO
INSERT [dbo].[UserFavorite] ([UserFavoriteId], [UserId], [RecipeId]) VALUES (14, 1, 22)
GO
INSERT [dbo].[UserFavorite] ([UserFavoriteId], [UserId], [RecipeId]) VALUES (15, 1, 12)
GO
INSERT [dbo].[UserFavorite] ([UserFavoriteId], [UserId], [RecipeId]) VALUES (16, 1, 2)
GO
SET IDENTITY_INSERT [dbo].[UserFavorite] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRefreshToken] ON 
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (2, 1, N'G/2fU5CKudYbnX3+UV1F+1aQy+TRwbLDXvdFuFY67vE=', CAST(N'2024-10-29T15:07:17.197' AS DateTime), CAST(N'2024-11-29T15:07:17.197' AS DateTime), N'2')
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (3, 1, N'bqMATQWZ2SPRxEMwHJtthaqgjYaEfbFCu108kiB+cX4=', CAST(N'2024-10-29T15:07:21.480' AS DateTime), CAST(N'2024-11-29T15:07:21.480' AS DateTime), N'3')
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (4, 1, N'TGWW1rdZIR0GRKVkP7UzukBzPZCQfxtvcFrnbU2fOEk=', CAST(N'2024-10-29T15:07:48.713' AS DateTime), CAST(N'2024-11-29T15:07:48.713' AS DateTime), N'1')
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (17, 9, N'Y7O62rEZ2215LHm1nwW8PP6cvKWxtnr6zJ1X3w13shI=', CAST(N'2024-10-31T00:30:10.170' AS DateTime), CAST(N'2024-11-30T00:30:10.170' AS DateTime), N'62ec91c4-955e-4692-87fe-cd13c62425a7')
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (25, 9, N'9m27IwWVox6hxLx1RP6CFrWOUF0JLAC/1Z0lWXsHePk=', CAST(N'2024-10-31T09:57:42.090' AS DateTime), CAST(N'2024-11-30T09:57:42.090' AS DateTime), N'729634c6-a4c3-42a3-8a1d-776807f8b04e')
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (26, 8, N'LPNDMDWbcWvk+hM0TYFHOWiq8eMQsouHQ7jv3Qw+LAw=', CAST(N'2024-10-31T10:42:01.630' AS DateTime), CAST(N'2024-11-30T10:42:01.630' AS DateTime), N'90eaa4bd-bede-4c3d-90d8-26a9526d99f5')
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (32, 8, N'S1Ert3LUiLULXL+cQlsVK/89KMxlrvreBZ6E3+mhiKc=', CAST(N'2024-10-31T12:38:27.723' AS DateTime), CAST(N'2024-11-30T12:38:27.723' AS DateTime), N'bf060f13-d2e8-44da-9e79-49704423556d')
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (33, 10, N'SjcAn7Y/0ssLltnRdlybvQEkAhr1DBfwJv1McrQ4f7U=', CAST(N'2024-10-31T12:40:15.367' AS DateTime), CAST(N'2024-11-30T12:40:15.367' AS DateTime), N'45dfc89c-23b1-4d54-b847-1de5d3c60f60')
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (35, 9, N'eMQ3GaS83OjuSaOLNLE73Bi1Daa5fxwct1+1s960h8c=', CAST(N'2024-10-31T13:27:47.480' AS DateTime), CAST(N'2024-11-30T13:27:47.480' AS DateTime), N'8e1defd4-f9c0-4e9f-8220-5a5b07175181')
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (39, 9, N'WERahExQeTt2L8iAJ2XcdyaKws3bNQwK4KQMffqNcrQ=', CAST(N'2024-10-31T14:23:00.580' AS DateTime), CAST(N'2024-11-30T14:23:00.580' AS DateTime), N'a433cb0c-1f89-4df1-9296-d7eebafe4c21')
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (50, 1, N'kFmNIYN2a4ksVcskFiyrPPb0EwUF9Vgh389aIad/Z1w=', CAST(N'2024-11-04T23:13:45.060' AS DateTime), CAST(N'2024-12-04T23:13:45.060' AS DateTime), N'17ec0f7a-1da2-4ddb-8af3-4bf6f088d6e4')
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (51, 1, N'TjmJpVyIclg+LU+8HeWjyT7kRj/z+95FQQAFVTi7+rs=', CAST(N'2024-11-05T09:45:19.813' AS DateTime), CAST(N'2024-12-05T09:45:19.813' AS DateTime), N'18e00bd9-ac2c-4b60-a77d-df3c1467d26f')
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (52, 1, N'67xx6NdCvgNvYh0BhK6HW2pNF31KabstQCOi5OzkGgo=', CAST(N'2024-11-05T22:40:14.517' AS DateTime), CAST(N'2024-12-05T22:40:14.517' AS DateTime), N'string')
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (53, 1, N'spGIKQDmI+jqnpXS5KpJqVSPzLUOP6j0PNmbUnIqFEI=', CAST(N'2024-11-05T23:07:51.220' AS DateTime), CAST(N'2024-12-05T23:07:51.220' AS DateTime), N'40c4fc77-79c1-4f94-adf4-39163b13e02c')
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (54, 1, N'a5+BIfgG7ELimDVwKuMDmvCtgMLdAQE5Lgt2+Y52V+w=', CAST(N'2024-11-05T23:45:53.310' AS DateTime), CAST(N'2024-12-05T23:45:53.310' AS DateTime), N'7f4c88d3-d19d-4e7f-8f7d-a984e3e62efc')
GO
INSERT [dbo].[UserRefreshToken] ([UserRefreshTokenId], [UserId], [RefreshToken], [CreatedAt], [ExpiresAt], [DeviceID]) VALUES (56, 14, N'Tb6VLxhdOzyEQz2WCwZgRe6BEtk2O0a2wmMrzPlpjK0=', CAST(N'2024-11-06T00:11:31.593' AS DateTime), CAST(N'2024-12-06T00:11:31.593' AS DateTime), N'1e8b9a31-8d5f-4e90-bd66-0702baa9a890')
GO
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
ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD  CONSTRAINT [Recipe_User_UserId_fk] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Recipe] CHECK CONSTRAINT [Recipe_User_UserId_fk]
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
ALTER TABLE [dbo].[RecipeComment]  WITH CHECK ADD  CONSTRAINT [FK_RecipeComment_ParentComment] FOREIGN KEY([ParentCommentId])
REFERENCES [dbo].[RecipeComment] ([CommentId])
GO
ALTER TABLE [dbo].[RecipeComment] CHECK CONSTRAINT [FK_RecipeComment_ParentComment]
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
