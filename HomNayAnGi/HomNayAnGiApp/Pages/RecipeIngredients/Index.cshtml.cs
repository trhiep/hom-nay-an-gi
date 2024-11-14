using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomNayAnGiApp.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using HomNayAnGiApp.Utils.JWTHelper;

namespace HomNayAnGiApp.Pages.RecipeIngredients
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly HomNayAnGiApp.Models.HomNayAnGiContext _context;
        private readonly string IngredientUrl = "http://localhost:5000/api/Ingredients";
        private readonly HttpClient _httpClient;

        private readonly IHttpContextAccessor _contextAccessor;
        

        public IndexModel(HomNayAnGiApp.Models.HomNayAnGiContext context, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;

            _context = context;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public IList<Ingredient> Ingredient { get;set; } = default!;
        public int LoggedInUserId { get; set; }   =default!;
        public string GetRole { get; set; } = default!;
        [BindProperty]
        public string? LoggedInUsername { get; set; }

        public async Task OnGetAsync()
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


            HttpResponseMessage response = await _httpClient.GetAsync(IngredientUrl);
            if (response.IsSuccessStatusCode)
            {
                string recipeDtoJSONString = await response.Content.ReadAsStringAsync();
                
                if(GetRole.Equals("ADMIN"))
                {
                    Ingredient = JsonConvert.DeserializeObject<IList<Ingredient>>(recipeDtoJSONString).ToList();
                }
                else
                {
                    Ingredient = JsonConvert.DeserializeObject<IList<Ingredient>>(recipeDtoJSONString).Where(x=>x.CreatedBy == LoggedInUserId).ToList();
                }
            }
        }
    }
}
