USE [master]
GO
/****** Object:  Database [HomNayAnGi]    Script Date: 10/23/2024 11:05:06 PM ******/
CREATE DATABASE [HomNayAnGi]
GO
ALTER DATABASE [HomNayAnGi] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HomNayAnGi] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HomNayAnGi] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HomNayAnGi] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HomNayAnGi] SET ARITHABORT OFF 
GO
ALTER DATABASE [HomNayAnGi] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [HomNayAnGi] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HomNayAnGi] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HomNayAnGi] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HomNayAnGi] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HomNayAnGi] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HomNayAnGi] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HomNayAnGi] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HomNayAnGi] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HomNayAnGi] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HomNayAnGi] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HomNayAnGi] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HomNayAnGi] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HomNayAnGi] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HomNayAnGi] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HomNayAnGi] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HomNayAnGi] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HomNayAnGi] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HomNayAnGi] SET  MULTI_USER 
GO
ALTER DATABASE [HomNayAnGi] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HomNayAnGi] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HomNayAnGi] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HomNayAnGi] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HomNayAnGi] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HomNayAnGi] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [HomNayAnGi] SET QUERY_STORE = ON
GO
ALTER DATABASE [HomNayAnGi] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [HomNayAnGi]
GO
/****** Object:  Table [dbo].[Ingredient]    Script Date: 10/23/2024 11:05:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredient](
	[IngredientId] [int] IDENTITY(1,1) NOT NULL,
	[IngredientName] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [Ingredient_pk] PRIMARY KEY CLUSTERED 
(
	[IngredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NutritionFact]    Script Date: 10/23/2024 11:05:06 PM ******/
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
/****** Object:  Table [dbo].[Recipe]    Script Date: 10/23/2024 11:05:06 PM ******/
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
	[IsPublic] [int] NOT NULL,
	[Video] [nvarchar](max) NULL,
 CONSTRAINT [Recipe_pk] PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeCategory]    Script Date: 10/23/2024 11:05:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeCategory](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](255) NOT NULL,
	[UserId] [int] NULL,
 CONSTRAINT [RecipeCategory_pk] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeComment]    Script Date: 10/23/2024 11:05:06 PM ******/
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
/****** Object:  Table [dbo].[RecipeIngredient]    Script Date: 10/23/2024 11:05:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipeIngredient](
	[RecipeId] [int] NOT NULL,
	[IngredientId] [int] NOT NULL,
	[Quantity] [float] NULL,
	[Unit] [nvarchar](50) NULL,
 CONSTRAINT [RecipeIngredient_pk] PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC,
	[IngredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipeStep]    Script Date: 10/23/2024 11:05:06 PM ******/
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
/****** Object:  Table [dbo].[StepImage]    Script Date: 10/23/2024 11:05:06 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 10/23/2024 11:05:06 PM ******/
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
/****** Object:  Table [dbo].[UserFavorite]    Script Date: 10/23/2024 11:05:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserFavorite](
	[UserId] [int] NOT NULL,
	[RecipeId] [int] NOT NULL,
 CONSTRAINT [UserFavorite_pk] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RecipeCategory] ON 

INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (1, N'Ẩm thực đường phố', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (2, N'Bánh', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (3, N'Canh', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (4, N'Cơm', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (5, N'Món chiên', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (6, N'Mì', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (7, N'Miến', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (8, N'Món hầm', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (9, N'Món khai vị', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (10, N'Món mặn', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (11, N'Món ngọt', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (12, N'Món nướng', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (13, N'Món tráng miệng', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (14, N'Nước xốt', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (15, N'Salad', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (16, N'Thức ăn chơi', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (17, N'Thức ăn dịp lễ hội', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (18, N'Thức ăn nhanh', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (19, N'Thức ăn nhẹ', NULL)
INSERT [dbo].[RecipeCategory] ([CategoryId], [CategoryName], [UserId]) VALUES (20, N'Thức ăn từ thuỷ sản', NULL)
SET IDENTITY_INSERT [dbo].[RecipeCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [Username], [Email], [Password], [CreatedAt], [Role], [IsActive]) VALUES (1, N'admin', N'admin@homnayangi.com', N'1', CAST(N'2024-10-23T22:44:24.457' AS DateTime), N'ADMIN', 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Recipe] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Recipe] ADD  DEFAULT (getdate()) FOR [UpdatedAt]
GO
ALTER TABLE [dbo].[RecipeComment] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (getdate()) FOR [CreatedAt]
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
ALTER TABLE [dbo].[RecipeCategory]  WITH CHECK ADD  CONSTRAINT [RecipeCategory_User_UserId_fk] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[RecipeCategory] CHECK CONSTRAINT [RecipeCategory_User_UserId_fk]
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
ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD  CONSTRAINT [CHECK_0] CHECK  (([DifficultyLevel]='hard' OR [DifficultyLevel]='medium' OR [DifficultyLevel]='easy'))
GO
ALTER TABLE [dbo].[Recipe] CHECK CONSTRAINT [CHECK_0]
GO
ALTER TABLE [dbo].[RecipeComment]  WITH CHECK ADD  CONSTRAINT [CHECK_1] CHECK  (([Rating]>=(1) AND [Rating]<=(5)))
GO
ALTER TABLE [dbo].[RecipeComment] CHECK CONSTRAINT [CHECK_1]
GO
USE [master]
GO
ALTER DATABASE [HomNayAnGi] SET  READ_WRITE 
GO
