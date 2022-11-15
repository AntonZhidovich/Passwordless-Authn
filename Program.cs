using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/account/login";
});
builder.Services.AddFido(options =>
{
    options.Licensee = "DEMO";
    options.LicenseKey =
    "eyJTb2xkRm9yIjowLjAsIktleVByZXNldCI6NiwiU2F2ZUtleSI6ZmFsc2UsIkxlZ2FjeUtleSI6ZmFsc2UsIlJlbmV3YWxTZW50VGltZSI6IjAwMDEtMDEtMDFUMDA6MDA6MDAiLCJhdXRoIjoiREVNTyIsImV4cCI6IjIwMjMtMDItMDhUMDA6MDA6MDAiLCJpYXQiOiIyMDIyLTExLTA4VDEwOjI3OjE1Iiwib3JnIjoiREVNTyIsImF1ZCI6Nn0=.ZKZqMeQ+6UNNbmDJcGjcrVyhHdwsTVjWR7OZ8eifEJv/RMJGHPNzwBgXy1XVlKWlb91rMNML++7fU4NqrP8U5CG2n/em20XTuzhGMLcGZ2MPgS2YAVG1WUJ7TuMKDXkyQgj9TW8HgodIwMv4sJX9voWRXFwaNgh/2L9arDxWk/uZawgq538zgRFv7dFvfi8xiKqMr9sPD58KyRxHxTwi26tx/T9tuiI7F8+amqN9wZP7LeG/PLQzDbQM3GZD1dMu0YNeFtmbt08V2S9Wlnq21nmDcDUnniTnT1mijJV/NvbvNLGe1timjjLUJGeE30nkxV5db5/tDw9bV7areZiq9i4BVKZDfU5X5MlIvZjk6wTf6pzT7BokyYphkGfKbX7iz0gk7H13TXFU6iHLrrmm8QWfto6jEwpKWHlz7THpz5O5seH/l1R8QkdHYeCfL1f5VPTuh/qQq2UvWfdOFOBfuNAG9QMXfdVSd0lhaoRSkFrErldmObGOSKlXD0DQwV9VMjWpZ+I6kZsXK1JtDBSe5daGotElDk+3HRvizXKQ3uGvQN91zlshiiRp+e7s2NqzMxRPcyS+JxQdWQymmcK9JDF/bsFnX+q+4lQyTPchFN1B/J7mGbBd0Vq9IrDvR9wWNIc5OuUwBlsYTJgtr2hx28ppcrRbKsnXV6xZsFMB/zQ=";
}).AddInMemoryKeyStore();
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedProto;
});
var app = builder.Build();

app.UseForwardedHeaders();
app.UseAuthentication();
app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();

app.Run();
