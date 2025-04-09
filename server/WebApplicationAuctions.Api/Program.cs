using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using WebApplicationAuctions.Api.Middleware.Extensions;
using WebApplicationAuctions.Core.Interfaces;
using WebApplicationAuctions.Core.Middleware;
using WebApplicationAuctions.Core.Services;
using WebApplicationAuctions.Data.DataModels;
using WebApplicationAuctions.Data.Interfaces;
using WebApplicationAuctions.Data.Repository;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAuthenticationExtentsion(
    issuer: "http://localhost:5120"
    , audience: "http://localhost:5120"
    , signingKey: "OurSuperSecretKeyForOurApi123456789hushhush");






builder.Services.AddControllers();


builder.Services.AddSwaggerExtended();

builder.Services.AddSingleton<IAuctionsContext, AuctionsContext>();

builder.Services.AddScoped<IUserRepo,UsersRepo>();      //Incase of name changes i commented....
builder.Services.AddScoped<IUsersService,UsersService>();     //Incase of name changes i commented....
builder.Services.AddScoped<IAuctionRepo, AuctionRepo>();
builder.Services.AddScoped<IAuctionService, AuctionService>();

builder.Services.AddScoped<IImageRepo, ImageRepo>();



builder.Services.AddCors();


builder.Services.AddScoped<IBidsRepo,BidsRepo>();
builder.Services.AddScoped<IBidsService, BidsService>();

builder.Services.AddScoped<IAuctionRepo, AuctionRepo>();
builder.Services.AddScoped<IAuctionService, AuctionService>();

builder.Services.AddScoped<ISearchRepo, SearchRepo>();
builder.Services.AddScoped<ISearchService, SearchService>();

builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddTransient<IJwtGenerator,JWTGenerator>();

var app = builder.Build();

app.UseRouting();

app.UseSwaggerExtended();


app.UseAuthentication();
app.UseAuthorization();

app.UseCors(builder =>
            builder.AllowAnyMethod()
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader());


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
