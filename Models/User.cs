namespace OnlineCourseEnrollment.Models;
public class User {
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public Company Company { get; set; } = new();
}
public class Company { public string Name { get; set; } = ""; }
