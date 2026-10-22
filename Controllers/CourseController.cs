using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OnlineCourseEnrollment.Services;
using OnlineCourseEnrollment.Models;
using OnlineCourseEnrollment.Extensions;

namespace OnlineCourseEnrollment.Controllers;
public class CourseController : Controller {
    private readonly IMemoryCache _cache;
    public CourseController(IMemoryCache cache) => _cache = cache;

    [ResponseCache(Duration=300, Location=ResponseCacheLocation.Any, NoStore=false)]
    public IActionResult Index() {
        if (!_cache.TryGetValue("courses", out List<Course>? courses)) {
            courses = CourseService.GetCourses();
            _cache.Set("courses", courses, TimeSpan.FromMinutes(5));
        }
        return View(courses);
    }

    [ResponseCache(Duration=300, Location=ResponseCacheLocation.Any, NoStore=false)]
    public IActionResult Details(int id) {
        var course = CourseService.GetCourses().FirstOrDefault(x => x.CourseId == id);
        return course is null ? NotFound() : View(course);
    }

    [HttpGet]
    public IActionResult Enroll(int id) {
        var c = CourseService.GetCourses().FirstOrDefault(x => x.CourseId == id);
        if (c is null) return NotFound();
        HttpContext.Session.SetInt32("SelectedCourseId", c.CourseId);
        return View(new EnrollmentViewModel {
            StudentName = Request.Cookies["StudentName"] ?? "",
            CourseId = c.CourseId, CourseName = c.CourseName,
            FinalFees = c.CalculateFinalFees()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Enroll(EnrollmentViewModel model) {
        var c = CourseService.GetCourses().FirstOrDefault(x => x.CourseId == model.CourseId);
        if (c is null) return NotFound();
        if (!ModelState.IsValid) {
            model.CourseName = c.CourseName; model.FinalFees = c.CalculateFinalFees();
            return View(model);
        }
        Response.Cookies.Append("StudentName", model.StudentName, new CookieOptions {
            HttpOnly=true, IsEssential=true, Expires=DateTimeOffset.UtcNow.AddDays(7)
        });
        HttpContext.Session.SetInt32("SelectedCourseId", c.CourseId);
        TempData["Success"] = $"Enrollment submitted for {model.StudentName}.";
        return RedirectToAction(nameof(Confirmation), new { courseId = c.CourseId });
    }

    public IActionResult Confirmation(int courseId) {
        var c = CourseService.GetCourses().FirstOrDefault(x => x.CourseId == courseId);
        if (c is null) return NotFound();
        ViewBag.StudentName = Request.Cookies["StudentName"] ?? "Student";
        ViewBag.SessionCourseId = HttpContext.Session.GetInt32("SelectedCourseId");
        return View(c);
    }
}
