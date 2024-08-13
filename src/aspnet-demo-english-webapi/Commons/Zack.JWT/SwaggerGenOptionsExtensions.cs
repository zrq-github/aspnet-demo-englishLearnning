using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Zack.JWT
{
    public static class SwaggerGenOptionsExtensions
    {
        /// <summary>
        /// 为Swagger增加Authentication报文头
        /// </summary>
        /// <remarks>
        /// ASP.NET Core要求（这也是HTTP的规范）JWT放到名字为Authorization的HTTP请求报文头中，
        /// 报文头的值为“Bearer JWT”​。默认情况下，我们无法在Swagger中添加请求报文头，
        /// 因此我们需要借助第三方工具来发送带自定义报文头的HTTP请求。
        /// </remarks>
        public static void AddAuthenticationHeader(this SwaggerGenOptions c)
        {
            c.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
            {
                Description = "Authorization header. \r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Authorization"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Authorization"
                        },
                        Scheme = "oauth2",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        }
    }
}
