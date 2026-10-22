using OnlineCourseEnrollment.Models;

namespace OnlineCourseEnrollment.Services;
public static class CourseService {
    public static List<Course> GetCourses() => new() {
        new() { CourseId=101, CourseName="ASP.NET Core MVC", Duration="6 Weeks", Fees=12000, DiscountPercentage=10 },
        new() { CourseId=102, CourseName="C# Programming", Duration="5 Weeks", Fees=10000, DiscountPercentage=5 },
        new() { CourseId=103, CourseName="Entity Framework Core", Duration="4 Weeks", Fees=9000, DiscountPercentage=15 },
        new() { CourseId=104, CourseName="Web API Development", Duration="6 Weeks", Fees=14000, DiscountPercentage=20 }
    };
}
