using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Hestia.Tunnel
{
    public class TunnelMiddleware: IMiddleware
    {
        readonly Func<HttpContext, IServiceProvider, ITunnel> Builder;
        readonly IServiceProvider Services;

        public TunnelMiddleware(IServiceProvider services, Func<HttpContext, IServiceProvider, ITunnel> builder)
        {
            Builder = builder;
            Services = services;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            ITunnel tunnel = Builder?.Invoke(context,Services);
            if(tunnel == null) { await next.Invoke(context); return; }
            if (await tunnel.BeforeInvoke(context)) 
            {
                await next.Invoke(context);
                await tunnel.AfterInvoke(context);
            }            
        }
    }
}