﻿using System;
using System.Collections.Generic;

namespace HomNayAnGiAPI.Models
{
    public partial class RecipeIngredient
    {
        public int RecipeIngredientId { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public double? Quantity { get; set; }
        public string? Unit { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
