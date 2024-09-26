
namespace 中间件流程
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //builder.Services.AddControllers();
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            // 通过IMiddleware来实现注册中间件，
            builder.Services.AddSingleton<TestMiddleware>();
            
            var app = builder.Build();

            // 显示注册中间件
            app.MapGet("/", () => "Hello World");
            app.MapGet("/test", () => "zrq test");
            app.Map("/test1", async mapbuilder =>
            {
                mapbuilder.Use(async (context, next) =>
                {
                    context.Response.ContentType = "text/html";
                    await context.Response.WriteAsync("1 Start<br/>");
                    await next.Invoke();
                    await context.Response.WriteAsync("1 End<br/>");
                });
                mapbuilder.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("2 Start<br/>");
                    await next.Invoke();
                    await context.Response.WriteAsync("2 End<br/>");
                });
                // 使用自定义中间件
                mapbuilder.UseMiddleware<TestMiddleware>();
                mapbuilder.Run(async ctx =>
                {
                    await ctx.Response.WriteAsync("hello middleware <br/>");
                }) ;
            });

            app.Run();
        }

        public static void DefaultMain(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
