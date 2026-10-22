using System.ComponentModel.DataAnnotations;
namespace OnlineCourseEnrollment.Models;
public class Course {
    [Display(Name="Course ID")] public int CourseId { get; set; }
    [Required] public string CourseName { get; set; } = "";
    [Required] public string Duration { get; set; } = "";
    [Range(0,1000000)] public decimal Fees { get; set; }
    [Range(0,100)] public decimal DiscountPercentage { get; set; }
    public decimal CalculateFinalFees() => Fees - (Fees * DiscountPercentage / 100m);
}
