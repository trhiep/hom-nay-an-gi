using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HomNayAnGiAPI.Models
{
    public partial class RecipeStep
    {
        public RecipeStep()
        {
            StepImages = new HashSet<StepImage>();
        }

        public int StepId { get; set; }
        public int? RecipeId { get; set; }
        public int? StepNumber { get; set; }
        public string? Instruction { get; set; }

        public virtual Recipe? Recipe { get; set; }
        [JsonIgnore]
        public virtual ICollection<StepImage> StepImages { get; set; }
    }
}
