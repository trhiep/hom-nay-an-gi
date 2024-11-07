namespace HomNayAnGiApp.Middlewares
{
    public class JwtCookieAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtCookieAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Cookies.TryGetValue("accessToken", out var token))
            {
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            }

            await _next(context);
        }
    }
}
