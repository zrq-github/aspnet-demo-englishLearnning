namespace 中间件流程
{
    public class TestMiddleware:IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("TestMiddleware Start<br/>");
            await next.Invoke(context);
            await context.Response.WriteAsync("TestMiddleware End<br/>");
        }
    }
}
