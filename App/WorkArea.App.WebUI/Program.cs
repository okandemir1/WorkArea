using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Globalization;
using System.Text.Unicode;
using WorkArea.App.WebUI.Helpers;
using WorkArea.Infrastructure;

var cultureInfo = new CultureInfo("tr-TR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddContextInfrastructure(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600;
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddMemoryCache();



builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(1200);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(365);
    options.Events.OnRedirectToLogin = (context) =>
    {
        if (context.Request.Headers["Content-Type"].Contains("application/json"))
        {
            context.HttpContext.Response.StatusCode = 401;
        }
        else
        {
            context.Response.Redirect(context.RedirectUri);
        }

        return Task.CompletedTask;
    };
});

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAuthorization();
builder.Services.AddSession();
builder.Services.AddTransient<SessionHelper>();

builder.Services.AddWebEncoders(o =>
{
    o.TextEncoderSettings = new System.Text.Encodings.Web.TextEncoderSettings(UnicodeRanges.All);
});

builder.Services.AddMvc(x => x.EnableEndpointRouting = false).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context =>
    {
        context.Context.Response.Headers.Add("cache-control", new[] { "public,max-age=31536000" });
        context.Context.Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") }); // Format RFC1123
    }
});

app.UseRouting();
app.UseStaticFiles();
app.UseStatusCodePages();
app.UseAuthentication();
app.UseSession();

app.UseMvc(routes =>
{
    routes.MapRoute("default", "{controller}/{action}/{id?}", new { controller = "home", action = "index" });
});

app.Run();
