using Microsoft.AspNetCore.Authentication.Cookies;

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
    "eyJTb2xkRm9yIjowLjAsIktleVByZXNldCI6NiwiU2F2ZUtleSI6ZmFsc2UsIkxlZ2FjeUtleSI6ZmFsc2UsIlJlbmV3YWxTZW50VGltZSI6IjAwMDEtMDEtMDFUMDA6MDA6MDAiLCJhdXRoIjoiREVNTyIsImV4cCI6IjIwMjItMTEtMjdUMDE6MDA6MDIuMzA2NzM5MyswMDowMCIsImlhdCI6IjIwMjItMTAtMjhUMDE6MDA6MDIiLCJvcmciOiJERU1PIiwiYXVkIjo2fQ==.KOnEigxW9u2WOhy4iNfIpw+MkOBmzrHeb23JYTYZC1KdiGSYIwzHLP8NA8mljjMFf1H19XyQn9qi1ElhGWdYTLdrCxSog3dp2nbJqs9DJ+E3yRd57pEsyJtbQyqvAVn/4kLoO1YuQualommNtVjwf+uM543IMoKYfKFBg52MUhgfacCiarobGWOoTXDMrWZtSAdTzC96AqXP2FXyFJ0wWY9cSMRAx6gJ7jLvWMwBKasCZg+uUNE63gREexkT2kS4MAt39GdrFbZsbqqmhoGj8ecl6ybCPydszOdSgf4kYhhRGiqHSfMw1fajYcKdUktm53Ly2f3X4bxB4XXhnGitcvZzpKdWjlX54XdaDGZNzqd6iReXP+6/l2Ac3W+j6yFyhaW/S/0FfDS8MVCLxcCJlaa1J7o+ZZ+evPjpukBr95mAESNo9LgcMaZgTHWUQYPGO2CdZgyCcmFXnoguGStwcG+a9HnNWCxxiFVuLK48qJt8BO/vGgkflqSlC89QE6BL/FTsi3JpVXjYYASxKAQv5aaswX4D2ZTWKvZxW/PfF3yy/w2kUWWlc5rkxy/Mm5rAN51NHca2iHawJSJTFt2IOxnjwl2zpZ/eiF7BKQFXd3e+ZxMf+cDeITVnhYHFVaUC4CGWpHJHsW6bHYhIfxS8Zgvo0TZCTxTNzWlSf7IF/SI=";
}).AddInMemoryKeyStore();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.Run();
