using ArticlesViewer.Application;
using ArticlesViewer.Domain;
using ArticlesViewer.Infrastructure;
using ArticlesViewer.UI.Policies.Handlers;
using ArticlesViewer.UI.Policies.Requirements;
using Azure.Storage.Blobs;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc(options =>
{
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddSingleton(x => 
    new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobConnectionString")));

builder.Services.AddDbContext<AppDbContext>((DbContextOptionsBuilder options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDb"));
});

builder.Services.AddIdentity<User, UserRole>(options =>
{
    var identityOpt = builder.Configuration.GetSection("IdentityOptions");
    options.Password.RequiredLength = identityOpt.GetValue<int>("MinimumPasswordLength");
    options.User.RequireUniqueEmail = identityOpt.GetValue<bool>("RequireUniqueEmail");
})
.AddDefaultTokenProviders()
.AddEntityFrameworkStores<AppDbContext>()
.AddUserStore<UserStore<User, UserRole, AppDbContext, Guid>>()
.AddRoleStore<RoleStore<UserRole, AppDbContext, Guid>>();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddTransient<IAuthorizationHandler, AllowDeleteArticleHandler>();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser().Build();

    options.AddPolicy("DeleteOperation", policy =>
    {
        policy.Requirements.Add(new AllowDeleteArticleRequirement());
    });
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
