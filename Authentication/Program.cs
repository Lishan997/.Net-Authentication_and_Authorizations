using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataProtection();

var app = builder.Build();

app.MapGet("/username", (HttpContext ctx) => {

    var authCookie = ctx.Request.Headers.Cookie.FirstOrDefault(x => x.StartsWith("auth=usr"));
    var payLoad = authCookie.Split("=").Last();
    var parts = payLoad.Split(":");
    var userName = parts[1];
    return userName;
    
});


app.MapGet("/login", (HttpContext ctx, IDataProtectionProvider idp) => {

    var protector = idp.CreateProtector("auth-cookie");

    ctx.Response.Headers["set-cookie"] = "auth=usr:anton";
    return "ok";

});

app.Run();
