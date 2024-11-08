using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using HomNayAnGiApp.Models.APIModel;
using HomNayAnGiApp.Models.DTO;
using HomNayAnGiApp.Utils.JWTHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace HomNayAnGiApp.Pages.Recipes
{
    [Authorize]
    public class UpdateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly string RecipeUrl = "http://localhost:5000/api/Recipes";
        public UpdateModel(IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpClient = new HttpClient();
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            // Retrieve the token from cookies and set it as the Authorization header
            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
            Console.WriteLine("SAVED TOKEN = " + accessToken);
            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            }
        }


        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public RecipeUpdateRequest RecipeUpdate { get; set; }

        [BindProperty]
        public string LoggedInUsername { get; set; }

        [BindProperty]
        public int LoggedInUserId { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (Id != 0)
            {
                var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];
                Console.WriteLine(accessToken);
                if (string.IsNullOrEmpty(accessToken))
                {
                    return RedirectToPage("/Login/Index");
                }
                LoggedInUsername = JwtHelper.GetUsernameFromClaims(accessToken);
                LoggedInUserId = int.Parse(JwtHelper.GetUserIdFromClaims(accessToken));

                HttpResponseMessage response = await _httpClient.GetAsync($"{RecipeUrl}/{Id}");
                if (response.IsSuccessStatusCode)
                {
                    string responseJsonString = await response.Content.ReadAsStringAsync();
                    var recipeDtoApiResponse = JsonConvert.DeserializeObject<ApiResponse<RecipeDTO>>(responseJsonString);
                    if (recipeDtoApiResponse != null)
                    {
                        await Console.Out.WriteLineAsync("DTO: " + recipeDtoApiResponse.Data.ToString());
                        RecipeUpdate = _mapper.Map<RecipeUpdateRequest>(recipeDtoApiResponse.Data);
                        await Console.Out.WriteLineAsync(RecipeUpdate.ToString());
                        return Page();
                    }
                }
            }
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostUploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { error = "Không có tệp nào được chọn." });
            }

            // Upload ảnh lên Cloudinary
            var cloudinary = new Cloudinary(new Account("dpnvzshsh", "785631258213433", "VR3-KnUWxuPyGs2kVTFLtsrF8mY"));
            string folderPath = "HomNayAnGi";
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = folderPath
            };
            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new JsonResult(new { secure_url = uploadResult.SecureUri.ToString() });
            }

            return BadRequest(new { error = "Tải ảnh lên thất bại" });
        }
    }
}
