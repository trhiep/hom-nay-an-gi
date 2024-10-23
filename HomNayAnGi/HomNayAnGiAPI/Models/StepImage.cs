using System;
using System.Collections.Generic;

namespace HomNayAnGiAPI.Models
{
    public partial class StepImage
    {
        public int StepImageId { get; set; }
        public int StepId { get; set; }
        public string ImageLink { get; set; } = null!;

        public virtual RecipeStep Step { get; set; } = null!;
    }
}
