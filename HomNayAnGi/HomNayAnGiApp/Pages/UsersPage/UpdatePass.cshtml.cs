using HomNayAnGiApp.Models;
using HomNayAnGiApp.Utils.JWTHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace HomNayAnGiApp.Pages.UsersPage
{
    [Authorize]
    public class UpdatePassModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdatePassModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public int LoggedInUserId { get; set; }

        [BindProperty]
        public string? LoggedInUsername { get; set; }
        public async Task<IActionResult> OnPost(string oldpass, string pass1, string pass2)
        {
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
            Console.WriteLine(accessToken);
            if (string.IsNullOrEmpty(accessToken))
            {
                return RedirectToPage("/Login/Index");
            }
            LoggedInUserId = int.Parse(JwtHelper.GetUserIdFromClaims(accessToken));

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"http://localhost:5000/api/Users/{LoggedInUserId}"; // Replace with the actual category ID
                    HttpResponseMessage userResponse = await client.GetAsync(url);
                    var user = userResponse.Content.ReadFromJsonAsync<User>().Result;

                    var updatedUser = new User
                    {
                        UserId = LoggedInUserId,  // Replace with the ID of the category to update
                        Password = pass1,
                        Role = user?.Role!,
                        Username = user?.Username!,
                        Email = user?.Email,
                        CreatedAt = user?.CreatedAt,
                        IsActive = user.IsActive,
                    };

                    string jsonContent = JsonSerializer.Serialize(updatedUser);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Password updated successfully.");
                    }
                    else
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error: {response.StatusCode}");
                        Console.WriteLine($"Response Body: {responseBody}");
                    }
                    return RedirectToPage("/Index");
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                    return Page();
                }
            }

        }
    }
}
