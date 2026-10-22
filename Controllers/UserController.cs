using Microsoft.AspNetCore.Mvc;
using OnlineCourseEnrollment.Models;
using OnlineCourseEnrollment.Services;
namespace OnlineCourseEnrollment.Controllers;
public class UserController : Controller {
    private readonly IHttpClientFactory _factory;
    public UserController(IHttpClientFactory factory) => _factory = factory;
    public async Task<IActionResult> Index(CancellationToken ct) {
        try {
            var service = new UserService(_factory.CreateClient());
            return View(await service.GetUsersAsync(ct));
        } catch (HttpRequestException) {
            ViewBag.Error = "Could not retrieve users from JSONPlaceholder.";
            return View(new List<User>());
        }
    }
}
