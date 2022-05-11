using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Hestia.Tunnel
{
    [ExcludeFromCodeCoverage]
    public static class TunnelExtensions
    {
        public static void AddTunnel(this IServiceCollection services,IConfiguration configuration, Func<HttpContext, IServiceProvider, ITunnel> builder = null)
        {
            services.AddReverseProxy().LoadFromConfig(configuration);
            services.AddScoped(sp => new TunnelMiddleware(sp, builder));
        }

        public static void UseTunnel(this WebApplication app, Action<IReverseProxyApplicationBuilder> before = null, Action<IReverseProxyApplicationBuilder> after = null)
        {
            app.MapReverseProxy(proxy => {
                before?.Invoke(proxy);
                proxy.UseMiddleware<TunnelMiddleware>();
                after?.Invoke(proxy);
            });
        }
    }
}
