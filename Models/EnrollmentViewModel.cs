using System.ComponentModel.DataAnnotations;
namespace OnlineCourseEnrollment.Models;
public class EnrollmentViewModel {
    [Required] public string StudentName { get; set; } = "";
    public int CourseId { get; set; }
    public string CourseName { get; set; } = "";
    public decimal FinalFees { get; set; }
}
