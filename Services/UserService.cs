using OnlineCourseEnrollment.Models;

using System.Net.Http.Json;
namespace OnlineCourseEnrollment.Services;
public class UserService {
    private readonly HttpClient _client;
    public UserService(HttpClient client) => _client = client;
    public async Task<List<User>> GetUsersAsync(CancellationToken ct = default) =>
        await _client.GetFromJsonAsync<List<User>>("https://jsonplaceholder.typicode.com/users", ct) ?? new();
}
