namespace HomNayAnGiApp.Middlewares
{
    public class JwtTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Lấy access token từ cookie
            if (context.Request.Cookies.TryGetValue("accessToken", out var accessToken))
            {
                // Thêm access token vào header
                context.Request.Headers["Authorization"] = $"Bearer {accessToken}";
            }

            // Gọi middleware tiếp theo trong pipeline
            await _next(context);
        }
    }
}
