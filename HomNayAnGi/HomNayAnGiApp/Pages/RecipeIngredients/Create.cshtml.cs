using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HomNayAnGiApp.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using HomNayAnGiApp.Utils.JWTHelper;

namespace HomNayAnGiApp.Pages.RecipeIngredients
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly HomNayAnGiApp.Models.HomNayAnGiContext _context;
        private readonly string IngredientUrl = "http://localhost:5000/api/Ingredients";
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _contextAccessor;
        private string GetRole;
        private int LoggedInUserId;
        public CreateModel(HomNayAnGiApp.Models.HomNayAnGiContext context, IHttpContextAccessor contextAccessor)
        {
            
            _contextAccessor = contextAccessor;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _context = context;
        }

        //public IActionResult OnGet()
        //{

        //}

        [BindProperty]
        public string LoggedInUsername { get; set; }

        [BindProperty]
        public Ingredient Ingredient { get; set; } = default!;
        
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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

            if (GetRole.Equals("ADMIN"))
            {
                Ingredient.CreatedBy = 0;
            } else
            {
                Ingredient.CreatedBy = LoggedInUserId;
            }

            string jsonStr = JsonConvert.SerializeObject(Ingredient);
            
            

            var jsonContent = new StringContent(jsonStr, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(IngredientUrl, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return BadRequest();
            }
              


    }
}
}
