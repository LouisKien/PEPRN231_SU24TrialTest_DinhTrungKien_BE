using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PEPRN231_SU24TrialTest_DinhTrungKien_BE.Repositories.Models
{
    public partial class WatercolorsPainting
    {
        [Key]
        public string PaintingId { get; set; } = null!;
        [RegularExpression(@"^(([A-Z][a-z0-9]*|\d+)\s)*([A-Z][a-z0-9]*|\d+)$", ErrorMessage = "Each word must start with a capital letter and can include lowercase letters, spaces, and digits.")]
        public string PaintingName { get; set; } = null!;
        public string? PaintingDescription { get; set; }
        public string? PaintingAuthor { get; set; }
        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }
        [Range(1000, int.MaxValue)]
        public int? PublishYear { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? StyleId { get; set; }

        public virtual Style? Style { get; set; }
    }
}
