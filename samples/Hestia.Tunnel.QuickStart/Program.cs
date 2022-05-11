using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Hestia.Tunnel;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureServices((host, services) =>
{
    var yarp = builder.Configuration.GetSection("YARP");
    services.AddTunnel(yarp);
});

var app = builder.Build();

app.UseTunnel();

app.Run();
