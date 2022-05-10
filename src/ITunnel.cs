using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Hestia.Tunnel
{
    public interface ITunnel
    {
        Task<bool> BeforeInvoke(HttpContext context);

        Task AfterInvoke(HttpContext context);
    }
}
