using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomNayAnGiApp.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using HomNayAnGiApp.Utils.JWTHelper;

namespace HomNayAnGiApp.Pages.RecipeIngredients
{
    public class EditModel : PageModel
    {
        private readonly HomNayAnGiApp.Models.HomNayAnGiContext _context;
        private readonly string IngredientUrl = "http://localhost:5000/api/Ingredients/";
        private readonly HttpClient _httpClient;
        private string LoggedInUsername;
        private string GetRole;
        private int LoggedInUserId;
        private readonly IHttpContextAccessor _contextAccessor;

        public EditModel(HomNayAnGiApp.Models.HomNayAnGiContext context, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _context = context; 
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [BindProperty]
        public Ingredient Ingredient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            //Get user cookies
            var accessToken = _contextAccessor.HttpContext?.Request.Cookies["accessToken"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer" + accessToken);
            }
            LoggedInUsername = JwtHelper.GetUsernameFromClaims(accessToken);
            LoggedInUserId = int.Parse(JwtHelper.GetUserIdFromClaims(accessToken));
            GetRole = JwtHelper.GetRoleFromJwt(accessToken);


            if (id == null)
            {
                return NotFound();
            }
            HttpResponseMessage response = await _httpClient.GetAsync(IngredientUrl + id);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Ingredient = JsonConvert.DeserializeObject<Ingredient>(jsonStr);

            }

            if (Ingredient.CreatedBy != LoggedInUserId && !GetRole.Equals("ADMIN"))
            {
                return BadRequest();
            }
          

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            //Get user cookies
            var accessToken = _contextAccessor.HttpContext?.Request.Cookies["accessToken"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer" + accessToken);
            }
            LoggedInUsername = JwtHelper.GetUsernameFromClaims(accessToken);
            LoggedInUserId = int.Parse(JwtHelper.GetUserIdFromClaims(accessToken));
            GetRole = JwtHelper.GetRoleFromJwt(accessToken);

            string jsonStr = JsonConvert.SerializeObject(Ingredient);
            
            var content = new StringContent(jsonStr, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(IngredientUrl+Ingredient.IngredientId, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }

        private bool IngredientExists(int id)
        {
          return (_context.Ingredients?.Any(e => e.IngredientId == id)).GetValueOrDefault();
        }
    }
}
