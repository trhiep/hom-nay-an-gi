using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HomNayAnGiApp.Middlewares
{
    public class RedirectUnauthorizedFilter : IAsyncPageFilter
    {
        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var resultContext = await next();

            // Kiểm tra mã lỗi 401 và điều hướng về trang Login
            if (resultContext.HttpContext.Response.StatusCode == 401)
            {
                resultContext.HttpContext.Response.Redirect("/Login/Index");
            }
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }
    }

    public class UnauthorizedExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.HttpContext.Response.StatusCode == 401)
            {
                context.Result = new RedirectToPageResult("/Account/Login");
            }
        }
    }
}
