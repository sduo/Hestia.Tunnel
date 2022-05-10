using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Hestia.Tunnel
{
    public class TunnelMiddleware: IMiddleware
    {
        readonly Func<HttpContext, ITunnel> Builder;

        public TunnelMiddleware(Func<HttpContext, ITunnel> builder)
        {
            Builder = builder;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            ITunnel tunnel = Builder?.Invoke(context);
            if (await tunnel?.BeforeInvoke(context) == false) { return; }
            await next.Invoke(context);
            await tunnel?.AfterInvoke(context);
        }
    }
}