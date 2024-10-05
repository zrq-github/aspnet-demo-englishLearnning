using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting;
using Ron.EventBus;

namespace CommonInitializer
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 设置一些默认的配置
        /// </summary>
        /// <remarks>
        /// UseCors; UseForwardedHeaders; UseAuthentication; UseAuthorization;
        /// </remarks>
        /// <returns></returns>
        public static IApplicationBuilder UseDefault(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseEventBus();
            // 启用Cors
            app.UseCors();
            // 启动处理代理服务器转发。主要是Nginx服务器
            app.UseForwardedHeaders();
            //if (env.IsProduction())
            //{
            //    // 配置应用来使用来自代理服务器的头部信息
            //    app.UseForwardedHeaders(new ForwardedHeadersOptions
            //    {
            //        ForwardedHeaders = ForwardedHeaders.XForwardedProto
            //    });

            //    // 如果不是HTTPS，则重定向到HTTPS版本的URL
            //    // 可能不能与与ForwardedHeaders很好的工作，而且webapi项目也没必要配置这个
            //    app.UseHttpsRedirection();
            //}
            // 用户验证
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
