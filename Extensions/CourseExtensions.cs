using OnlineCourseEnrollment.Models;

namespace OnlineCourseEnrollment.Extensions;
public static class CourseExtensions {
    public static decimal CalculateFinalFees(this Course c) => c.Fees - c.Fees * c.DiscountPercentage / 100m;
    public static string GetFeeLabel(this Course c) => c.DiscountPercentage > 0 ? $"{c.DiscountPercentage:0.##}% discount" : "No discount";
}
