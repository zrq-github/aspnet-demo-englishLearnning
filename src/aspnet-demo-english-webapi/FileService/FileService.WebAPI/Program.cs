using CommonInitializer;
using FileService.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureDbConfiguration();
builder.ConfigureExtraServices(new InitializerOptions
{
    LogFilePath = @"C:/Dev/aspnet-demo/FileService.log",
    EventBusQueueName = "FileService.WebAPI",
});

// Add services to the container.
builder.Services//.AddOptions() //asp.net core项目中AddOptions()不写也行，因为框架一定自动执行了
    .Configure<SMBStorageOptions>(builder.Configuration.GetSection("FileService:SMB"))
    .Configure<UpYunStorageOptions>(builder.Configuration.GetSection("FileService:UpYun"));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "FileService.WebAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FileService.WebAPI v1"));
}
app.UseStaticFiles();
app.UseDefault(app.Environment);

app.MapControllers();

app.Run();
